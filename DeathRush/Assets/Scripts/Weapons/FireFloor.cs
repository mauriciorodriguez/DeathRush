using UnityEngine;
using System.Collections;

public class FireFloor : Ammo {

    public float staydamage = 0.01f;
    public float duration = 2f;
    private float _current;

	// Use this for initialization
	void Update()
    {
        _current += Time.deltaTime;
        if (_current > duration)
        {
            _current = 0;
            PoolManager.instance.fire.PutBackObject(this.gameObject);
        }
    }

    public override void OnAcquire()
    {
        base.OnAcquire();
        _current = 0;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_IA)
        {
            if(col.GetComponentInParent<IAVehicleData>())
            col.GetComponentInParent<IAVehicleData>().Damage(staydamage * 20, _bulletOwner);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.layer == K.LAYER_IA)
        {
            if (col.GetComponentInParent<IAVehicleData>())
                col.GetComponentInParent<IAVehicleData>().Damage(staydamage, _bulletOwner);
        }
    }
}
