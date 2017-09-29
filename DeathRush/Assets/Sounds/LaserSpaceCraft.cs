using UnityEngine;
using System.Collections;

public class LaserSpaceCraft : MonoBehaviour 
{

    public float damage;
	void Start ()
    {
	
	}
	
	void Update () 
    {
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponentInParent<VehicleIAController>() != null)
        {
            var iaVehicleData = col.GetComponentInParent<IAVehicleData>();
            iaVehicleData.Damage(damage, iaVehicleData);           
        }
        else if (col.GetComponentInParent<VehiclePlayerController>() != null)
        {
            var playerVehicleData = col.GetComponentInParent<PlayerVehicleData>();
            playerVehicleData.Damage(damage, playerVehicleData);  
        }
    }
}
