using UnityEngine;
using System.Collections;
using System;

public class IADroneGadget : Trap
{
    public float damage;
    public float shootRounds;
    public float speed = 0.2f;
    public float lifeTime;
    public GameObject feedbackVisual;
    public Transform particlePoint;
    private bool _setPosition;
    public float _timerUp = 3f;
    private Collider _myCollider;
    private GameObject target;

	// Use this for initialization
	void Start ()
    {
        transform.position = transform.position + transform.forward * 3f + transform.up*2f;
        _myCollider = GetComponentInChildren<Collider>();
        _myCollider.enabled = false;

        StartCoroutine(Attack());
	}

    IEnumerator Attack()
    {
        if (target != null && target.GetComponent<VehicleData>() != null)
        {
            transform.LookAt(target.transform.position);
            target.GetComponent<VehicleData>().Damage(damage, GetOwner());
            shootRounds--;
            if (shootRounds <= 0)
                Destroy(this.gameObject);

            Instantiate(feedbackVisual, particlePoint.position, particlePoint.rotation);

        }
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(Attack());
    }
	
	// Update is called once per frame
	public override void Update ()
    {
        if (!_setPosition)
        {
            transform.position += transform.up * Time.deltaTime * speed;
            _timerUp -= Time.deltaTime;
            if (_timerUp <= 0)
            {
                _setPosition = true;
                _myCollider.enabled = true;
            }
        }

        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
            Destroy(this.gameObject);
	}



    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_IA && col.GetComponentInParent<VehicleData>() != null && target == null)
        {
            target = col.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_IA && target == col.gameObject.transform.parent.gameObject)
        {
            target = null;
        }
    }
}
