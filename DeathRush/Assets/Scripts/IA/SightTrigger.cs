using UnityEngine;
using System.Collections;

public class SightTrigger : MonoBehaviour
{
    private IAVehicleData myController;

	// Use this for initialization
	void Start ()
    {
        myController = this.GetComponentInParent<IAVehicleData>();
	
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider col)
    {
        if (myController != col.GetComponentInParent<IAVehicleData>())
        {
            if (col.gameObject.GetComponentInParent<Vehicle>() != null)
                myController.EnemySee(col.transform.position);
        }
    }
}
