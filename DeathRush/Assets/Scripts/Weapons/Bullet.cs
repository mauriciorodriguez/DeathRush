using UnityEngine;
using System.Collections;

public class Bullet : Ammo
{
    public float speed = 75;
    public float powerDamage = 5;
    public float destroyTime;
    private float _lifeTime;
    public GameObject spark;
    private Collider col;

    // Use this for initialization
    void Start()
    {
        col = this.GetComponent<Collider>();
        col.isTrigger = true;

    }

    // Update is called once per frame
    void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > 0.05f && col.isTrigger) col.isTrigger = false;
        if (destroyTime <= _lifeTime) DestroyThis();

        transform.position += transform.forward * speed * Time.deltaTime;

    }

    public override void OnRelease()
    {
        base.OnRelease();
        _lifeTime = 0;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == K.LAYER_IA)
        {
            col.gameObject.GetComponent<IAVehicleData>().Damage(powerDamage, _bulletOwner);
            Vector3 cont = col.contacts[0].point;
            Instantiate(spark, cont + -transform.forward, Quaternion.identity);
            DestroyThis();
        }
      /*  else if (col.gameObject.layer == K.LAYER_PLAYER)
        {
            col.gameObject.GetComponent<PlayerVehicleData>().Damage(powerDamage, _bulletOwner);
            Vector3 cont = col.contacts[0].point;
            Instantiate(spark, cont + -transform.forward, Quaternion.identity);
            DestroyThis();
        }*/
        else
        {
            if (col.contacts.Length != 0)
                Instantiate(spark, col.contacts[0].point, Quaternion.identity);
            DestroyThis();
        }
    }

    void DestroyThis()
    {
        //Destroy(gameObject);
        PoolManager.instance.bullets.PutBackObject(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
