using UnityEngine;
using System.Collections;

public class OrbitAround : MonoBehaviour
{
    public Transform centerOfSatellite;
    public Vector3 dirRotation;
    public float speed;
	void Start ()
    {
	
	}
    void LateUpdate()
    {
        Orbit();
    }
    void Orbit()
    {
        if (centerOfSatellite != null) transform.RotateAround(centerOfSatellite.transform.position, dirRotation, speed * Time.deltaTime);
    }
}
