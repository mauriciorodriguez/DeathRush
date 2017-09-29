using UnityEngine;
using UnityEditor;

public class ScriptableObjectsCreator
{
    [MenuItem("Assets/Create/Asset Data Base/VehiclePlayerData")]
    public static void CreateVehicleData()
    {
        ScriptableObjectUtility.CreateAsset<VehicleVars>();
    }

    [MenuItem("Assets/Create/Asset Data Base/VehiclesManagerData")]
    public static void CreateGameManagerData()
    {
      //  ScriptableObjectUtility.CreateAsset<VehiclesManagerData>();
    }

}