using UnityEngine;
using System.Collections;


public class Rocket : Ammo
{
    public float lifeTime;
    public float velocity;
    public float turnSpeed;
    public float radio;
    private float damage;
    public LayerMask layersDamage;
    public Rigidbody myBody;
    public GameObject expFeed;
    private int tipe;
    private GameObject target;
    private Vector3 targetVector;
    private Quaternion rotationTarget;
    private float _life;
    protected SoundManager _soundManagerReference;

    protected void Start()
    {
        _soundManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<SoundManager>();
    }
    
    void Update ()
    {
        if (tipe == 1 && target != null)
        {
            targetVector = target.transform.position;
            if (Vector3.Distance(transform.position, targetVector) < 1f) Explote();
        }

        if (tipe == 2)
        {
            if (Vector3.Distance(transform.position, targetVector) < 2f)
                Explote();
        }
        _life += Time.deltaTime;

        if (_life > lifeTime)
            Explote();

        transform.LookAt(targetVector);	
	}
	
	void FixedUpdate ()
    {
        if (targetVector != null)
        {
            myBody.velocity = transform.forward * velocity;

            /*rotationTarget = Quaternion.LookRotation(targetVector - transform.position);
            myBody.MoveRotation(Quaternion.RotateTowards(transform.rotation , rotationTarget, turnSpeed));*/
        }
	}

    public override void OnRelease()
    {
        base.OnRelease();
        _life = 0;
    }

    public void SetTarget(GameObject v, float damageDone)
    {
        damage = damageDone;
        target = v;
        tipe = 1;
    }
    public void SetTarget(Vector3 v, float damageDone)
    {
        targetVector = v;
        tipe = 2;
        damage = damageDone;
    }

    private void Explote()
    {
        /*
        var cols = Physics.OverlapSphere(transform.position, radio, layersDamage);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.layer == K.LAYER_IA)
            {
                cols[i].GetComponentInParent<VehicleData>().Damage(damage);
                DestroyThis();
            }
        }
        */
        
        var colsDrone = Physics.OverlapSphere(transform.position, radio);
        foreach (var coll in colsDrone)
        {
            if (coll.gameObject.layer == K.LAYER_IA && coll.GetComponentInParent<VehicleData>() != null)
            {
                _soundManagerReference.PlaySound(K.SOUND_MISSILE_HEAVY);
                coll.GetComponentInParent<IAVehicleData>().Damage(damage, _bulletOwner);
            }

            if (coll.gameObject.layer == K.LAYER_PLAYER && coll.GetComponentInParent<VehicleData>() != null)
            {
                _soundManagerReference.PlaySound(K.SOUND_MISSILE_HEAVY);
                coll.GetComponentInParent<PlayerVehicleData>().Damage(damage, _bulletOwner);
            }

            if (coll.gameObject.layer == K.LAYER_DESTRUCTIBLE && coll.GetComponent<Animation>() != null) coll.GetComponent<DestructibleElement>().DestroyDrone();

            if (coll.gameObject.layer == K.LAYER_DESTRUCTIBLE && coll.gameObject.tag == "Mine")
            {
                coll.GetComponentInParent<Trap>().SetOwner(_bulletOwner);
                coll.GetComponentInParent<Trap>().Detonator();
            }
            DestroyThis();
        }
//       _soundManagerReference.PlaySound(K.SOUND_MISSILE);
        CreateParticle();
    }


    void OnCollisionEnter(Collision col)
    {

        Explote();/*
        if (col.gameObject.transform.parent.transform.parent.gameObject.layer == K.LAYER_PLAYER)
        {
            _soundManagerReference.PlaySound(K.SOUND_MISSILE_HEAVY);
        }*/

        /*
        var cols = Physics.OverlapSphere(transform.position, radio, layersDamage);
        for (int i = 0; i < cols.Length; i++)
        {/*
            if (cols[i].GetComponent<Rigidbody>() != null)
            {
                Vector3 direction = cols[i].transform.position - transform.position;
                float dist = direction.magnitude;
                direction.Normalize();
                print(cols[i].gameObject);
            
                cols[i].GetComponent<Rigidbody>().AddForce(direction * damage * (1 - (dist / radio)));
                // print("impact");
                //cols[i].GetComponent<Rigidbody>().AddExplosionForce(expPower, transform.position, expRadius, 1.5f, ForceMode.Impulse);
                //cols[i].rigidbody.AddForce(direction * expPower * (1 - (dist / expRadius)));
            }

            if (cols[i].gameObject.layer == K.LAYER_IA || cols[i].gameObject.layer == K.LAYER_PLAYER)
            {
               //cols[i].GetComponentInParent<VehicleData>().Damage(damage, _bulletOwner);

                //cols[i].gameObject.transform.parent.gameObject.GetComponent<IAController>().Damage(damage);
                CreateParticle();
                _soundManagerReference.PlaySound(K.SOUND_MISSILE_HEAVY);
            }
        }

      //  if (col.gameObject.tag == "Target")
        DestroyThis();*/
    }

    void CreateParticle()
    {
        GameObject.Instantiate(expFeed, transform.position, Quaternion.identity);
        DestroyThis();
    }

    private void DestroyThis()
    {
        PoolManager.instance.rocketLock.PutBackObject(gameObject);
    }
}
