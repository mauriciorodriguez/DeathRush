using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleExhibition : MonoBehaviour
{
    public ScreenHUB screenHub;
    public BarsManager barsManager;

    private PlayerData _playerData;
    private Vehicle _vehicleToExhibit;
    public Transform[] vehiclesPosition;

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

        if(_vehicleToExhibit.vehicleVars.vehicleType == VehicleVars.Type.Buggy)
        {
            _vehicleToExhibit.transform.parent = vehiclesPosition[0];
            _vehicleToExhibit.transform.position = vehiclesPosition[0].position;
        }
        else if (_vehicleToExhibit.vehicleVars.vehicleType == VehicleVars.Type.Bigfoot)
        {
            _vehicleToExhibit.transform.parent = vehiclesPosition[1];
            _vehicleToExhibit.transform.position = vehiclesPosition[1].position;
        }
        else if (_vehicleToExhibit.vehicleVars.vehicleType == VehicleVars.Type.Truck)
        {
            _vehicleToExhibit.transform.parent = vehiclesPosition[2];
            _vehicleToExhibit.transform.position = vehiclesPosition[2].position;
        }
        else if (_vehicleToExhibit.vehicleVars.vehicleType == VehicleVars.Type.Alien)
        {
            _vehicleToExhibit.transform.parent = vehiclesPosition[3];
            _vehicleToExhibit.transform.position = vehiclesPosition[3].position;
        }

        barsManager.OnImplementation();
        barsManager.BarStats(_vehicleToExhibit.vehicleVars.vehicleType);
    }
}
