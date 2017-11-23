using UnityEngine;
using System.Collections;

public class Spinning : MonoBehaviour
{
    public enum RotationAxes { RotX = 0, RotY = 1, RotZ = 2 }
    public RotationAxes rotationXYZ = RotationAxes.RotX;

    public float speed = 1f;

    void Update ()
    {
        if (rotationXYZ == RotationAxes.RotX) transform.Rotate(speed * Time.deltaTime, 0, 0);
        else if (rotationXYZ == RotationAxes.RotY) transform.Rotate(0, speed *  Time.deltaTime, 0);
        else if (rotationXYZ == RotationAxes.RotZ) transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
