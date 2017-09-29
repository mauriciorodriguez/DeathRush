using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RacerData
{
    public enum LevelType
    {
        Rookie,
        Experimented,
        Veteran,
        Legendary
    }

    public enum Gender
    {
        Male,
        Female
    }

    public string racerName;
    public VehicleVars.Type racerVehicleType;
    public Classes racerClass;
    public LevelType racerLevelType;
    public float currentLife, maxLife, racerExp;
    public Vehicle racerVehicle;
    public Weapon.Type equippedPrimaryWeapon, equippedSecondaryWeapon, equippedGadget;
    public int level, cost, positionInHire;
    public int skillPoints;
    public int usedSkillPoints = 0;
    public int lastRacePosition = 0;
    public Classes.TypeTierOne unlockedTierOne;
    public Classes.TypeTierTwo unlockedTierTwo;
    public Classes.TypeTierThree unlockedTierThree;
    public bool newAlloysUsed;
    public int headNumber, hairNumber, extraNumer, tierOneNumber, tierTwoNumber, tierThreeNumber, skinColorNumber, hairColorNumber;
    public Gender gender;
    public int id;

    //persistencia
    public List<VehicleVars.Type> unlockedVehicles = new List<VehicleVars.Type>();
    private int _unlockedVehiclesCount;

    private Dictionary<Classes.Type, Func<Classes>> classInit = new Dictionary<Classes.Type, Func<Classes>>() { { Classes.Type.Runner, () => new ClassRunner() },
                                                                                                               { Classes.Type.Berserk,()=>new ClassBerserk()},
                                                                                                               { Classes.Type.Soldier,()=>new ClassSoldier()},
                                                                                                               { Classes.Type.Superstar,()=>new ClassSuperstar()},
                                                                                                               { Classes.Type.Technician,()=>new ClassTechnician()}
                                                                                                              };

    public RacerData()
    {
    }

    public RacerData(string name, Gender g, VehicleVars.Type vType, Classes.Type rClass, int head, int hair, int extra, int hairColor, int skinColor, float mLife = 100, int rCost = 0, float rExp = 0)
    {
        cost = rCost;
        racerName = name;
        racerVehicleType = vType;
        racerVehicle = GetVehiclePrefab(racerVehicleType);
        maxLife = mLife;
        currentLife = maxLife;
        equippedPrimaryWeapon = Weapon.Type.MissileLuncher;
        equippedSecondaryWeapon = Weapon.Type.MolotovLauncher;
        equippedGadget = Weapon.Type.Mines;
        AddExperience(rExp);
        SetClass(rClass);
        headNumber = head;
        hairNumber = hair;
        extraNumer = extra;
        gender = g;
        skinColorNumber = skinColor;
        hairColorNumber = hairColor;
        unlockedVehicles.Add(vType);
    }

    public void AddExperience(float expAmount = 0, bool recalculate = false)
    {
        if (recalculate) { racerExp = 0; }
        racerExp += expAmount;
        if (racerExp >= 1000)
        {
            racerLevelType = LevelType.Legendary;
            level = 3;
            skillPoints = 3;
        }
        else if (racerExp >= 500)
        {
            racerLevelType = LevelType.Veteran;
            level = 2;
            skillPoints = 2;
        }
        else if (racerExp >= 100)
        {
            racerLevelType = LevelType.Experimented;
            level = 1;
            skillPoints = 1;
        }
        else
        {
            racerLevelType = LevelType.Rookie;
            level = 0;
        }
    }

    public void SetClass(Classes.Type cT, bool recalculate = false)
    {
        if (recalculate)
        {
            unlockedTierOne = Classes.TypeTierOne.None;
            unlockedTierTwo = Classes.TypeTierTwo.None;
            unlockedTierThree = Classes.TypeTierThree.None;
            usedSkillPoints = 0;
        }
        racerClass = classInit[cT]();
    }

    private Vehicle GetVehiclePrefab(VehicleVars.Type v)
    {
        GameObject go = null;
        go = VehicleVars.prefabsDict[v]();
        return go.GetComponent<Vehicle>();
    }

    public void SaveRacer(int id)
    {
        this.id = id;
        PlayerPrefs.SetString(K.PREFS_RACER_NAME + id, racerName);
        PlayerPrefs.SetInt(K.PREFS_RACER_ID + id, id);
        PlayerPrefs.SetString(K.PREFS_RACER_VEHICLE_TYPE + id, racerVehicleType + "");
        PlayerPrefs.SetString(K.PREFS_RACER_CLASS + id, racerClass.classType.ToString());
        PlayerPrefs.SetFloat(K.PREFS_RACER_CURRENT_LIFE + id, currentLife);
        PlayerPrefs.SetFloat(K.PREFS_RACER_MAX_LIFE + id, maxLife);
        PlayerPrefs.SetString(K.PREFS_RACER_PRIMARY + id, equippedPrimaryWeapon.ToString());
        PlayerPrefs.SetString(K.PREFS_RACER_SECONDARY + id, equippedSecondaryWeapon.ToString());
        PlayerPrefs.SetString(K.PREFS_RACER_GADGET + id, equippedGadget.ToString());
        PlayerPrefs.SetFloat(K.PREFS_RACER_EXP + id, racerExp);
        PlayerPrefs.SetInt(K.PREFS_RACER_USED_SKILLPOINTS + id, usedSkillPoints);
        PlayerPrefs.SetString(K.PREFS_RACER_UNLOCKED_TIER_ONE + id, unlockedTierOne.ToString());
        PlayerPrefs.SetString(K.PREFS_RACER_UNLOCKED_TIER_TWO + id, unlockedTierTwo.ToString());
        PlayerPrefs.SetString(K.PREFS_RACER_UNLOCKED_TIER_THREE + id, unlockedTierThree.ToString());
        PlayerPrefs.SetString(K.PREFS_RACER_NEW_ALLOYS_USED + id, newAlloysUsed.ToString());
        PlayerPrefs.SetString(K.PREFS_RACER_GENDER + id, gender.ToString());
        PlayerPrefs.SetInt(K.PREFS_RACER_HEAD + id, headNumber);
        PlayerPrefs.SetInt(K.PREFS_RACER_HAIR + id, hairNumber);
        PlayerPrefs.SetInt(K.PREFS_RACER_EXTRA + id, extraNumer);
        PlayerPrefs.SetInt(K.PREFS_RACER_TIER_ONE + id, tierOneNumber);
        PlayerPrefs.SetInt(K.PREFS_RACER_TIER_TWO + id, tierTwoNumber);
        PlayerPrefs.SetInt(K.PREFS_RACER_TIER_THREE + id, tierThreeNumber);
        PlayerPrefs.SetInt(K.PREFS_RACER_SKIN_COLOR + id, skinColorNumber);
        PlayerPrefs.SetInt(K.PREFS_RACER_HAIR_COLOR + id, hairColorNumber);
        _unlockedVehiclesCount = 0;
        foreach (var unlockedVehicle in unlockedVehicles)
        {
            PlayerPrefs.SetString(K.PREFS_RACER_UNLOCKED_VEHICLES + id + _unlockedVehiclesCount++, unlockedVehicle.ToString());
        }
        PlayerPrefs.SetInt(K.PREFS_RACER_UNLOCKED_VEHICLES_COUNT + id, _unlockedVehiclesCount);
    }

    public void SaveRacerForHire(int id)
    {
        PlayerPrefs.SetString(K.PREFS_RACER_NAME + "@" + id, racerName);
        PlayerPrefs.SetString(K.PREFS_RACER_VEHICLE_TYPE + "@" + id, racerVehicleType + "");
        PlayerPrefs.SetString(K.PREFS_RACER_CLASS + "@" + id, racerClass.classType.ToString());
        PlayerPrefs.SetFloat(K.PREFS_RACER_CURRENT_LIFE + "@" + id, currentLife);
        PlayerPrefs.SetFloat(K.PREFS_RACER_MAX_LIFE + "@" + id, maxLife);
        PlayerPrefs.SetString(K.PREFS_RACER_GENDER + "@" + id, gender.ToString());
        PlayerPrefs.SetInt(K.PREFS_RACER_HEAD + "@" + id, headNumber);
        PlayerPrefs.SetInt(K.PREFS_RACER_HAIR + "@" + id, hairNumber);
        PlayerPrefs.SetInt(K.PREFS_RACER_EXTRA + "@" + id, extraNumer);
        PlayerPrefs.SetInt(K.PREFS_RACER_SKIN_COLOR + "@" + id, skinColorNumber);
        PlayerPrefs.SetInt(K.PREFS_RACER_HAIR_COLOR + "@" + id, hairColorNumber);
        PlayerPrefs.SetInt(K.PREFS_RACER_POSITION_IN_HIRE + "@" + id, positionInHire);
        PlayerPrefs.SetInt(K.PREFS_RACER_COST + "@" + id, cost);
    }

    public void LoadRacerForHire(int id)
    {
        racerName = PlayerPrefs.GetString(K.PREFS_RACER_NAME + "@" + id);
        racerVehicleType = (VehicleVars.Type)Enum.Parse(typeof(VehicleVars), PlayerPrefs.GetString(K.PREFS_RACER_VEHICLE_TYPE + "@" + id));
        currentLife = PlayerPrefs.GetFloat(K.PREFS_RACER_CURRENT_LIFE + "@" + id);
        maxLife = PlayerPrefs.GetFloat(K.PREFS_RACER_MAX_LIFE + "@" + id);
        gender = (Gender)Enum.Parse(typeof(Gender), PlayerPrefs.GetString(K.PREFS_RACER_GENDER + "@" + id));
        hairNumber = PlayerPrefs.GetInt(K.PREFS_RACER_HAIR + "@" + id);
        headNumber = PlayerPrefs.GetInt(K.PREFS_RACER_HEAD + "@" + id);
        extraNumer = PlayerPrefs.GetInt(K.PREFS_RACER_EXTRA + "@" + id);
        skinColorNumber = PlayerPrefs.GetInt(K.PREFS_RACER_SKIN_COLOR + "@" + id);
        hairColorNumber = PlayerPrefs.GetInt(K.PREFS_RACER_HAIR_COLOR + "@" + id);
        cost = PlayerPrefs.GetInt(K.PREFS_RACER_COST + "@" + id);
        SetClass((Classes.Type)Enum.Parse(typeof(Classes.Type), PlayerPrefs.GetString(K.PREFS_RACER_CLASS + "@" + id)));
        CompleateLoadInit();
        positionInHire = PlayerPrefs.GetInt(K.PREFS_RACER_POSITION_IN_HIRE + "@" + id);
    }

    public void LoadRacer(int id)
    {
        id = PlayerPrefs.GetInt(K.PREFS_RACER_ID + id);
        racerName = PlayerPrefs.GetString(K.PREFS_RACER_NAME + id);
        racerVehicleType = (VehicleVars.Type)Enum.Parse(typeof(VehicleVars.Type), PlayerPrefs.GetString(K.PREFS_RACER_VEHICLE_TYPE + id));
        equippedPrimaryWeapon = (Weapon.Type)Enum.Parse(typeof(Weapon.Type), PlayerPrefs.GetString(K.PREFS_RACER_PRIMARY + id));
        equippedSecondaryWeapon = (Weapon.Type)Enum.Parse(typeof(Weapon.Type), PlayerPrefs.GetString(K.PREFS_RACER_SECONDARY + id));
        equippedGadget = (Weapon.Type)Enum.Parse(typeof(Weapon.Type), PlayerPrefs.GetString(K.PREFS_RACER_GADGET + id));
        currentLife = PlayerPrefs.GetFloat(K.PREFS_RACER_CURRENT_LIFE + id);
        maxLife = PlayerPrefs.GetFloat(K.PREFS_RACER_MAX_LIFE + id);
        racerExp = PlayerPrefs.GetFloat(K.PREFS_RACER_EXP + id);
        usedSkillPoints = PlayerPrefs.GetInt(K.PREFS_RACER_USED_SKILLPOINTS + id);
        newAlloysUsed = Convert.ToBoolean(PlayerPrefs.GetString(K.PREFS_RACER_NEW_ALLOYS_USED + id));
        unlockedTierOne = (Classes.TypeTierOne)Enum.Parse(typeof(Classes.TypeTierOne), PlayerPrefs.GetString(K.PREFS_RACER_UNLOCKED_TIER_ONE + id));
        unlockedTierTwo = (Classes.TypeTierTwo)Enum.Parse(typeof(Classes.TypeTierTwo), PlayerPrefs.GetString(K.PREFS_RACER_UNLOCKED_TIER_TWO + id));
        unlockedTierThree = (Classes.TypeTierThree)Enum.Parse(typeof(Classes.TypeTierThree), PlayerPrefs.GetString(K.PREFS_RACER_UNLOCKED_TIER_THREE + id));
        gender = (Gender)Enum.Parse(typeof(Gender), PlayerPrefs.GetString(K.PREFS_RACER_GENDER + id));
        hairNumber = PlayerPrefs.GetInt(K.PREFS_RACER_HAIR + id);
        headNumber = PlayerPrefs.GetInt(K.PREFS_RACER_HEAD + id);
        extraNumer = PlayerPrefs.GetInt(K.PREFS_RACER_EXTRA + id);
        tierOneNumber = PlayerPrefs.GetInt(K.PREFS_RACER_TIER_ONE + id);
        tierTwoNumber = PlayerPrefs.GetInt(K.PREFS_RACER_TIER_TWO + id);
        tierThreeNumber = PlayerPrefs.GetInt(K.PREFS_RACER_TIER_THREE + id);
        skinColorNumber = PlayerPrefs.GetInt(K.PREFS_RACER_SKIN_COLOR + id);
        hairColorNumber = PlayerPrefs.GetInt(K.PREFS_RACER_HAIR_COLOR + id);
        AddExperience(racerExp, true);
        SetClass((Classes.Type)Enum.Parse(typeof(Classes.Type), PlayerPrefs.GetString(K.PREFS_RACER_CLASS + id)));
        _unlockedVehiclesCount = PlayerPrefs.GetInt(K.PREFS_RACER_UNLOCKED_VEHICLES_COUNT + id);
        for (int i = 0; i < _unlockedVehiclesCount; i++)
        {
            unlockedVehicles.Add((VehicleVars.Type)Enum.Parse(typeof(VehicleVars.Type), PlayerPrefs.GetString(K.PREFS_RACER_UNLOCKED_VEHICLES + id + i)));
        }
        CompleateLoadInit();
    }

    public void CompleateLoadInit()
    {
        racerVehicle = GetVehiclePrefab(racerVehicleType);
    }
}
