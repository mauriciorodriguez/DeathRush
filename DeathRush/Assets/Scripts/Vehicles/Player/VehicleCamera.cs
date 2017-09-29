using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class VehicleCamera : MonoBehaviour
{
    //public RawImage crosshair;
    public Transform target = null;
    //private float _height;
    //private float _distanceHeight;
    private Rigidbody _rbTarget;
    //public float rotationDamping = 3f;
    public float minFOV = 50f;
    public float maxFOVGround = 70f;
    public float maxFOVAir = 90f;
    //private Vector3 _crosshairFixedZPostion;
    private float _maxFOV;

    public bool rotateCamera;
    public void Init(Transform target)
    {
        this.target = target;
        _rbTarget = target.GetComponent<Rigidbody>();
    }

    /*private void Update()
    {
        _crosshairFixedZPostion.x = Input.mousePosition.x;
        _crosshairFixedZPostion.y = Input.mousePosition.y;
        crosshair.transform.position = _crosshairFixedZPostion;
    }*/
    void LateUpdate()
    {
        if (!target || !_rbTarget) return;

        float speed = (_rbTarget.transform.InverseTransformDirection(_rbTarget.velocity).z) * K.MPS_TO_MPH_MULTIPLIER;

        //float speedFactor = Mathf.Clamp01(_rbTarget.velocity.magnitude / 70);
        float speedFactor = Mathf.Clamp01(speed / target.GetComponent<Vehicle>().vehicleVars.topSpeed);

        Camera.main.fieldOfView = Mathf.Lerp(minFOV, CalculateMaxFov(), speedFactor);

        if (rotateCamera) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, target.localEulerAngles.z);
        else if (transform.localEulerAngles.z != 0) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

    private float CalculateMaxFov()
    {
        if (target.GetComponent<Vehicle>().isGrounded)
        {
            if (_maxFOV > maxFOVGround)
            {
                _maxFOV--;
            }
            else
            {
                _maxFOV++;
            }
            _maxFOV = Mathf.Clamp(_maxFOV, minFOV, maxFOVGround);
        }
        else
        {
            if (_maxFOV > maxFOVAir)
            {
                _maxFOV--;
            }
            else
            {
                _maxFOV++;
            }
            _maxFOV = Mathf.Clamp(_maxFOV, minFOV, maxFOVAir);
        }
        return _maxFOV;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (target != null)
            Gizmos.DrawLine(transform.position, target.transform.position + transform.up * 3);
    }
}
