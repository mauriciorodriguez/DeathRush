using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScreenGarage : ScreenView
{
    public Transform vehicleShowSpawnPoint;
    public Button buyCarButton, leftButton, rightButton;

    private PlayerData _playerData;
    private Vehicle _vehicleToExhibit;
    private List<GaragePurchaseButtons> _listGarageButtons = new List<GaragePurchaseButtons>();
    private List<VehicleVars.Type> _vehiclesToShow = new List<VehicleVars.Type> { VehicleVars.Type.Buggy, VehicleVars.Type.Bigfoot, VehicleVars.Type.Truck, VehicleVars.Type.Alien };
    private int _vehiclesToShowPosition;

    private void OnEnable()
    {
        cameraMenu.setMount(cameraMenu.garageMount);
        _playerData = PlayerData.instance;
        ExhibitVehicle();
        _vehiclesToShowPosition = _vehiclesToShow.IndexOf(_playerData.racerList[_playerData.selectedRacer].racerVehicleType);
        InitButtons();
    }

    private void InitButtons()
    {
        if (!_playerData.racerList[_playerData.selectedRacer].unlockedVehicles.Contains(_vehiclesToShow[_vehiclesToShowPosition]))
        {
            buyCarButton.gameObject.SetActive(true);
            float percentageOfPrice = 1;
            if (_playerData.racerList[_playerData.selectedRacer].racerClass.classType == Classes.Type.Technician)
            {
                if (_playerData.racerList[_playerData.selectedRacer].unlockedTierOne == Classes.TypeTierOne.BargainDriver) percentageOfPrice = .6f;
                else percentageOfPrice = .8f;
            }
            buyCarButton.GetComponentInChildren<Text>().text = "$" + (VehicleVars.cost[_vehiclesToShow[_vehiclesToShowPosition]] * percentageOfPrice);
        }
        else buyCarButton.gameObject.SetActive(false);
        _listGarageButtons.Clear();
        _listGarageButtons.AddRange(GetComponentsInChildren<GaragePurchaseButtons>());
        foreach (var button in _listGarageButtons)
        {
            button.GetComponent<Image>().color = Color.white;

            if (_playerData.unlockedWeapons.Contains(button.weaponType))
            {
                button.GetComponent<Image>().color = Color.yellow;
            }
            if (_playerData.racerList[_playerData.selectedRacer].equippedPrimaryWeapon == button.weaponType ||
                 _playerData.racerList[_playerData.selectedRacer].equippedSecondaryWeapon == button.weaponType ||
                 _playerData.racerList[_playerData.selectedRacer].equippedGadget == button.weaponType
                )
            {
                button.GetComponent<Image>().color = Color.green;
            }
        }
    }

    public void OnChangeCar(int i)
    {
        _vehiclesToShowPosition += i;
        int vehiclesToShowMax = _playerData.playerUpgrades.Contains(Upgrade.Type.AlienVehicle) ? _vehiclesToShow.Count - 1 : _vehiclesToShow.Count - 2;
        if (_vehiclesToShowPosition > vehiclesToShowMax) _vehiclesToShowPosition = 0;
        if (_vehiclesToShowPosition < 0) _vehiclesToShowPosition = _vehiclesToShow.Count - 1;
        ExhibitVehicle();
        InitButtons();
        if (_playerData.racerList[_playerData.selectedRacer].unlockedVehicles.Contains(_vehiclesToShow[_vehiclesToShowPosition]))
        {
            _playerData.racerList[_playerData.selectedRacer].racerVehicleType = _vehiclesToShow[_vehiclesToShowPosition];
            _playerData.racerList[_playerData.selectedRacer].CompleateLoadInit();
        }
    }

    public void OnBuyCarButton()
    {
        int cost = int.Parse(buyCarButton.GetComponentInChildren<Text>().text.Remove(0, 1));
        if (cost <= _playerData.resources)
        {
            buyCarButton.gameObject.SetActive(false);
            _playerData.racerList[_playerData.selectedRacer].unlockedVehicles.Add(_vehiclesToShow[_vehiclesToShowPosition]);
            _playerData.racerList[_playerData.selectedRacer].racerVehicleType = _vehiclesToShow[_vehiclesToShowPosition];
            _playerData.resources -= cost;
            _playerData.racerList[_playerData.selectedRacer].CompleateLoadInit();
        }
    }

    public void ExhibitVehicle()
    {
        if (_vehicleToExhibit) Destroy(_vehicleToExhibit.gameObject);
        var vType = _vehiclesToShow[_vehiclesToShowPosition];
        _vehicleToExhibit = Instantiate(VehicleVars.prefabsDict[vType]()).GetComponent<Vehicle>();
        _vehicleToExhibit.vehicleVars.vehicleType = vType;
        _vehicleToExhibit.InstantiateWeapons(_playerData.racerList[_playerData.selectedRacer].equippedPrimaryWeapon, _playerData.racerList[_playerData.selectedRacer].equippedSecondaryWeapon, _playerData.racerList[_playerData.selectedRacer].equippedGadget, true);
        _vehicleToExhibit.EnableComponents(false);
        _vehicleToExhibit.transform.parent = vehicleShowSpawnPoint;
        _vehicleToExhibit.transform.rotation = vehicleShowSpawnPoint.rotation;
        _vehicleToExhibit.transform.position = vehicleShowSpawnPoint.position;
        _vehicleToExhibit.transform.localScale = Vector3.one * 45;
    }

    public void OnPurchaseButton(GaragePurchaseButtons btn)
    {
        HandleButtonClicked(btn.weaponType);
    }

    private void HandleButtonClicked(Weapon.Type wt)
    {
        switch (wt)
        {
            case Weapon.Type.Turret:
                HandlePrimaryWeapon(wt);
                break;
            case Weapon.Type.MissileLuncher:
                HandlePrimaryWeapon(wt);
                break;
            case Weapon.Type.SawLauncher:
                HandlePrimaryWeapon(wt);
                break;
            case Weapon.Type.LaserBeam:
                HandlePrimaryWeapon(wt);
                break;
            case Weapon.Type.MolotovLauncher:
                HandleSecondaryWeapon(wt);
                break;
            case Weapon.Type.LockLauncher:
                HandleSecondaryWeapon(wt);
                break;
            case Weapon.Type.FreezeRay:
                HandleSecondaryWeapon(wt);
                break;
            case Weapon.Type.FlameThrower:
                HandleSecondaryWeapon(wt);
                break;
            case Weapon.Type.Mines:
                HandleGadget(wt);
                break;
            case Weapon.Type.Oil:
                HandleGadget(wt);
                break;
            case Weapon.Type.ElectromagneticMine:
                HandleGadget(wt);
                break;
            case Weapon.Type.CombatDrone:
                HandleGadget(wt);
                break;
            default:
                break;
        }
        if (!_playerData.unlockedWeapons.Contains(wt))
        {
            _playerData.unlockedWeapons.Add(wt);
        }
    }

    private void HandlePrimaryWeapon(Weapon.Type wt)
    {
        _playerData.racerList[_playerData.selectedRacer].equippedPrimaryWeapon = wt;
        ExhibitVehicle();
        InitButtons();
    }

    private void HandleSecondaryWeapon(Weapon.Type wt)
    {
        _playerData.racerList[_playerData.selectedRacer].equippedSecondaryWeapon = wt;
        ExhibitVehicle();
        InitButtons();
    }

    private void HandleGadget(Weapon.Type wt)
    {
        _playerData.racerList[_playerData.selectedRacer].equippedGadget = wt;
        ExhibitVehicle();
        InitButtons();
    }
}
