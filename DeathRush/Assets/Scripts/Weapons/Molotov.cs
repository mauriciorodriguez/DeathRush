using UnityEngine;
using System.Collections;

public class Molotov : Ammo {
    
    public float fallSpeed;
    private Rigidbody _rb;
    private RaycastHit hit;
    public LayerMask maskGround;
	// Use this for initialization
	void Start ()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
      //  transform.position += -transform.up * fallSpeed * Time.deltaTime;
	}
    void FixedUpdate()
    {
        _rb.AddForce(-transform.up , ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == K.LAYER_GROUND)
        {

            GameObject fire = PoolManager.instance.fire.GetObject();
            fire.transform.position = col.contacts[0].point + Vector3.up;

            fire.GetComponent<FireFloor>().SetObjectParent(_bulletOwner);


            PoolManager.instance.molotov.PutBackObject(gameObject);
        }
        else
        {
            Physics.Raycast(col.contacts[0].point, -transform.up, out hit, maskGround);

            GameObject fire = PoolManager.instance.fire.GetObject();
            fire.transform.position = hit.point + Vector3.up;
            fire.GetComponent<FireFloor>().SetObjectParent(_bulletOwner);


            PoolManager.instance.molotov.PutBackObject(gameObject);
        }
    }


    public override void OnRelease()
    {
        base.OnRelease();
        _rb.velocity = Vector3.zero;
    }
}
