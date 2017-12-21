using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public Country.NamesType countryType;
    public int resources, selectedRacer, hiredRacers, racesCompleted, racersQty;
    public List<RacerData> racerList;
    public bool canHireNewRacers;
    public List<Upgrade.Type> playerUpgrades;
    public List<int> expNeededToLvlUp;
    public List<Weapon.Type> unlockedWeapons;
    public bool isRaceFinished;
    public List<RacerData> racersForHire;
    public Dictionary<Country.NamesType, int> countryChaos;

    private int _racerCount, _upgradesCount, _unlockedWeaponsCount, _racerForHireCount;

    private void Awake()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetData()
    {
        countryChaos = new Dictionary<Country.NamesType, int>()
        {
            { Country.NamesType.TheLeagueOfClans, 0 },
            { Country.NamesType.NuclearRepublic, 0},
            { Country.NamesType.TheNewEmpire, 0},
            { Country.NamesType.SouthernFighters, 0},
            { Country.NamesType.SonsOfTheApocalypse, 0},
            { Country.NamesType.TheWatchers, 0}
        };
        selectedRacer = -1;
        racesCompleted = 0;
        racersQty = 0;
        racerList = new List<RacerData>();
        canHireNewRacers = false;
        playerUpgrades = new List<Upgrade.Type>();
        expNeededToLvlUp = new List<int>() { 100, 500, 1000 };
        unlockedWeapons = new List<Weapon.Type>();
        racersForHire = new List<RacerData>();
        _racerCount = 0;
        _upgradesCount = 0;
        _unlockedWeaponsCount = 0;
        _racerForHireCount = 0;
    }

    public void SavePlayer()
    {
        PlayerPrefs.DeleteAll();
        _racerCount = 0;
        _upgradesCount = 0;
        _unlockedWeaponsCount = 0;
        _racerForHireCount = 0;
        if (countryType == Country.NamesType.None) return;
        PlayerPrefs.SetString(K.PREFS_PLAYER_COUNTRY, countryType.ToString());
        PlayerPrefs.SetInt(K.PREFS_PLAYER_RESOURCES, resources);
        PlayerPrefs.SetInt(K.PREFS_PLAYER_RACES_COMPLETED, racesCompleted);
        PlayerPrefs.SetString(K.PREFS_PLAYER_CAN_HIRE, canHireNewRacers.ToString());
        PlayerPrefs.SetString(K.PREFS_PLAYER_RACE_FINISHED, isRaceFinished.ToString());
        PlayerPrefs.SetInt(K.PREFS_PLAYER_HIRED_RACERS, hiredRacers);
        foreach (var racer in racerList)
        {
            if (!unlockedWeapons.Contains(racer.equippedPrimaryWeapon))
            {
                unlockedWeapons.Add(racer.equippedPrimaryWeapon);
            }
            if (!unlockedWeapons.Contains(racer.equippedSecondaryWeapon))
            {
                unlockedWeapons.Add(racer.equippedSecondaryWeapon);
            }
            if (!unlockedWeapons.Contains(racer.equippedGadget))
            {
                unlockedWeapons.Add(racer.equippedGadget);
            }

            unlockedWeapons.Add(Weapon.Type.Turret);
            unlockedWeapons.Add(Weapon.Type.MolotovLauncher);
            racer.SaveRacer(_racerCount);
            _racerCount++;
        }
        PlayerPrefs.SetInt(K.PREFS_PLAYER_RACER_COUNT, _racerCount);
        foreach (var racer in racersForHire)
        {
            racer.SaveRacerForHire(_racerForHireCount);
            _racerForHireCount++;
        }
        PlayerPrefs.SetInt(K.PREFS_PLAYER_RACER_FOR_HIRE_COUNT, _racerForHireCount);
        foreach (var upgrade in playerUpgrades)
        {
            PlayerPrefs.SetString(K.PREFS_PLAYER_UPGRADES + _upgradesCount, upgrade.ToString());
            _upgradesCount++;
        }
        PlayerPrefs.SetInt(K.PREFS_PLAYER_UPGRADES_COUNT, _upgradesCount);
        foreach (var weaponU in unlockedWeapons)
        {
            PlayerPrefs.SetString(K.PREFS_PLAYER_UNLOCKED_WEAPONS + _unlockedWeaponsCount, weaponU.ToString());
            _unlockedWeaponsCount++;
        }
        PlayerPrefs.SetInt(K.PREFS_PLAYER_UNLOCKED_WEAPONS_COUNT, _unlockedWeaponsCount);
        foreach (var country in countryChaos.Keys)
        {
            PlayerPrefs.SetInt(K.PREFS_PLAYER_CHAOS + country, countryChaos[country]);
        }
    }

    public RacerData GetSelectedRacer()
    {
        if (selectedRacer < 0) return null;
        else return racerList[selectedRacer];
    }

    public void LoadPlayer()
    {
        racesCompleted = PlayerPrefs.GetInt(K.PREFS_PLAYER_RACES_COMPLETED);
        hiredRacers = PlayerPrefs.GetInt(K.PREFS_PLAYER_HIRED_RACERS);
        countryType = (Country.NamesType)Enum.Parse(typeof(Country.NamesType), PlayerPrefs.GetString(K.PREFS_PLAYER_COUNTRY));
        resources = PlayerPrefs.GetInt(K.PREFS_PLAYER_RESOURCES);
        racerList.Clear();
        playerUpgrades.Clear();
        unlockedWeapons.Clear();
        _racerCount = PlayerPrefs.GetInt(K.PREFS_PLAYER_RACER_COUNT);
        for (int i = 0; i < _racerCount; i++)
        {
            RacerData racer = new RacerData();
            racer.LoadRacer(i);
            racerList.Add(racer);
        }
        _racerForHireCount = PlayerPrefs.GetInt(K.PREFS_PLAYER_RACER_FOR_HIRE_COUNT);
        for (int i = 0; i < _racerForHireCount; i++)
        {
            RacerData racer = new RacerData();
            racer.LoadRacerForHire(i);
            racersForHire.Add(racer);
        }
        _upgradesCount = PlayerPrefs.GetInt(K.PREFS_PLAYER_UPGRADES_COUNT);
        for (int i = 0; i < _upgradesCount; i++)
        {
            playerUpgrades.Add((Upgrade.Type)Enum.Parse(typeof(Upgrade.Type), PlayerPrefs.GetString(K.PREFS_PLAYER_UPGRADES + i)));
        }
        canHireNewRacers = Convert.ToBoolean(PlayerPrefs.GetString(K.PREFS_PLAYER_CAN_HIRE));
        isRaceFinished = Convert.ToBoolean(PlayerPrefs.GetString(K.PREFS_PLAYER_RACE_FINISHED));
        _unlockedWeaponsCount = PlayerPrefs.GetInt(K.PREFS_PLAYER_UNLOCKED_WEAPONS_COUNT);
        for (int i = 0; i < _unlockedWeaponsCount; i++)
        {
            unlockedWeapons.Add((Weapon.Type)Enum.Parse(typeof(Weapon.Type), PlayerPrefs.GetString(K.PREFS_PLAYER_UNLOCKED_WEAPONS + i)));
        }
        foreach (var country in countryChaos.Keys.ToList())
        {
            countryChaos[country] = PlayerPrefs.GetInt(K.PREFS_PLAYER_CHAOS + country);
        }
    }

    public void AddRacer(RacerData r)
    {
        racerList.Add(r);
        SavePlayer();
    }
}
