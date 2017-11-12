using UnityEngine;
using System.Collections;

public class Spinning : MonoBehaviour
{
    public float speed = 1f;

	void Update ()
    {
        transform.Rotate(0, speed *  Time.deltaTime, 0);

         //   transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
    }

    public void RotateToTrack(int num)
    {
        if (num == 1) transform.localEulerAngles = new Vector3(0, -153f,0);
        if (num == 2) transform.localEulerAngles = new Vector3(0, -18.863f, 0);
        if (num == 3) transform.localEulerAngles = new Vector3(0, 9.95f, 0);
        if (num == 4) transform.localEulerAngles = new Vector3(0, -82.846f, 0);
        if (num == 5) transform.localEulerAngles = new Vector3(0, -127.526f, 0);
    }
}
