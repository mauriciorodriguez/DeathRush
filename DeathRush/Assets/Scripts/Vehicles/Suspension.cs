using UnityEngine;
using System.Collections;

public class Suspension : MonoBehaviour
{
    public float springConstant, damperConstant, restLenght;

    private float _previousLength, _currentLength, _springVelocity, _springForce, _damperForce, _wheelRadius;
    private Rigidbody _rb;
    private bool _isGrounded;
    private bool _isGroundedRamp;

    private void Start()
    {
        _isGrounded = false;
        _rb = transform.parent.parent.parent.GetComponent<Rigidbody>();
        _wheelRadius = GetComponentInChildren<MeshRenderer>().bounds.extents.y;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, restLenght + _wheelRadius))
        {

            _previousLength = _currentLength;
            _currentLength = restLenght - (hit.distance - _wheelRadius);
            _springVelocity = (_currentLength - _previousLength) / Time.fixedDeltaTime;
            _springForce = springConstant * _currentLength;
            _damperForce = damperConstant * _springVelocity;
            _rb.AddForceAtPosition(transform.up * (_springForce + _damperForce), transform.position);
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

    public bool IsGrounded()
    {
        return _isGrounded;
    }

    public bool IsGroundedRamp()
    {
        return _isGroundedRamp;
    }

    /// <summary>
    /// Obtencion del radio de la rueda.
    /// </summary>
    /// <returns>Radio de la rueda</returns>
    public float GetWheelRadius()
    {
        return _wheelRadius;
    }

    public float GetCurrentSuspensionLenght()
    {
        return _currentLength;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (restLenght + _wheelRadius) * -Vector3.up);
    }
}
