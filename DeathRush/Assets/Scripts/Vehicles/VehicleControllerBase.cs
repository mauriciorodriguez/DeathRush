using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ASDWheelDriveType
{
    RWD, //REAR WHEEL DRIVE
    FWD, //FRONT WHEEL DRIVE
    AWD //ALL WHEEL DRIVE
};
public class VehicleControllerBase : MonoBehaviour 
{
    public Transform[] wheelMeshList = new Transform[4];
    public WheelCollider[] wheelColliderList = new WheelCollider[4];

    //Tipo de tracción en las ruedas
    public ASDWheelDriveType wheelDriveType = ASDWheelDriveType.RWD;
    private ASDWheelDriveType _prevWheelDriveType;
    public float torque = 1500f;
    private float _prevTorque;
    public float handbrake;
    public float brake;
    public float nitroPower;
    public float minSteerAngle = 10f;
    public float maxSteerAngle = 15f;

    public Vector3 centerOfMass;

    private WheelFrictionCurve wfc;
    private Rigidbody _rb;

    public float maxSpeed;
    public float maxReverseSpeed;
    private bool _reversing;
    private bool _isGrounded;
    private bool _isGroundedRamp;
  //  public float currentSpeed { get; private set; }
    public float currentSpeed;
    private Vector3 _localCurrentSpeed;
    public float downForce;
    public bool stuckToTheFloor;

    //SidewaysFriction: mayor a 0 --> menor deslizamiento
    public float minSidewaysFriction;
    public float maxSidewaysFriction;

    public float _airTimer;
	void Start ()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centerOfMass;
        wfc = new WheelFrictionCurve();
	}

    void Update()
    {
     //   UpdateTiresPosition();

        CheckIfGrounded();

        SetAirRotation();
    }

    
    void FixedUpdate()
    {
        ApplyDrive();

        //Derrape
        Drift();
    }

    private void Drift()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            for (int i = 2; i < 4; i++)
            {
                wfc = wheelColliderList[i].sidewaysFriction;

                var driftRight = Mathf.Lerp(maxSidewaysFriction, minSidewaysFriction, Input.GetAxis("Horizontal"));
                var driftLeft = Mathf.Lerp(maxSidewaysFriction, minSidewaysFriction, -Input.GetAxis("Horizontal"));


                if (Input.GetAxis("Horizontal") > 0) wfc.stiffness = driftRight;
                if (Input.GetAxis("Horizontal") < 0) wfc.stiffness = driftLeft;

                wheelColliderList[i].sidewaysFriction = wfc;
            }
        }
    }

    private void ApplyDrive()
    {
        // Convierte la velocidad actual a MPH
        currentSpeed = Mathf.Round(_rb.velocity.magnitude * 2.23693629f);

        _localCurrentSpeed = transform.InverseTransformDirection(_rb.velocity);
/*
        // Chequea sí la dirección en el eje Z es negativo(reversa).
        if (_localCurrentSpeed.z < -0.1f) _reversing = true;
        else _reversing = false;*/

        //Chequea condiciones y aplica una fuerza al vehículo según que tracción tenga.
        if (currentSpeed < maxSpeed && Input.GetAxis("Vertical") > 0 && _isGrounded || currentSpeed < maxReverseSpeed && Input.GetAxis("Vertical") < 0 && _isGrounded)
        {
            for (int i = 0; i <= 3; i++) wheelColliderList[i].brakeTorque = 0;
            if (wheelDriveType == ASDWheelDriveType.RWD) for (int i = 2; i <= 3; i++) wheelColliderList[i].motorTorque = torque * Input.GetAxis("Vertical");
            else if (wheelDriveType == ASDWheelDriveType.FWD) for (int i = 0; i <= 1; i++) wheelColliderList[i].motorTorque = torque * Input.GetAxis("Vertical");
            else if (wheelDriveType == ASDWheelDriveType.AWD) for (int i = 0; i <= 3; i++) wheelColliderList[i].motorTorque = torque * Input.GetAxis("Vertical");
        }
        // Aplica un freno suave.
        else if (Input.GetKey(KeyCode.LeftShift) == false && (Input.GetKey(KeyCode.Space) == false))
        {
            for (int i = 0; i <= 3; i++)
            {
                wheelColliderList[i].brakeTorque = brake;
                wheelColliderList[i].motorTorque = 0;
            }
        }

        ///NITRO

        if (Input.GetKey(KeyCode.LeftShift) && _isGrounded)
        {
            if (_prevTorque == 0)
            {
                _prevTorque = torque;
                _prevWheelDriveType = wheelDriveType;
            }
            torque = nitroPower;
            wheelDriveType = ASDWheelDriveType.AWD;
        }
        else if (_prevTorque != 0)
        {
            torque = _prevTorque;
            _prevTorque = 0;
            wheelDriveType = _prevWheelDriveType;
            _prevWheelDriveType = new ASDWheelDriveType();
        }

        //Freno de mano
        if (Input.GetKey(KeyCode.Space) || Input.GetAxis("Vertical") < 0 && _localCurrentSpeed.z > 0 || Input.GetAxis("Vertical") > 0 && _localCurrentSpeed.z < 0)
        {
            for (int i = 0; i <= 3; i++)
            {
                wheelColliderList[i].brakeTorque = handbrake;
                wheelColliderList[i].motorTorque = 0;
            }
        }

        //////////////////////

        //Calcula el ángulo de giro que debe usar teniendo en cuenta sú maximo y mínimo y la velocidad actual del vehículo.
        float currentSteerAngle = EvaluateSpeedToTurn(_rb.velocity.magnitude * 2.23693629f) * Input.GetAxis("Horizontal");
        for (int i = 0; i < 2; i++) wheelColliderList[i].steerAngle = currentSteerAngle;

        //Aplica una fuerza hacia abajo
        AddDownForce();
    }
    
    private float EvaluateSpeedToTurn(float speed)
    {
        if (speed > maxSpeed / 2) return minSteerAngle;
        var speedIndex = 1 - (speed / (maxSpeed / 2));

        return minSteerAngle + speedIndex * (maxSteerAngle - minSteerAngle);
    }
    private void UpdateTiresPosition()
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

    protected void SetAirRotation()
    {
        if (!_isGroundedRamp && !_isGrounded) _airTimer += Time.deltaTime;
        else _airTimer = 0;
        if (!_isGroundedRamp && !_isGrounded && _airTimer > .5f)
        {
            var desiredRotation = transform.rotation.eulerAngles;
            desiredRotation.x = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(desiredRotation), .3f);
        }

    }

    protected void AddDownForce()
    {
       //Vehículo pegado al piso
       if (_isGrounded && stuckToTheFloor)
       {
           for (int i = 0; i < 4; i++) _rb.AddForceAtPosition(-wheelColliderList[i].transform.up * downForce * _rb.velocity.magnitude, wheelColliderList[i].transform.position);
       }
       //Gravedad
       else _rb.AddForce(-Vector3.up * downForce * _rb.velocity.magnitude);
        
    }
    protected void CheckIfGrounded()
    {
        foreach (var wheelCollider in wheelColliderList)
        {
            RaycastHit hit;
            if (Physics.Raycast(wheelCollider.transform.position, -transform.up, out hit, 0.5f + wheelCollider.radius))
            {
                if (hit.collider.gameObject.layer == K.LAYER_RAMP || hit.collider.gameObject.layer == K.LAYER_OBSTACLE)
                {
                    _isGroundedRamp = true;
                }
                else
                {
                    _isGroundedRamp = false;
                }
                if (hit.collider.gameObject.layer == K.LAYER_GROUND)
                {
                    _isGrounded = true;
                }
                else
                {
                    _isGrounded = false;
                }
            }

            else
            {
                _isGrounded = false;
                _isGroundedRamp = false;
            }
        }
    }


}
