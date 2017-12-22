using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGarage : MonoBehaviour
{
    public GameObject[] vehicles;
    public PlayerData _playerData { get; private set; }
    public VehicleExhibition vehicleExhibition;
    private int _selectedRacer = -1;

    private void Awake()
    {
        _playerData = PlayerData.instance;
    }

    void Update ()
    {
        if (_playerData == null || _playerData.racerList.Count == 0) return;

        UpdateGarage();
    }

    public void SelectVehicle(VehicleVars.Type vhtype)
    {
        _playerData = PlayerData.instance;

        _playerData.racerList[_playerData.selectedRacer].racerVehicleType = vhtype;
        _playerData.racerList[_playerData.selectedRacer].CompleateLoadInit();

        vehicleExhibition.ExhibitVehicle(_playerData.racerList[_playerData.selectedRacer].racerVehicle);

        UpdateGarage();

        foreach (var vh in vehicles)
        {
            if (vh.GetComponent<Vehicle>().vehicleVars.vehicleType == vhtype) vh.SetActive(false);
            else if(_playerData.racerList[_playerData.selectedRacer].unlockedVehicles.Contains(vh.GetComponent<Vehicle>().vehicleVars.vehicleType)) vh.SetActive(true);
        }
    }
    private void UpdateGarage()
    {
        if (_playerData.selectedRacer != _selectedRacer && _playerData.selectedRacer != -1)
        {
            foreach (var vh in vehicles)
            {
                vh.SetActive(false);
                if (_playerData.racerList[_playerData.selectedRacer].unlockedVehicles.Contains(vh.GetComponent<Vehicle>().vehicleVars.vehicleType))
                {
                    if (_playerData.racerList[_playerData.selectedRacer].racerVehicleType == vh.GetComponent<Vehicle>().vehicleVars.vehicleType) vh.SetActive(false);
                    else vh.SetActive(true);
                }
            }
            _selectedRacer = _playerData.selectedRacer;
        }
    }


    public void EnableVehicle(VehicleVars.Type vhtype)
    {
        foreach (var vh in vehicles)
        {
            if(vh.GetComponent<Vehicle>().vehicleVars.vehicleType == vhtype) vh.SetActive(true);
        }
    }
}
