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

    public void RotateHologramToTrack(int num)
    {
         if (num == 1) transform.localEulerAngles = new Vector3(0, -153f,0);
         if (num == 2) transform.localEulerAngles = new Vector3(0, -18.863f, 0);
         if (num == 3) transform.localEulerAngles = new Vector3(0, 9.95f, 0);
         if (num == 4) transform.localEulerAngles = new Vector3(0, -82.846f, 0);
         if (num == 5) transform.localEulerAngles = new Vector3(0, -127.526f, 0);
    }
}
