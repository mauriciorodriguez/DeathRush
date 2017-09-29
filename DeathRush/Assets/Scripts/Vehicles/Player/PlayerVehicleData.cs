using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class PlayerVehicleData : VehicleData
{

    public bool minerPerk;
    public bool knightPerk;
    public bool riskyPerk;
    public bool spikesPerk;


    public GameObject DamagePortrait;
    private GameObject _glassDamage;
    private List<RectTransform> _crackedGlass;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        var pd = PlayerData.instance;
        _racerData = pd.racerList[pd.selectedRacer];
        _glassDamage = GameObject.FindGameObjectWithTag(K.TAG_GLASS_DAMAGE);

        _crackedGlass = _glassDamage.GetComponentsInChildren<RectTransform>().ToList();
        _crackedGlass.RemoveAt(0);

        CheckHealthBar(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Damage(float damageTaken, VehicleData atk)
    {
        if (_racerData.racerClass.classType == Classes.Type.Superstar &&
            _racerData.unlockedTierTwo == Classes.TypeTierTwo.Lucky)
        {
            if (Random.value <= .3f)
            {
                return;
            }
        }
        if (_racerData.racerClass.classType == Classes.Type.Technician &&
            _racerData.unlockedTierThree == Classes.TypeTierThree.DamageControl)
        {
            if (!damageControlUsed && (currentLife - damageTaken) <= 0)
            {
                damageControlUsed = true;
                _damageControlTimer = 30;
                currentLife = 1;
                damageTaken = 0;
            }
        }
        base.Damage(damageTaken, atk);
        if (!_alive)
        {
            K.pilotIsAlive = false;
        }
    }

    public override void CheckHealthBar(bool hasCured)
    {
        /*  float calc_health = currentLife / maxLife;
          _visualHealth.fillAmount = calc_health;*/
        base.CheckHealthBar(hasCured);
        if (currentLife != maxLife)
        {
            var index = Random.Range(0, _crackedGlass.Count - 1);
            _crackedGlass[index].GetComponent<RawImage>().enabled = true;
        }

        if (hasCured)
        {
            foreach (var glass in _crackedGlass) glass.GetComponent<RawImage>().enabled = false;
        }

        if (currentLife >= 80)
        {
            whiteSmoke.Stop();
            blackSmoke.Stop();
            fire.Stop();

        }
        else if (currentLife >= 50)
        {
            whiteSmoke.Play();
            blackSmoke.Stop();
            fire.Stop();
        }
        else if (currentLife >= 30)
        {
            whiteSmoke.Stop();
            blackSmoke.Play();
            fire.Stop();
        }

        else if (currentLife >= 0)
        {
            whiteSmoke.Stop();
            blackSmoke.Play();
            fire.Play();
        }
    }
}
