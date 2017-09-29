using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public enum WheelDriveType
{
    RWD, //REAR WHEEL DRIVE
    FWD, //FRONT WHEEL DRIVE
    AWD //ALL WHEEL DRIVE
};
public class VehicleVars : ScriptableObject 
{
    
    public enum Type
    {
        Buggy,
        Bigfoot,
        Truck,
        Alien
    }
    public static Dictionary<Type, Func<GameObject>> prefabsDict = new Dictionary<Type, Func<GameObject>>()
    {
        {Type.Buggy, () => Resources.Load<GameObject>(K.PATH_VEHICLE_BUGGY) },
        {Type.Bigfoot, () => Resources.Load<GameObject>(K.PATH_VEHICLE_BIGFOOT) },
        {Type.Truck, () => Resources.Load<GameObject>(K.PATH_VEHICLE_TRUCK) },
        {Type.Alien, () => Resources.Load<GameObject>(K.PATH_VEHICLE_X_T42) }
    };

    public static Dictionary<Type, int> cost = new Dictionary<Type, int>()
    {
        {Type.Buggy,100 },
        {Type.Bigfoot,200 },
        {Type.Truck,300 },
        {Type.Alien,500 }
    };

    public Type vehicleType;
    public string vehicleName;

    public float nitroPower, nitroTimer, rechargeNitro;
    public float resetTime;

    //Tipo de tracción en las ruedas
    public WheelDriveType wheelDriveType = WheelDriveType.RWD;
    public float handBrakeForce, brakeForce;
    public float torque, topSpeed, topReverseSpeed, downForce, stuckForceVehicle;
    public float minSteerAngle, maxSteerAngle;
    //Rear Wheels SidewaysFriction: mayor a 0 --> menor deslizamiento
    public float minSidewaysFriction, maxSidewaysFriction;
    public bool stuckToTheFloor;


    

}
