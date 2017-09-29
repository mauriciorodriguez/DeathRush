using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleExhibition : MonoBehaviour
{
    public ScreenHUB screenHub;
    public BarsManager barsManager;

    private PlayerData _playerData;
    private Vehicle _vehicleToExhibit;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
    }

    public void ExhibitVehicle(Vehicle vehicle)
    {
        if (!_playerData) _playerData = PlayerData.instance;
        if (!vehicle)
        {
            if (_vehicleToExhibit) Destroy(_vehicleToExhibit.gameObject);
            return;
        }
        if (_vehicleToExhibit) Destroy(_vehicleToExhibit.gameObject);
        _vehicleToExhibit = Instantiate(vehicle.gameObject).GetComponent<Vehicle>();
        _vehicleToExhibit.InstantiateWeapons(_playerData.racerList[_playerData.selectedRacer].equippedPrimaryWeapon, _playerData.racerList[_playerData.selectedRacer].equippedSecondaryWeapon, _playerData.racerList[_playerData.selectedRacer].equippedGadget,true);
        _vehicleToExhibit.EnableComponents(false);
        _vehicleToExhibit.transform.parent = transform;
        _vehicleToExhibit.transform.localPosition = Vector3.zero;
        _vehicleToExhibit.transform.rotation = transform.rotation;
        _vehicleToExhibit.transform.localScale = Vector3.one * 30;


        barsManager.OnImplementation();
        barsManager.BarStats(_vehicleToExhibit.vehicleVars.vehicleType);
    }
}
