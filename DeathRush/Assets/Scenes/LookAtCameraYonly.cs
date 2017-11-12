using UnityEngine;
using System.Collections;
public class LookAtCameraYonly : MonoBehaviour
{
    private Quaternion _rotation;
    void Start()
    {
        _rotation = transform.localRotation;
    }

    void Update()
    {
        Vector3 v = Camera.main.transform.position - transform.position;
        v.x = v.z = 0f;
        transform.LookAt(Camera.main.transform.position - v);
        transform.Rotate(0, 180, 0);

      //  transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,Camera.main.transform.rotation * Vector3.up);
    }
}