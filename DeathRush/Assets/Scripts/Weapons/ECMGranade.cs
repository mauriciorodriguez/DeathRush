using UnityEngine;
using System.Collections;

public class ECMGranade : Trap
{

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
        if (col.gameObject.layer == K.LAYER_PLAYER || col.gameObject.layer == K.LAYER_IA)
        {
            if (col.gameObject.layer == K.LAYER_PLAYER)
            {
                if (PlayerData.instance.racerList[PlayerData.instance.selectedRacer].unlockedTierTwo == Classes.TypeTierTwo.Hacker) return;
                var weaps = col.gameObject.GetComponentsInParent<Weapon>();
                for (int i = 0; i < weaps.Length; i++)
                {
                    weaps[i].EMP();
                }
                  
            }
            else if (col.gameObject.layer == K.LAYER_IA)
                col.gameObject.GetComponentInParent<IAVehicleData>().StopAttacks();



            if (col.gameObject != null && !touched)
                touched = true;
        }
    }

    public override void Detonator()
    {
        // print("detonate");

        base.Detonator();

        Instantiate(feedback, transform.position + transform.up, Quaternion.identity);
        Destroy(this.gameObject);
    }
    

}
