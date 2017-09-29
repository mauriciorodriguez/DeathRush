using UnityEngine;
using System.Collections;

public class HeGranade : Trap
{
    
    private float coolLig = 0.5f;
    private float currentCool;

    // Use this for initialization
    void Start ()
    {
	
	}

    public override void Update()
    {
        detonTime -= Time.deltaTime;
        if (detonTime <= 0)
            Detonator();
    }

    public void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(-transform.forward * 0.25f, ForceMode.Impulse);
    }

    // Update is called once per frame


    public override void Detonator()
    {
        Explosion();
    }

    public void Explosion()
    {
        // print("explote");

        var cols = Physics.OverlapSphere(transform.position, expRadius, layersDamege);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].GetComponent<Rigidbody>() != null)
            {
                // print("impact");
                //print(cols[i].gameObject);
                cols[i].GetComponent<Rigidbody>().AddExplosionForce(expPower, transform.position, expRadius, 2f, ForceMode.Impulse);
                if (cols[i].gameObject.layer == K.LAYER_PLAYER)
                    cols[i].gameObject.GetComponentInParent<PlayerVehicleData>().Damage(expDamage, GetOwner());

                if (cols[i].gameObject.layer == K.LAYER_IA)
                    cols[i].gameObject.GetComponentInParent<IAVehicleData>().Damage(expDamage, GetOwner());


            }
        }
        Instantiate(feedback, transform.position + transform.up, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
