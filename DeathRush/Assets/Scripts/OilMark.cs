using UnityEngine;
using System.Collections;

public class OilMark : Trap
{
    public LayerMask layer;

	// Use this for initialization
	void Start ()
    {
        touched = true;
        var rays = Physics.RaycastAll(transform.position, -Vector3.up);
        foreach (var item in rays)
        {
            if (item.transform.gameObject.layer == K.LAYER_GROUND)
            {
                transform.position = item.point + Vector3.up * 0.2f;
            }
        }
	
	}

    public override void Detonator()
    {
        base.Detonator();
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponentInParent<Vehicle>() != null)
        {
            col.gameObject.GetComponentInParent<Vehicle>().OnOil();
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponentInParent<Vehicle>() != null)
        {
            col.gameObject.GetComponentInParent<Vehicle>().OutOil(expPower);
        }
    }
}
