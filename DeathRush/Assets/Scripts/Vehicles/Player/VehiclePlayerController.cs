using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System;

public class VehiclePlayerController : Vehicle
{
    public GameObject visualNitro;
    [HideInInspector]
    public Image visualNitroBar;
    [HideInInspector]
    public Text wrongDirectionText;

    private Bloom _cameraBloom;
    private VignetteAndChromaticAberration _cameraViggneteAndChromaticAberration;
    private MotionBlur _cameraMotionBlur;
    private float _chargedAmount;
    private RacerData _racerData;
    private WheelFrictionCurve _wfc;
    //Prueba
    public Text debugInput;
    //Prueba

    protected override void Start()
    {
        visualNitroBar = GameObject.FindGameObjectWithTag(K.TAG_VISUAL_NITRO).GetComponent<Image>();
        wrongDirectionText = GameObject.FindGameObjectWithTag(K.TAG_WRONGDIRECTION_TEXT).GetComponent<Text>();
        wrongDirectionText.gameObject.SetActive(false);
        base.Start();
        _racerData = _playerData.racerList[_playerData.selectedRacer];
        vehicleName = _racerData.racerName;
        _cameraBloom = Camera.main.GetComponent<Bloom>();
        _cameraViggneteAndChromaticAberration = Camera.main.GetComponent<VignetteAndChromaticAberration>();
        _cameraMotionBlur = Camera.main.GetComponent<MotionBlur>();
        _wfc = new WheelFrictionCurve();
        Init();
    }

    private void Init()
    {
        _racerData.racerClass.Execute(this, _racerData.unlockedTierOne, _racerData.unlockedTierTwo, _racerData.unlockedTierThree);
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.NextGenEngine)) vehicleVars.topSpeed += (vehicleVars.topSpeed * .1f);
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.ExperimentalFuel)) vehicleVars.nitroTimer += (vehicleVars.nitroTimer * .25f);
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.NuclearIgnition)) vehicleVars.torque += (vehicleVars.torque * .2f);
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.HybridWeapons))
            foreach (var weapon in weaponManager.GetComponentsInChildren<Weapon>())
            {
                weapon.damage += (weapon.damage * .1f);
            }
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.NewAlloys))
            if (!_racerData.newAlloysUsed)
            {
                _racerData.newAlloysUsed = true;
                var vd = GetComponent<VehicleData>();
                vd.maxLife += (vd.maxLife * .2f);
                vd.currentLife += (vd.currentLife * .2f);
            }
    }
    public override void Move(float motorInput, float handbrakeInput, float steerInput, float nitroInput)
    {
        base.Move(motorInput, handbrakeInput, steerInput, nitroInput);

        //Derrape
        Drift();

        if (!isGrounded) transform.RotateAround(transform.position, transform.up, _steerInput);
      //  if (!isGrounded) transform.RotateAround(transform.position, transform.right, _motorInput);
    }

    protected override void ApplyHandbrake()
    {
        base.ApplyHandbrake();

        if (Input.GetKey(KeyCode.Space) || _motorInput < 0 && _localCurrentSpeed > 0 || _motorInput > 0 && _localCurrentSpeed < 0)
        {
            for (int i = 0; i <= 3; i++)
            {
                wheelColliderList[i].brakeTorque = vehicleVars.handBrakeForce;
                wheelColliderList[i].motorTorque = 0;
            }
        }
    }

    private void Drift()
    {
        if (_steerInput > 0 || _steerInput < 0)
        {
            for (int i = 2; i < 4; i++)
            {
                _wfc = wheelColliderList[i].sidewaysFriction;

                var driftRight = Mathf.Lerp(vehicleVars.maxSidewaysFriction, vehicleVars.minSidewaysFriction, _steerInput);
                var driftLeft = Mathf.Lerp(vehicleVars.maxSidewaysFriction, vehicleVars.minSidewaysFriction, -_steerInput);


                if (_steerInput > 0) _wfc.stiffness = driftRight;
                if (_steerInput < 0) _wfc.stiffness = driftLeft;

                wheelColliderList[i].sidewaysFriction = _wfc;
            }
        }
    }

    public override void SetCheckpoint(Checkpoint chk)
    {
        base.SetCheckpoint(chk);
        LapCount();
    }

    protected override void Update()
    {
        base.Update();
        CheckBars();
        CheckDirection();
        CheckShakeCamera();
        if (Input.GetKeyDown(KeyCode.R) && _canForceRespawn) ResetCar();

        //PRUEBA
        if (Input.GetKeyUp(KeyCode.F1))
        {
            //CheckBars();
            _canRechargeNitro = true;
            RechargeNitro();
        }

        if (Input.GetKeyUp(KeyCode.F2))
        {
            GetComponent<PlayerVehicleData>().currentLife = GetComponent<PlayerVehicleData>().maxLife;
            GetComponent<PlayerVehicleData>().CheckHealthBar(true);
        }

        if (Input.GetKeyUp(KeyCode.F3))
        {
            GetComponent<PlayerVehicleData>().minerPerk = !GetComponent<PlayerVehicleData>().minerPerk;
            if (GetComponent<PlayerVehicleData>().minerPerk == true)
            {
                debugInput.text = "F3: Activar Miner Perk. Daño de minas reducido";
            }
            else
            {
                debugInput.text = "F3: Desactivar Miner Perk. Daño de minas normalizado";
            }

        }

        if (Input.GetKeyUp(KeyCode.F4))
        {
            GetComponent<PlayerVehicleData>().knightPerk = !GetComponent<PlayerVehicleData>().knightPerk;
            if (GetComponent<PlayerVehicleData>().knightPerk == true)
            {
                debugInput.text = "F4: Activar Knight Perk. Más vida, menos velocidad";
                GetComponent<PlayerVehicleData>().maxLife = GetComponent<PlayerVehicleData>().maxLife * 2;
                GetComponent<PlayerVehicleData>().currentLife = GetComponent<PlayerVehicleData>().currentLife * 2;
                vehicleVars.topSpeed = 120;
            }
            else
            {
                debugInput.text = "F4: Desactivado Knight perk. Vida y velocidad normal";
                GetComponent<PlayerVehicleData>().maxLife = GetComponent<PlayerVehicleData>().maxLife / 2;
                GetComponent<PlayerVehicleData>().currentLife = GetComponent<PlayerVehicleData>().currentLife / 2;
                vehicleVars.topSpeed = 180;
            }

        }

        if (Input.GetKeyUp(KeyCode.F5))
        {
            GetComponent<PlayerVehicleData>().riskyPerk = !GetComponent<PlayerVehicleData>().riskyPerk;
            if (GetComponent<PlayerVehicleData>().riskyPerk == true)
            {
                debugInput.text = "F5: Activar Risky Perk. Menos vida, Más velocidad";
                //GetComponent<BuggyData>().maxLife = GetComponent<BuggyData>().maxLife * 0.5f;
                //GetComponent<BuggyData>().currentLife = GetComponent<BuggyData>().maxLife;
                vehicleVars.topSpeed = 200;
                vehicleVars.downForce = 50;
            }
            else
            {
                debugInput.text = "F5: Desactivado Risky perk. Vida y velocidad normal";
                // GetComponent<BuggyData>().maxLife = GetComponent<BuggyData>().maxLife * 2;
                //GetComponent<BuggyData>().currentLife = GetComponent<BuggyData>().currentLife = GetComponent<BuggyData>().maxLife;
                vehicleVars.topSpeed = 180;
                vehicleVars.downForce = 200;
            }

        }
        if (Input.GetKeyUp(KeyCode.F6))
        {
            GetComponent<PlayerVehicleData>().spikesPerk = !GetComponent<PlayerVehicleData>().spikesPerk;
            if (GetComponent<PlayerVehicleData>().spikesPerk == true)
            {
                debugInput.text = "F6: Activar Spikes Perk. Daño al chocar";
            }
            else
            {
                debugInput.text = "F6: Desactivado Spikes Perk. Ya no daña al chocar";
            }
        }
    }

    private void CheckShakeCamera()
    {
        if (_canShakeCamera && isGrounded)
        {
            _canShakeCamera = false;
            Camera.main.GetComponent<ShakeCamera>().DoShake();
        }
    }

    protected override void OnCollisionEnter(Collision c)
    {
        base.OnCollisionEnter(c);
        if (isCrasher && c.gameObject.layer == K.LAYER_IA)
        {
            c.gameObject.GetComponent<VehicleData>().currentLife -= 20;
        }
    }

    private void CheckBars()
    {
        CheckNitroBar();
        RechargeNitro();
    }


    private void CheckNitroBar()
    {
        float calc_nitro = _nitroTimer / vehicleVars.nitroTimer;
        visualNitroBar.fillAmount = calc_nitro;
        visualNitro.GetComponent<Renderer>().material.SetFloat("_ReduceLiquid", calc_nitro);
    }

    protected override void NitroInput(float nitroInput, float brakeInput)
    {
        base.NitroInput(nitroInput, brakeInput);
        if (_modeNitro)
        {
            _cameraBloom.enabled = true;
            _cameraViggneteAndChromaticAberration.enabled = true;
            _cameraMotionBlur.enabled = true;
        }
        else
        {
            _cameraBloom.enabled = false;
            _cameraViggneteAndChromaticAberration.enabled = false;
            _cameraMotionBlur.enabled = false;
        }
    }

    public void RechargeNitro()
    {
        if (!_modeNitro && _nitroTimer < vehicleVars.nitroTimer && _canRechargeNitro)
        {
            _nitroTimer += Time.deltaTime * 16f / vehicleVars.rechargeNitro;
            _chargedAmount += Time.deltaTime * 16f / vehicleVars.rechargeNitro;
        }
        if (_nitroTimer > 0) _nitroEmpty = false;
        if (visualNitroBar.fillAmount == 1 || _chargedAmount >= vehicleVars.nitroTimer)
        {
            _canRechargeNitro = false;
            _nitroEmpty = false;
            _chargedAmount = 0;
        }
    }

    /// <summary>
    /// Checkea si el jugador va en la direccion que deberia y le avisa si no lo hace.
    /// </summary>
    private void CheckDirection()
    {
        if (_lastCheckpoint)
        {
            var currentDirection = _lastCheckpoint.nextCheckpoint.transform.position - transform.position;
            if (Vector3.Angle(transform.forward, currentDirection) > 80)
            {
                _timerWrongDirection += Time.deltaTime;
            }
            else
            {
                wrongDirectionText.gameObject.SetActive(false);
                _timerWrongDirection = 0;
            }
        }
        if (_timerWrongDirection > 2)
        {
            wrongDirectionText.gameObject.SetActive(true);
        }
    }
    public void EndRaceHandbrake()
    {
        for (int i = 0; i <= 3; i++)
        {
            wheelColliderList[i].brakeTorque = 2000;
            wheelColliderList[i].motorTorque = 0;
        }

        backDust.Stop();
    }
}
