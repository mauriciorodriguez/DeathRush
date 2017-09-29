using UnityEngine;
using System.Collections;

public class SpikesDamage : MonoBehaviour
{

    public float damage;
    void Start()
    {

    }

    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.layer == K.LAYER_IA)
        {
            if (c.GetComponentInParent<IAVehicleData>() != null)
            {
                var vehicleData = c.GetComponentInParent<IAVehicleData>();
                c.GetComponentInParent<IAVehicleData>().Damage(damage, vehicleData);
            }
        }

        else if (c.gameObject.layer == K.LAYER_PLAYER)
        {
            if (c.GetComponentInParent<PlayerVehicleData>() != null)
            {
                var vehicleData = c.GetComponentInParent<PlayerVehicleData>();
                c.GetComponentInParent<PlayerVehicleData>().Damage(damage, vehicleData);
            }
        }
    }
}
