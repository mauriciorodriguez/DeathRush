using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VehicleData))]
[RequireComponent(typeof(InputController))]
public abstract class Vehicle : MonoBehaviour
{
    public VehicleVars vehicleVars;
    public string vehicleName;
    public static int _uid = 0;
    public Transform primaryWeaponPlaceholder, secondaryWeaponPlaceholder, gadgetPlaceholder;
    public float positionWeight { get; protected set; }
    public float lapCount { get; protected set; }

    public GameObject carFootprintPrefab;
    public Transform[] wheelMeshList = new Transform[4];
    public WheelCollider[] wheelColliderList = new WheelCollider[4];
   
    public bool isGrounded { private set; get; }
    public int racerPosition { private set; get; }
    public int uID { private set; get; }
    public bool isDestroyed = false;
    public bool hasEnded = false;

    public event Action<Vehicle> OnDeath = delegate { };
    public event Action<Vehicle> OnSpeedCheck = delegate { };
    public event Action<Vehicle> OnLapCount = delegate { };
    public event Action<Vehicle> OnRaceFinished = delegate { };

    protected bool _modeNitro = false;
    protected float _nitroTimer;
    protected bool _nitroEnd;
    protected float _lapsEnded;
    public bool _canRechargeNitro;
    protected bool _nitroEmpty;
    protected bool _countInAir;
    protected float _timerWrongDirection;

    //public Camera rearMirror;
    protected float _steerInput, _motorInput, _handbrakeInput;
    protected float _finalAngle;
    protected float resetTimer;
    public ParticleSystem backDust;
    protected AudioSource engineSound;
    public List<int> totalGears;
    public RacerData racer;
    public WeaponsManager weaponManager;
    [HideInInspector]
    public bool isCrasher, canHyperCharge;
    [HideInInspector]
    public int playerRacerID;

    protected int _checkpointNumber;
    protected Checkpoint _lastCheckpoint;
    protected CheckpointManager _checkpointMananagerReference;
    protected Rigidbody _rb;
    protected bool _isGroundedRamp, _canAdjustVelocityZ;
    protected List<GameObject> _wheelTrails;
    protected float _lastGroundedVelocityZ;
    protected bool _stoped = false;
    protected float _timerReset;
    protected float _resetWaitTime = 3;
    protected PlayerData _playerData;
    protected bool _canForceRespawn, _canShakeCamera;
    protected float  _airTimer;

    private bool _reversing;
    public Vector3 centerOfMass;
    private WheelDriveType _prevWheelDriveType;
    private float _prevTorque;
    public float _localCurrentSpeed { get; private set; }
    public float currentSpeed { get; private set; }
    protected bool _oilEffect;

    protected virtual void Start()
    {
        uID = _uid++;
        _playerData = PlayerData.instance;
        _checkpointMananagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<CheckpointManager>();
        isDestroyed = false;
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centerOfMass;

        /*_wheelTrails = new List<TrailRenderer>();
        foreach (var wheel in wheelSuspensionList)
        {
            var trail = wheel.GetComponentInChildren<TrailRenderer>();
            trail.enabled = false;
            _wheelTrails.Add(trail);
        }*/
        Cursor.visible = false;
        lapCount = 0;
        positionWeight = -Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[0].transform.position);
        _checkpointNumber = 0;
        _nitroTimer = vehicleVars.nitroTimer;
        _lapsEnded = 1;
        _nitroEmpty = false;
        /*_wheelTrails = new List<GameObject>();
        foreach (var wheel in wheelMeshList)
        {
            var go = Instantiate(carFootprintPrefab);
            go.transform.parent = wheel.transform.parent;
            go.transform.localPosition = new Vector3(0, -wheel.GetComponent<MeshRenderer>().bounds.extents.y, 0);
            _wheelTrails.Add(go);
        }*/
        engineSound = GetComponent<AudioSource>();
        weaponManager = GetComponentInChildren<WeaponsManager>();
    }

    public void EnableComponents(bool b)
    {
        GetComponent<Rigidbody>().isKinematic = !b;
        GetComponent<InputController>().enabled = b;
        GetComponent<AudioSource>().enabled = b;
        GetComponent<Vehicle>().enabled = b;
        GetComponent<VehicleData>().enabled = b;
        GetComponentInChildren<WeaponsManager>().enabled = b;
    }

    public void InstantiateWeapons(Weapon.Type primary, Weapon.Type secondary, Weapon.Type gadget, bool isInExhibition = false)
    {
        GameObject primaryWeapon = null;
        GameObject secondaryWeapon = null;
        GameObject gadgetGo = null;

       if (primary != Weapon.Type.Null) primaryWeapon = Weapon.prefabsDict[primary]();
       if (secondary != Weapon.Type.Null) secondaryWeapon = Weapon.prefabsDict[secondary]();
       if (gadget != Weapon.Type.Null) gadgetGo = Weapon.prefabsDict[gadget]();

        if (primaryWeapon != null)
        {
            var aux = Instantiate(primaryWeapon);
            aux.transform.parent = primaryWeaponPlaceholder;
            aux.transform.localPosition = Vector3.zero;
            aux.transform.rotation = primaryWeaponPlaceholder.rotation;
            if (isInExhibition)
            {
                aux.GetComponent<Weapon>().activeShoot = false;
            }
            else
                aux.GetComponent<Weapon>().activeShoot = true;
        }
        if (secondaryWeapon != null)
        {
            var aux = Instantiate(secondaryWeapon);
            aux.transform.parent = secondaryWeaponPlaceholder;
            aux.transform.localPosition = Vector3.zero;
            aux.transform.rotation = secondaryWeaponPlaceholder.rotation;
            if (isInExhibition)
            {
                aux.GetComponent<Weapon>().activeShoot = false;
            }
            else
                aux.GetComponent<Weapon>().activeShoot = true;
        }
        if (gadgetGo != null)
        {
            gadgetPlaceholder.GetComponent<GadgetLauncher>().SetGadget(gadgetGo);
            if (isInExhibition)
                gadgetPlaceholder.GetComponent<GadgetLauncher>().enabled = false;
            else
                gadgetPlaceholder.GetComponent<GadgetLauncher>().enabled = true;
        }
    }

    /// <summary>
    /// Destruccion del vehiculo.
    /// </summary>
    public void Die()
    {
        isDestroyed = true;
        gameObject.SetActive(false);
        OnDeath(this);
    }

    /// <summary>
    /// Finalizacion de la carrera del vehiculo.
    /// </summary>
    public void RaceFinished()
    {
        hasEnded = true;
        OnRaceFinished(this);
    }

    /// <summary>
    /// Conteo de vueltas.
    /// </summary>
    public void LapCount()
    {
        OnLapCount(this);
    }

    /// <summary>
    /// Checkeo de velocidad.
    /// </summary>
    public void SpeedCheck()
    {
        OnSpeedCheck(this);
    }

    /// <summary>
    /// Setea la posicion del corredor.
    /// </summary>
    /// <param name="pos">Nueva posicion</param>
    public void setRacerPosition(int pos)
    {
        racerPosition = pos;
    }

    /// <summary>
    /// Asigna el proximo checkpoint.
    /// </summary>
    /// <param name="chk">Checkpoint actual</param>
    public virtual void SetCheckpoint(Checkpoint chk)
    {
        _checkpointNumber = _checkpointMananagerReference.checkpointsList.Count - 1 == _checkpointNumber ? 0 : _checkpointNumber + 1;
        _lastCheckpoint = chk;
        lapCount += _checkpointMananagerReference.checkpointValue;
    }

    /// <summary>
    /// Mueve el vehiculo
    /// </summary>
    /// <param name="accelInput">Input aceleracion</param>
    /// <param name="brakeInput">Input freno</param>
    /// <param name="handbrakeInput">Input freno de mano</param>
    /// <param name="steerInput">Input de giro</param>
    /// <param name="nitroInput">Input de nitro</param>
    public virtual void Move(float motorInput, float handbrakeInput, float steerInput, float nitroInput)
    {
    //    _tempTopSpeed = vehicleVars.topSpeed;

     //   if (_oilEffect) steerInput *= -1f;
        _steerInput = steerInput;
        _motorInput = motorInput;
        _handbrakeInput = handbrakeInput;

        if (!_isGroundedRamp && !isGrounded && !_canAdjustVelocityZ) _canAdjustVelocityZ = true;
 /*       isGrounded = false;
        _isGroundedRamp = false;*/
        ApplyDrive();
        NitroInput(nitroInput, motorInput);
      //  Drag(accelInput, brakeInput);
        ApplyHandbrake();
        AddDownForce();
    }

    protected virtual void Update()
    {      
        positionWeight = Vector3.Distance(transform.position, _checkpointMananagerReference.checkpointsList[_checkpointNumber].transform.position);
        CheckIfGrounded();
        WheelsRotation();
        CheckDustVehicle();
        EngineSound();
        if (vehicleVars.vehicleType != VehicleVars.Type.Alien) WheelEffects();
        SetAirRotation();
        SpeedCheck();
        CheckCarVelocityForRestart();
     //   CheckRotationToAdjustVelocity();

      
    }


    /// <summary>
    /// Previene exceso de rotacion en el aire
    /// </summary>
    protected void SetAirRotation()
    {
        if (!_isGroundedRamp && !isGrounded) _airTimer += Time.deltaTime;
        else _airTimer = 0;
        if (!_isGroundedRamp && !isGrounded && _airTimer > .5f)
        {
            _canShakeCamera = true;
            var desiredRotation = transform.rotation.eulerAngles;
            desiredRotation.x = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(desiredRotation), .3f);
        }

    }

    protected virtual void ApplyHandbrake()
    { }

    /// <summary>
    /// Si el vehiculo rota mas de los angulos establecidos en angleToAdjust ya no reajustara la velocidad
    /// </summary>
    protected void CheckRotationToAdjustVelocity()
    {
        if (!_canAdjustVelocityZ) return;
        float angleToAdjust = 60;
        if (gameObject.transform.eulerAngles.x < (360 - angleToAdjust) &&
            gameObject.transform.eulerAngles.x > angleToAdjust ||
            /*gameObject.transform.eulerAngles.y < (360 - angleToAdjust) &&
            gameObject.transform.eulerAngles.y > angleToAdjust ||*/
            gameObject.transform.eulerAngles.z < (360 - angleToAdjust) &&
            gameObject.transform.eulerAngles.z > angleToAdjust
            )
        {
            _canAdjustVelocityZ = false;
            _lastGroundedVelocityZ = 0;
        }
    }

    /// <summary>
    /// Checkeo de velocidad del vehiculo para posible respawn.
    /// </summary>
    protected void CheckCarVelocityForRestart()
    {
        if (!GetComponent<InputControllerPlayer>())
        {
            if (_localCurrentSpeed * K.MPS_TO_MPH_MULTIPLIER < 20 && !_stoped)
            {
                _stoped = true;
            }
            else if (_stoped && _localCurrentSpeed * K.MPS_TO_MPH_MULTIPLIER > 20)
            {
                _stoped = false;
                _timerReset = 0;
            }
        }
        else
        {
            if (transform.localEulerAngles.z > 60 && transform.localEulerAngles.z < 300 && _localCurrentSpeed * K.MPS_TO_MPH_MULTIPLIER < 20)
            {
                _stoped = true;
            }
            else
            {
                _stoped = false;
                _timerReset = 0;
            }
        }
        if (_stoped)
        {
            _timerReset += Time.deltaTime;
            if (_timerReset >= _resetWaitTime)
            {
                _stoped = false;
                _timerReset = 0;
                ResetCar();
            }
        }
    }

    /// <summary>
    /// Efectos de las ruedas. Huellas.
    /// </summary>
    private void WheelEffects()
    {
        for (int i = 0; i < wheelColliderList.Length; i++)
        {
            var wheelEffect = wheelColliderList[i].GetComponent<WheelEffects>();
            if (_localCurrentSpeed * K.KPH_TO_MPS_MULTIPLIER > 50 && isGrounded && (_steerInput > .5f || _steerInput < -.5f))
            {
                if (isGrounded)
                {
                    wheelEffect.Skid();
                }
                continue;
            }
            wheelEffect.EndSkidTrail();
        }
    }

    public void EngineSound()
    {
        //GEARS

        for (var i = 0; i < totalGears.Count; i++)
        {
            if (totalGears[i] > currentSpeed) break;
            float minGearValue;
            float maxGearValue;
            if (i == 0) minGearValue = 0;
            else minGearValue = totalGears[i - 1];
            maxGearValue = totalGears[i];
            var enginePitch = ((currentSpeed - minGearValue) / (maxGearValue - minGearValue)) + 1;
            engineSound.pitch = enginePitch;
        }


        //ONE GEAR
 /*       var currVelZ = transform.InverseTransformDirection(_rb.velocity).z;
        var enginePitch = (currVelZ / 25) + 1;
        engineSound.pitch = enginePitch;*/

    }
    private void CheckDustVehicle()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SCENES_NUMBER.DesertTrack && !hasEnded)
        {
            if (_localCurrentSpeed > 10 && isGrounded || _localCurrentSpeed < -5 && isGrounded) backDust.Play();
            else backDust.Stop();
        }
    }

    /// <summary>
    /// Actualizacion visual de las ruedas. Rotacion y giro.
    /// </summary>
    protected void WheelsRotation()
    {
        if (vehicleVars.vehicleType == VehicleVars.Type.Alien)
        {
            for (int i = 0; i < wheelMeshList.Length; i++)
            {
                var rpm = (60 * _localCurrentSpeed) / (Mathf.PI * (wheelMeshList[i].GetComponent<MeshRenderer>().bounds.extents.y * 2));
                wheelMeshList[i].localEulerAngles = wheelMeshList[i].localEulerAngles + (Vector3.up * rpm);
            }
        }

        else
        {
            for (int i = 0; i < wheelMeshList.Length; i++)
            {
                Quaternion quat;
                Vector3 pos;
                wheelColliderList[i].GetWorldPose(out pos, out quat);
                wheelMeshList[i].position = pos;
                wheelMeshList[i].rotation = quat;
            }
        }
    }


    /// <summary>
    /// Caida.
    /// </summary>
    protected void AddDownForce()
    {
        //Vehículo pegado al piso
        if (isGrounded && vehicleVars.stuckToTheFloor)
        {
            for (int i = 0; i < 4; i++) _rb.AddForceAtPosition(-wheelColliderList[i].transform.up * vehicleVars.stuckForceVehicle * _rb.velocity.magnitude, wheelColliderList[i].transform.position);
        }
        //Gravedad
        else if (!isGrounded) _rb.AddForce((-Vector3.up * vehicleVars.downForce), ForceMode.Acceleration);

    }

    /// <summary>
    /// Aceleracion y freno.
    /// </summary>
    /// <param name="forwardForce">Fuerza de aceleracion</param>
    /// <param name="accI">Input de aceleracion</param>
    /// <param name="brakeF">Input de freno</param>
    protected void ApplyDrive()
    {
        // Convierte la velocidad actual a MPH

        currentSpeed = Mathf.Round(_rb.velocity.magnitude * K.MPS_TO_MPH_MULTIPLIER);
        /*if(gameObject.layer == K.LAYER_PLAYER)
            print(currentSpeed);*/
        _localCurrentSpeed = transform.InverseTransformDirection(_rb.velocity).z;
        /*
                // Chequea sí la dirección en el eje Z es negativo(reversa).
                if (_localCurrentSpeed.z < -0.1f) _reversing = true;
                else _reversing = false;*/

        //Chequea condiciones y aplica una fuerza al vehículo según que tracción tenga.
        if (currentSpeed < vehicleVars.topSpeed && _motorInput > 0 && isGrounded || currentSpeed < vehicleVars.topReverseSpeed && _motorInput < 0 && isGrounded)
        {
            for (int i = 0; i <= 3; i++) wheelColliderList[i].brakeTorque = 0;
            if (vehicleVars.wheelDriveType == WheelDriveType.RWD) for (int i = 2; i <= 3; i++) wheelColliderList[i].motorTorque = vehicleVars.torque * _motorInput;
            else if (vehicleVars.wheelDriveType == WheelDriveType.FWD) for (int i = 0; i <= 1; i++) wheelColliderList[i].motorTorque = vehicleVars.torque * _motorInput;
            else if (vehicleVars.wheelDriveType == WheelDriveType.AWD) for (int i = 0; i <= 3; i++) wheelColliderList[i].motorTorque = vehicleVars.torque * _motorInput;
        }
        // Aplica un freno suave.
        else if (Input.GetKey(KeyCode.LeftShift) == false && (Input.GetKey(KeyCode.Space) == false))
        {
            for (int i = 0; i <= 3; i++)
            {
                wheelColliderList[i].brakeTorque = vehicleVars.brakeForce;
                wheelColliderList[i].motorTorque = 0;
            }
        }

        //////////////////////

        //Calcula el ángulo de giro que debe usar teniendo en cuenta sú maximo y mínimo y la velocidad actual del vehículo.
        float currentSteerAngle = EvaluateSpeedToTurn(_rb.velocity.magnitude * K.MPS_TO_MPH_MULTIPLIER) * _steerInput;
        for (int i = 0; i < 2; i++) wheelColliderList[i].steerAngle = currentSteerAngle;

    }

    /// <summary>
    /// Giro del vehiculo.
    /// </summary>
    /// <param name="relativeVelocity">Velocidad relativa del vehiculo</param>

    private float EvaluateSpeedToTurn(float speed)
    {
        if (speed > vehicleVars.topSpeed / 2) return vehicleVars.minSteerAngle;
        var speedIndex = 1 - (speed / (vehicleVars.topSpeed / 2));

        return vehicleVars.minSteerAngle + speedIndex * (vehicleVars.maxSteerAngle - vehicleVars.minSteerAngle);
    }

    /// <summary>
    /// Vuelve a posicionar el vehiculo si queda trabado.
    /// </summary>
    public virtual void ResetCar()
    {
        if (_lastCheckpoint == null) return;

        _canAdjustVelocityZ = false;
        _lastGroundedVelocityZ = 0;
        Vector3 randomPoint = new Vector3(UnityEngine.Random.Range(_lastCheckpoint.minRandom, _lastCheckpoint.maxRandom), 0, UnityEngine.Random.Range(_lastCheckpoint.minRandom, _lastCheckpoint.maxRandom));
        randomPoint = _lastCheckpoint.transform.TransformPoint(randomPoint * 0.5f);

        var temp = Physics.RaycastAll(randomPoint + Vector3.up * 200, -_lastCheckpoint.transform.up, Mathf.Infinity);
        foreach (var item in temp)
        {
            if (item.collider.gameObject.layer == K.LAYER_GROUND)
            {
                //var pointRes = _lastCheckpoint.GetRespawnPoint();
                transform.position = item.point + Vector3.up * 3;

                //PARCHE PARA CITYTRACK(1er ATAJO)
                if (SceneManager.GetActiveScene().buildIndex == (int)SCENES_NUMBER.RushHour) transform.position = new Vector3(item.point.x, _lastCheckpoint.transform.position.y, item.point.z);
                else transform.position = item.point + Vector3.up * 3;
                transform.forward = _lastCheckpoint.transform.forward;
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
                return;
            }
        }
    }

    protected virtual void NitroInput(float nitroInput, float motorInput)
    {
        if (nitroInput > 0 && (isGrounded || _isGroundedRamp) && !_nitroEmpty) _modeNitro = true;
        else _modeNitro = false;

   /*     if (_modeNitro)
        {
            if (_prevTorque == 0)
            {
                _prevTorque = vehicleVars.torque;
                _prevWheelDriveType = vehicleVars.wheelDriveType;
            }
            vehicleVars.torque = vehicleVars.nitroPower;
            vehicleVars.wheelDriveType = WheelDriveType.AWD;

            _nitroTimer -= Time.deltaTime;

            if (_nitroTimer < 0)
            {
                _modeNitro = false;
                _nitroEnd = true;
                _nitroEmpty = true;
            }
        }
        else if (_prevTorque != 0)
        {
            vehicleVars.torque = _prevTorque;
            _prevTorque = 0;
            vehicleVars.wheelDriveType = _prevWheelDriveType;
            _prevWheelDriveType = new WheelDriveType();

            for (int i = 0; i <= 3; i++)
            {
                wheelColliderList[i].brakeTorque = vehicleVars.brakeForce;
                wheelColliderList[i].motorTorque = 0;
            }
        }*/

        if (_modeNitro)
        {

            if (motorInput < 0) _rb.AddRelativeForce(0, 0, -vehicleVars.nitroPower, ForceMode.Acceleration);
            else _rb.AddRelativeForce(0, 0, vehicleVars.nitroPower, ForceMode.Acceleration);
            _nitroTimer -= Time.deltaTime;

            if (_nitroTimer < 0)
            {
                _modeNitro = false;
                _nitroEnd = true;
                _nitroEmpty = true;
            }
        }

    }

    public void PushRamp(float amount)
    {
        if (_playerData.racerList[_playerData.selectedRacer].racerClass.classType == Classes.Type.Runner &&
            _playerData.racerList[_playerData.selectedRacer].unlockedTierTwo == Classes.TypeTierTwo.Oportunist)
        {
            amount += (amount * .2f);
        }
        _rb.AddRelativeForce(0, 0, amount);
        //_modeNitro = false;
    }

    protected void CheckIfGrounded()
    {
        foreach (var wheelCollider in wheelColliderList)
        {
            RaycastHit hit;
            if (Physics.Raycast(wheelCollider.transform.position, -transform.up, out hit, 0.5f + wheelCollider.radius))
            {
                if (hit.collider.gameObject.layer == K.LAYER_RAMP || hit.collider.gameObject.layer == K.LAYER_OBSTACLE)_isGroundedRamp = true;
                else _isGroundedRamp = false;

                if (hit.collider.gameObject.layer == K.LAYER_GROUND) isGrounded = true;
                else isGrounded = false;
            }

            else
            {
                isGrounded = false;
                _isGroundedRamp = false;
            }
        }
    }

    protected virtual void OnCollisionEnter(Collision hit)
    {
        if ((hit.gameObject.layer == K.LAYER_GROUND ||
            hit.gameObject.layer == K.LAYER_RAMP || hit.gameObject.layer == K.LAYER_OBSTACLE))
        {
            _canForceRespawn = true;
        }
    }

    public void OnOil()
    {
        _oilEffect = true;
    }

    public void OutOil(float duration)
    {
        Invoke("BackNormal", duration);
    }

    private void BackNormal() { _oilEffect = false; }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_rb) Gizmos.DrawSphere(_rb.worldCenterOfMass, .5f);
    }

}
