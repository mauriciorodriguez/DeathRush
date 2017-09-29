using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Portrait : MonoBehaviour
{
    public Image head, hair, extra, tOne, tTwo, tThree;
    

    public string racerName;
    public VehicleVars.Type racerVehicle;
    public Classes.Type racerClass;
    public float maxLife;
    public int cost;
    [HideInInspector]
    public int headNumber, hairNumber, extraNumber, tierOneNumber, tierTwoNumber, tierThreeNumber, skinColorNumber, hairColorNumber, positionInHire;
    [HideInInspector]
    public RacerData.Gender gender;

    private PlayerData _playerData;

    public void Randomize()
    {
        _playerData = PlayerData.instance;
        var vehicleTypes = Enum.GetNames(typeof(VehicleVars.Type));
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.AlienVehicle)) racerVehicle = (VehicleVars.Type)Enum.Parse(typeof(VehicleVars.Type), vehicleTypes[UnityEngine.Random.Range(0, vehicleTypes.Length)]);
        else racerVehicle = (VehicleVars.Type)Enum.Parse(typeof(VehicleVars.Type), vehicleTypes[UnityEngine.Random.Range(0, vehicleTypes.Length - 1)]);
        var classesTypes = Enum.GetNames(typeof(Classes.Type));
        racerClass = (Classes.Type)Enum.Parse(typeof(Classes.Type), classesTypes[UnityEngine.Random.Range(0, classesTypes.Length)]);
        var genderTypes = Enum.GetNames(typeof(RacerData.Gender));
        gender = (RacerData.Gender)Enum.Parse(typeof(RacerData.Gender), genderTypes[UnityEngine.Random.Range(0, genderTypes.Length)]);
        cost = VehicleVars.cost[racerVehicle];
        racerName = Country.countriesNames[_playerData.countryType][gender][UnityEngine.Random.Range(0, Country.countriesNames[_playerData.countryType][gender].Count)];
    }

    public void Build(RacerData rd)
    {
        GameObject.FindGameObjectWithTag(K.TAG_PORTRAIT_BUILDER).GetComponent<PortraitBuilder>().Build(this, rd);
    }
}
