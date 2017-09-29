using UnityEngine;
using System.Collections;

public class Deathzone : MonoBehaviour {

    void OnTriggerEnter(Collider c)
    {

       if (c.gameObject.layer == K.LAYER_IA)
       {
           if (c.GetComponentInParent<IAVehicleData>() != null)
           {
               var vehicleData = c.GetComponentInParent<IAVehicleData>();
               c.GetComponentInParent<IAVehicleData>().Damage(vehicleData.currentLife, vehicleData);
           }
       }

       else if (c.gameObject.layer == K.LAYER_PLAYER)
       {
           if (c.GetComponentInParent<VehicleData>() != null)
           {
               var vehicleData = c.GetComponentInParent<VehicleData>();
               c.GetComponentInParent<VehicleData>().Damage(vehicleData.currentLife, vehicleData);
           }
       }


    }
}
