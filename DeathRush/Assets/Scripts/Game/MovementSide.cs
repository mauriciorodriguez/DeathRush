using UnityEngine;
using System.Collections;
public class MovementSide : MonoBehaviour
{
    protected Rigidbody _rb;
    public Transform waypointStart, waypointEnd;
    public float speed;
    private Vector3 direction;
    private Transform destination;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
        SetDestination(waypointStart);
    }
    private void Update()
    {
        _rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destination.position) < speed * Time.deltaTime)
            SetDestination(destination == waypointStart ? waypointEnd : waypointStart);
    }
    private void SetDestination(Transform dest)
    {
        destination = dest;
        direction = (destination.position - transform.position).normalized;
    }
}
