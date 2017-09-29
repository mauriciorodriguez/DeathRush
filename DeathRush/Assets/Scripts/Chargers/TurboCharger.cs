using UnityEngine;
using System.Collections;

public class TurboCharger : MonoBehaviour
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
        var aux = c.GetComponentInParent<VehiclePlayerController>();
        //print("Repair");
        if (aux != null)
        {
            aux._canRechargeNitro = true;
            aux.RechargeNitro();
            if (aux.canHyperCharge)
            {
                aux.GetComponent<VehicleData>().currentLife = aux.GetComponent<VehicleData>().maxLife;
            }

            if (c.gameObject.layer == K.LAYER_PLAYER) fade = true;
        }
    }
}
