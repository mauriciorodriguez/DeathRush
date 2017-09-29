using UnityEngine;
using System;
using System.Collections;

public class SawBlade : Ammo
{ 
    private float _powerDamage = 10;
    private LayerMask _myLayer;

    private Collider col;


    void Awake()
    {
        col = this.GetComponent<Collider>();
        col.isTrigger = true;

    }

    public void SetPower(float damage)
    {
        _powerDamage = damage;
    }
    public void SetLayer(LayerMask layer)
    {
        _myLayer = layer;
    }

    void OnTriggerEnter(Collider col)
    {
        print(col.gameObject);
        if (col.gameObject.layer != _myLayer)
        {
            if (col.gameObject.layer == K.LAYER_IA)
            {
                if (col.gameObject.GetComponentInParent<IAVehicleData>() != null) col.gameObject.GetComponentInParent<IAVehicleData>().Damage(_powerDamage, _bulletOwner);
                else col.gameObject.GetComponent<IAVehicleData>().Damage(_powerDamage , _bulletOwner);
                DestroyThis();
            }
            else if (col.gameObject.layer == K.LAYER_GROUND)
            {
                WaitToDestroy();
            }else
            {
                if (col.gameObject.layer != K.LAYER_CHECKPOINT && col.gameObject.layer != 2)
                    DestroyThis();
            }
        }
    }
    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(10);
        DestroyThis();
    }

    void DestroyThis()
    {
        PoolManager.instance.sawBlade.PutBackObject(gameObject);
    }
}
