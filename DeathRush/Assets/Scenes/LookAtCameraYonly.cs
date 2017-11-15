using UnityEngine;
using System.Collections;
public class LookAtCameraYonly : MonoBehaviour
{
    private Vector3 _rotation;
    void Start()
    {
        _rotation = transform.eulerAngles;
    }

    void Update()
    {
        Vector3 v = Camera.main.transform.position - transform.position;
        v.x = v.z = 0f;
        transform.LookAt(Camera.main.transform.position - v);
        transform.Rotate(_rotation.x, 180, _rotation.z);

      //  transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,Camera.main.transform.rotation * Vector3.up);
    }
}