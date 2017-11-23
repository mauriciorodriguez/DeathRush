using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGarage : MonoBehaviour
{
    public GameObject[] vehicles;
    private PlayerData _playerData;

    private void Awake()
    {
        _playerData = PlayerData.instance;
    }

    void Update ()
    {
		
	}

    public void EnableVehicle(VehicleVars.Type vhtype)
    {
        foreach (var vh in vehicles)
        {
            if(vh.GetComponent<Vehicle>().vehicleVars.vehicleType == vhtype) vh.SetActive(true);
        }
    }
}
