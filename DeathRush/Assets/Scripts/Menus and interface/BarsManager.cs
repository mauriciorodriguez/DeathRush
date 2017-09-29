using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarsManager : MonoBehaviour {

    private PlayerData _playerData;

    public Image armor;
    public Image armorGreen;
    public Image armorRed;
    private float armorValue;
    private float armorGlobal;
    private float armorPlus;
    private float armorMinus;

    public Image damage;
    public Image damageGreen;
    public Image damageRed;
    private float damageValue;
    private float damageGlobal;
    private float damagePlus;
    private float damageMinus;

    public Image maxSpeed;
    public Image maxSpeedGreen;
    public Image maxSpeedRed;
    private float maxSpeedValue;
    private float maxSpeedGlobal;
    private float maxSpeedPlus;
    private float maxSpeedMinus;

    public Image acceleration;
    public Image accelerationGreen;
    public Image accelerationRed;
    private float accelerationValue;
    private float accelerationGlobal;
    private float accelerationPlus;
    private float accelerationMinus;

    public Image turbo;
    public Image turboGreen;
    public Image turboRed;
    private float turboValue;
    private float turboGlobal;
    private float turboPlus;
    private float turboMinus;


    public Image weight;
    private float weightValue;

    public void Awake()
    {
        armor.GetComponentInChildren<Text>().text = "";
        damage.GetComponentInChildren<Text>().text = "";

        UpdateBars();
        UpdateGreenBars();
        UpdateRedBars();
    }


    public void OnImplementation()
    {
        _playerData = PlayerData.instance;
        var racer = _playerData.GetSelectedRacer();

        armorPlus = 0;
        armorMinus = 0;
        maxSpeedPlus = 0;
        maxSpeedMinus = 0;
        damagePlus = 0;
        damageMinus = 0;
        accelerationPlus = 0;
        accelerationMinus = 0;
        turboPlus = 0;
        turboMinus = 0;

        //MODIFICACIÓN MEDIANTE PERKS

        if (racer.racerClass.classType==Classes.Type.Soldier)
        {
            armorPlus = 0.2f;
            damagePlus = 0.2f;
            maxSpeedMinus = 0.1f;
            turboMinus = 0.1f;
        }
        if (racer.racerClass.classType== Classes.Type.Berserk)
        {
            damagePlus = 0.2f;
            armorMinus = 0.1f;
        }
        if(racer.racerClass.classType== Classes.Type.Runner)
        {
            maxSpeedPlus = 0.2f;
            accelerationPlus = 0.2f;
            damageMinus = 0.2f;
            armorMinus = 0.1f;
        }



        
    }

    public void BarStats(VehicleVars.Type vt)
    {
        if (vt == VehicleVars.Type.Buggy)
        {
            armorValue = 0.15f;
            damageValue = 0.15f;
            maxSpeedValue = 0.45f;
            accelerationValue = 0.30f;
            turboValue = 0.30f;
            weightValue = 0.15f;
        }
        else if (vt == VehicleVars.Type.Bigfoot)
        {
            armorValue = 0.30f;
            damageValue = 0.30f;
            maxSpeedValue = 0.30f;
            accelerationValue = 0.45f;
            turboValue = 0.15f;
            weightValue = 0.30f;
        }
        else if (vt == VehicleVars.Type.Truck)
        {
            armorValue = 0.45f;
            damageValue = 0.45f;
            maxSpeedValue = 0.15f;
            accelerationValue = 0.15f;
            turboValue = 0.30f;
            weightValue = 0.45f;
        }
        /*
        else if (vt==VehicleType.Alien)
        {
            //Proximamente
        }
        */

        UpdateBars();
        UpdateGreenBars();
        UpdateRedBars();

        armor.GetComponentInChildren<Text>().text = "Armor";
        damage.GetComponentInChildren<Text>().text = "Damage";
        maxSpeed.GetComponentInChildren<Text>().text = "Max Speed";
        acceleration.GetComponentInChildren<Text>().text = "Acceleration";
        turbo.GetComponentInChildren<Text>().text = "Turbo";
        weight.GetComponentInChildren<Text>().text = "Weight";

    }

    private void UpdateBars()
    {
        armor.fillAmount = armorValue - armorMinus;
        damage.fillAmount = damageValue - damageMinus;
        maxSpeed.fillAmount = maxSpeedValue - maxSpeedMinus;
        acceleration.fillAmount = accelerationValue - accelerationMinus;
        turbo.fillAmount = turboValue - turboMinus;
        weight.fillAmount = weightValue;

    }

    //VERDES
    private void UpdateGreenBars()
    {
        //ARMOR
        if (armorPlus+armorGlobal>armorMinus)
        {
            armorGreen.fillAmount = armorValue + armorPlus +armorGlobal - armorMinus;

        }
        else
        {
            armorGreen.fillAmount = 0f;

        }


        //DAMAGE
        if (damagePlus + damageGlobal > damageMinus)
        {
            damageGreen.fillAmount = damageValue + damagePlus + damageGlobal - damageMinus;

        }
        else
        {
            damageGreen.fillAmount = 0f;

        }


        //MAX SPEED
        if (maxSpeedPlus + maxSpeedGlobal > maxSpeedMinus)
        {
            maxSpeedGreen.fillAmount = maxSpeedValue + maxSpeedPlus + maxSpeedGlobal - maxSpeedMinus;

        }
        else
        {
            maxSpeedGreen.fillAmount = 0f;

        }

        //ACCELERATION
        if (accelerationPlus + accelerationGlobal > accelerationMinus)
        {
            accelerationGreen.fillAmount = accelerationValue + accelerationPlus + accelerationGlobal - accelerationMinus;

        }
        else
        {
            accelerationGreen.fillAmount = 0f;

        }

        //TURBO
        if (turboPlus + turboGlobal > turboMinus)
        {
            turboGreen.fillAmount = turboValue + turboPlus + turboGlobal - turboMinus;

        }
        else
        {
            turboGreen.fillAmount = 0f;

        }


    }

    private void UpdateRedBars()
    {
        //ARMOR
        if (armorPlus + armorGlobal < armorMinus)
        {
            armorRed.fillAmount = armorValue-armorPlus-armorGlobal;
        }
        else
        {
            armorRed.fillAmount = 0f;
        }



        //DAMAGE
        if (damagePlus + damageGlobal < damageMinus)
        {
            damageRed.fillAmount = damageValue-damagePlus-damageGlobal;
        }
        else
        {
            damageRed.fillAmount = 0f;
        }

        //MAX SPEED
        if (maxSpeedPlus + maxSpeedGlobal < maxSpeedMinus)
        {
            maxSpeedRed.fillAmount = maxSpeedValue - maxSpeedPlus - maxSpeedGlobal;
        }
        else
        {
            maxSpeedRed.fillAmount = 0f;
        }

        //ACCELERATION
        if (accelerationPlus + accelerationGlobal < accelerationMinus)
        {
            accelerationRed.fillAmount = accelerationValue - accelerationPlus - accelerationGlobal;
        }
        else
        {
            accelerationRed.fillAmount = 0f;
        }

        //TURBO
        if (turboPlus + turboGlobal < turboMinus)
        {
            turboRed.fillAmount = turboValue - turboPlus - turboGlobal;
        }
        else
        {
            turboRed.fillAmount = 0f;
        }

    }
}
