using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MolotovNPCs : MonoBehaviour
{
    private Rigidbody _rb;
    public float viewAngle;
    public float viewDistance;
    public bool targetInSight;
    private Vector3 _dirToTarget;
    private float _angleToTarget;
    private float _distanceToTarget;
    private GameObject _targetReference;
    private NavMeshAgent _agent;
    private float _agentSpeed;
    private bool _defaultValues;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (!_targetReference)
        {
            _targetReference = GameObject.FindGameObjectWithTag("Player");
            if (_targetReference.GetComponent<InputControllerPlayer>().enabled) _targetReference = null;
        }
        if (_targetReference != null && _targetReference.GetComponent<InputControllerPlayer>().enabled)  McInSight();
    }

    void McInSight()
    {
        _dirToTarget = (_targetReference.transform.position + _targetReference.transform.up / 2) - (transform.position + transform.up / 20 + transform.forward / 2);
        _angleToTarget = Vector3.Angle(transform.forward, _dirToTarget);
        _distanceToTarget = Vector3.Distance(transform.position + transform.up / 20 + transform.forward / 2, _targetReference.transform.position + _targetReference.transform.up / 2);

        RaycastHit rch;
        bool obstaclesBetween = false;
        if (Physics.Raycast(transform.position + transform.up * 2.5f + transform.forward * 1f, _dirToTarget, out rch, _distanceToTarget))
        {
            if (rch.collider.gameObject.layer == K.LAYER_OBSTACLE) obstaclesBetween = true;
        }

        if (_angleToTarget <= viewAngle && _distanceToTarget <= viewDistance && !obstaclesBetween) targetInSight = true;

        if (targetInSight)
        {
            _agent.SetDestination(_targetReference.transform.position - transform.forward * 1.5f);
       /*     float xRot = transform.eulerAngles.x;
            transform.LookAt(_targetReference.transform.position);
            transform.eulerAngles = new Vector3(xRot, transform.eulerAngles.y, transform.eulerAngles.z);*/

        }
    }
/*
    void OnDrawGizmos()
    {
        Gizmos.color = targetInSight ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position + transform.up / 20 + transform.forward / 2, _targetReference.transform.position + _targetReference.transform.up / 2);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * viewDistance));

        Vector3 rightLimit = Quaternion.AngleAxis(viewAngle, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (rightLimit * viewDistance));

        Vector3 leftLimit = Quaternion.AngleAxis(-viewAngle, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (leftLimit * viewDistance));
    }*/
}
