using UnityEngine;
using System.Collections;

public class RepairCharger : MonoBehaviour
{
    public float timer;
    private float _timer;
    private bool fade;
    private Material material;
    public Material hologramMaterial;
    void Awake()
    {
        Material mat = new Material(hologramMaterial);
        material = GetComponent<Renderer>().material = mat;
    }
    void Update()
    {
        if (fade)
        {
            if (material.GetFloat("_OpacityClip") > 0.2f)
            {
                _timer -= Time.deltaTime / 3;
                material.SetFloat("_OpacityClip", _timer);
            }
            else fade = false;
        }
        else
        {
            if (material.GetFloat("_OpacityClip") < 0.6f)
            {
                _timer += Time.deltaTime / 3;
                material.SetFloat("_OpacityClip", _timer);
            }
        }
    }
    void OnTriggerEnter(Collider c)
    {
        //print("Repair");
        if (c.GetComponentInParent<VehicleData>() != null)
        {
            //print("Repair");
            //print(c.GetComponentInParent<PlayerVehicleData>().currentLife);
            //print(c.GetComponent<BuggyData>().currentLife);
            if (c.GetComponentInParent<VehiclePlayerController>() && c.GetComponentInParent<VehiclePlayerController>().canHyperCharge)
            {
                c.GetComponentInParent<VehicleData>().currentLife = c.GetComponentInParent<VehicleData>().maxLife;
                c.GetComponentInParent<VehiclePlayerController>()._canRechargeNitro = true;
                c.GetComponentInParent<VehiclePlayerController>().RechargeNitro();
                c.GetComponentInParent<PlayerVehicleData>().CheckHealthBar(true);
            }
            else if (c.GetComponentInParent<IAVehicleData>())
            {
                c.GetComponentInParent<IAVehicleData>().currentLife = c.GetComponentInParent<IAVehicleData>().currentLife + c.GetComponentInParent<IAVehicleData>().maxLife / 4;
                c.GetComponentInParent<IAVehicleData>().currentLife = Mathf.Clamp(c.GetComponentInParent<IAVehicleData>().currentLife, 0, c.GetComponentInParent<IAVehicleData>().maxLife);
                c.GetComponentInParent<IAVehicleData>().CheckHealthBar(true);
            }
            else
            {
                c.GetComponentInParent<VehicleData>().currentLife = c.GetComponentInParent<VehicleData>().currentLife + c.GetComponentInParent<VehicleData>().maxLife / 4;
                c.GetComponentInParent<VehicleData>().currentLife = Mathf.Clamp(c.GetComponentInParent<VehicleData>().currentLife, 0, c.GetComponentInParent<VehicleData>().maxLife);
                c.GetComponentInParent<PlayerVehicleData>().CheckHealthBar(true);
                c.GetComponentInParent<PlayerVehicleData>().damageControlSaved = true;
                c.GetComponentInParent<PlayerVehicleData>().uiRef.showDamagecontrolCountdown = false;
            }

            if (c.gameObject.layer == K.LAYER_PLAYER) fade = true;

            /*if (c.GetComponentInParent<BuggyData>().currentLife + c.GetComponentInParent<BuggyData>().maxLife / 4 > c.GetComponentInParent<BuggyData>().maxLife)
            {
                c.GetComponentInParent<BuggyData>().currentLife = c.GetComponentInParent<BuggyData>().maxLife;
            }
            else
            {
                c.GetComponentInParent<BuggyData>().currentLife = c.GetComponentInParent<BuggyData>().currentLife + c.GetComponentInParent<BuggyData>().maxLife / 4;
            }*/
            //print(c.GetComponentInParent<BuggyData>().currentLife);
        }
    }

}
