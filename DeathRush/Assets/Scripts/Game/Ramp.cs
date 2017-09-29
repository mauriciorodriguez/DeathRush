using UnityEngine;
using System.Collections;

public class Ramp : MonoBehaviour
{
    public float forceRamp;
    public bool ignoreSpeed;
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == K.LAYER_PLAYER || other.gameObject.layer == K.LAYER_IA)
        {
            if (ignoreSpeed) other.gameObject.GetComponentInParent<Vehicle>().PushRamp(forceRamp);
            else if (other.gameObject.GetComponentInParent<Vehicle>().currentSpeed * K.MPS_TO_MPH_MULTIPLIER > 20f) other.gameObject.GetComponentInParent<Vehicle>().PushRamp(forceRamp);
        }


    }
}
