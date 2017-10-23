using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenu : MonoBehaviour
{
    public float speed = .1f;
    public Transform searchRaceMount, upgradesMount, chaosMapMount, hubMount, hirePilotMount, garageMount, racerInfoMount;
    private Transform currentMount;

    private void Update()
    {
        if (currentMount == null) return;
        transform.position = Vector3.Lerp(transform.position, currentMount.position, speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speed);
    }

    public void setMount(Transform newMount)
    {
        currentMount = newMount;
    }
}
