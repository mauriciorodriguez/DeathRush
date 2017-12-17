using UnityEngine;
using System.Collections;

public class HurtZone : MonoBehaviour
{

    public float damage;
    void OnTriggerEnter(Collider c)
    {
       if (c.gameObject.layer == K.LAYER_IA)
       {
           if (c.GetComponentInParent<IAVehicleData>() != null)
           {
               var vehicleData = c.GetComponentInParent<IAVehicleData>();
               c.GetComponentInParent<IAVehicleData>().Damage(damage, vehicleData);
               c.GetComponentInParent<VehicleIAController>().ResetCar();
           }
       }

       if (c.gameObject.layer == K.LAYER_PLAYER)
       {
           if (c.GetComponentInParent<VehicleData>() != null)
           {
               var vehicleData = c.GetComponentInParent<VehicleData>();
               c.GetComponentInParent<VehicleData>().Damage(damage, vehicleData);
               c.GetComponentInParent<VehiclePlayerController>().ResetCar();
            }
       }


    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.layer == K.LAYER_IA)
        {
            if (c.GetComponentInParent<IAVehicleData>() != null)
            {
                var vehicleData = c.GetComponentInParent<IAVehicleData>();
                c.GetComponentInParent<IAVehicleData>().Damage(damage, vehicleData);
                c.GetComponentInParent<VehicleIAController>().ResetCar();
            }
        }

        else if (c.gameObject.layer == K.LAYER_PLAYER)
        {
            if (c.GetComponentInParent<VehicleData>() != null)
            {
                var vehicleData = c.GetComponentInParent<VehicleData>();
                c.GetComponentInParent<VehicleData>().Damage(damage, vehicleData);
                c.GetComponentInParent<VehiclePlayerController>().ResetCar();
            }
        }


    }
}
