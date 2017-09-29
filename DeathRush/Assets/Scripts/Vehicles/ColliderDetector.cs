using UnityEngine;
using System.Collections;

public class ColliderDetector : MonoBehaviour {


    public VehiclePlayerController player;

    void OnCollission(Collision c)
    {
       print("ENTER COLLISION");
        if (player.GetComponent<PlayerVehicleData>().spikesPerk == true)
        {
            if (c.gameObject.layer == K.LAYER_IA)
            {
//                c.gameObject.GetComponentInParent<IAVehicleData>().Damage(20); //Check If is Used
                print("Daño al chocar");
            }
        }


    }
}
