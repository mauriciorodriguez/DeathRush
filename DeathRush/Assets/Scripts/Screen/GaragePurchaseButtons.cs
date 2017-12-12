using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GaragePurchaseButtons : MonoBehaviour
{
    public Weapon.Type weaponType;
    public float weaponCost;

    private PlayerData _playerData;
    private int _technicianCost;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
        if (_playerData.racerList[_playerData.selectedRacer].racerClass.classType == Classes.Type.Technician)
        {
            float percentage = 0;
            if (_playerData.racerList[_playerData.selectedRacer].unlockedTierOne == Classes.TypeTierOne.BargainDriver)
            {
                percentage = .6f;
            }
            else
            {
                percentage = .8f;
            }
            _technicianCost = (int)(weaponCost * percentage);
            GetComponentInChildren<Text>().text = StringSplitter.Split(weaponType.ToString()) + "\n$" + _technicianCost;
            if (!_playerData.unlockedWeapons.Contains(weaponType) && _playerData.resources < _technicianCost)
            {
           //     GetComponent<Button>().interactable = false;
            }
            else
            {
            //    GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            GetComponentInChildren<Text>().text = StringSplitter.Split(weaponType.ToString()) + "\n$" + weaponCost;
            if (!_playerData.unlockedWeapons.Contains(weaponType) && _playerData.resources < weaponCost)
            {
                GetComponent<Button>().interactable = false;
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }
        }

            DisableLockedGadgets();

    }

    private void DisableLockedGadgets()
    {
        if (!_playerData.playerUpgrades.Contains(Upgrade.Type.SawLauncher) &&
            weaponType == Weapon.Type.SawLauncher) GetComponent<Button>().interactable = false;
        if (!_playerData.playerUpgrades.Contains(Upgrade.Type.Flamethrower) &&
           weaponType == Weapon.Type.FlameThrower) GetComponent<Button>().interactable = false;
        if (!_playerData.playerUpgrades.Contains(Upgrade.Type.Oil) &&
           weaponType == Weapon.Type.Oil) GetComponent<Button>().interactable = false;
        if (!_playerData.playerUpgrades.Contains(Upgrade.Type.Smoke) &&
           weaponType == Weapon.Type.Smoke) GetComponent<Button>().interactable = false;
        if (!_playerData.playerUpgrades.Contains(Upgrade.Type.Shield) &&
           weaponType == Weapon.Type.Shield) GetComponent<Button>().interactable = false;
        /* if (!_playerData.playerUpgrades.Contains(Upgrade.Type.CombatDrone) &&
            weaponType == Weapon.Type.) GetComponent<Button>().interactable = false;*/
        if (!_playerData.playerUpgrades.Contains(Upgrade.Type.DeathRay) &&
       weaponType == Weapon.Type.LaserBeam) GetComponent<Button>().interactable = false;
        if (!_playerData.playerUpgrades.Contains(Upgrade.Type.CrioBlast) &&
           weaponType == Weapon.Type.FreezeRay) GetComponent<Button>().interactable = false;
        if (!_playerData.playerUpgrades.Contains(Upgrade.Type.ElectromagneticMine) &&
           weaponType == Weapon.Type.ElectromagneticMine) GetComponent<Button>().interactable = false;
        if (!_playerData.playerUpgrades.Contains(Upgrade.Type.CombatDrone) &&
           weaponType == Weapon.Type.CombatDrone) GetComponent<Button>().interactable = false;


        if (_playerData.playerUpgrades.Contains(Upgrade.Type.SawLauncher) && weaponType == Weapon.Type.SawLauncher)
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().enabled = false;
        }
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.Flamethrower) && weaponType == Weapon.Type.FlameThrower)
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().enabled = false;
        }
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.Oil) && weaponType == Weapon.Type.Oil)
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().enabled = false;
        }
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.Smoke) && weaponType == Weapon.Type.Smoke)
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().enabled = false;
        }
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.Shield) && weaponType == Weapon.Type.Shield)
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().enabled = false;
        }
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.DeathRay) &&  weaponType == Weapon.Type.LaserBeam)
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().enabled = false;
        }
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.CrioBlast) && weaponType == Weapon.Type.FreezeRay)
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().enabled = false;
        }
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.ElectromagneticMine) && weaponType == Weapon.Type.ElectromagneticMine)
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().enabled = false;
        }
        if (_playerData.playerUpgrades.Contains(Upgrade.Type.CombatDrone) && weaponType == Weapon.Type.CombatDrone)
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Button>().enabled = false;
        }


        if (_playerData.resources > weaponCost) GetComponent<Button>().enabled = true;

    }
}
