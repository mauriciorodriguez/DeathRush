using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PressMine : Trap
{
    //public AudioSource sound;
    public GameObject feedLight;
    private float coolLig = 0.5f;
    private float currentCool;

    // protected SoundManager _soundManagerReference;

    private void Start()
    {
        // _soundManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<SoundManager>();
    }

    public override void Update()
    {
        base.Update();

        if (feedLight == null) return;

        if (currentCool < coolLig)
        {
            feedLight.SetActive(false);
            currentCool += Time.deltaTime;
        }
        else
        {
            feedLight.SetActive(true);
            currentCool = 0;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == K.LAYER_PLAYER || col.gameObject.layer == K.LAYER_IA || col.gameObject.layer == K.LAYER_MISSILE || col.gameObject.layer == K.LAYER_IGNORERAYCAST && col.GetComponent<FireFloor>())
        {
            if (col.gameObject != null && !touched)
                touched = true;
        }
    }

    public override void Detonator()
    {
        // print("detonate");

        base.Detonator();
        Explosion();


    }

    public void Explosion()
    {
        // print("explote");


        var cols = Physics.OverlapSphere(transform.position, expRadius, layersDamege);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].GetComponentInParent<Rigidbody>() != null)
            {

                cols[i].GetComponentInParent<Rigidbody>().AddExplosionForce(expPower, transform.position, expRadius, 0.5f, ForceMode.Impulse);
                if (cols[i].gameObject.layer == K.LAYER_PLAYER)
                {
                    //PRUEBA DE PERK
                    var pd = PlayerData.instance;
                    float dmg = 0;
                    if (pd.racerList[pd.selectedRacer].unlockedTierOne == Classes.TypeTierOne.Miner)
                    {
                        dmg = expDamage * .25f;
                    }
                    else dmg = expDamage;

                    VehicleData own;
                    if (GetOwner() != null) { own = GetOwner(); }
                    else own = cols[i].gameObject.GetComponentInParent<PlayerVehicleData>();

                    cols[i].gameObject.GetComponentInParent<PlayerVehicleData>().Damage(dmg, own);
                }
                else if (cols[i].gameObject.layer == K.LAYER_IA)
                {

                    VehicleData own;
                    if (GetOwner() != null) { own = GetOwner(); }
                    else own = cols[i].gameObject.GetComponentInParent<IAVehicleData>();

                    if (cols[i].gameObject.GetComponentInParent<IAVehicleData>())
                    cols[i].gameObject.GetComponentInParent<IAVehicleData>().Damage(expDamage, own);
                }
            }
        }
        Instantiate(feedback, transform.position + transform.up, Quaternion.identity);
        //   sound.Play();        
        Destroy(this.gameObject);

        if (transform.GetComponentInParent<NavMeshAgent>() != null) Destroy(transform.parent.gameObject);
    }

}
