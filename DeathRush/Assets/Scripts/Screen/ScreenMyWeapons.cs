using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMyWeapons : MonoBehaviour
{
    public PlayerData _playerData { get; private set; }

    public GameObject[] myWeapons;

    private void Awake()
    {
        //   cameraMenu.setMount(cameraMenu.myWeaponsMount);
        _playerData = PlayerData.instance;
    }
    protected void Update()
    {
        if (_playerData == null)
        {
            _playerData = PlayerData.instance;
            return;
        }

        foreach (var wp in myWeapons)
        {
            if (!_playerData.unlockedWeapons.Contains(wp.GetComponentInChildren<PointReference>().weapon)) wp.SetActive(false);
            else wp.SetActive(true);
        }
    }


    public void HandlePrimaryWeapon(Weapon.Type wt)
    {
        _playerData.racerList[_playerData.selectedRacer].equippedPrimaryWeapon = wt;
    }

    public void HandleSecondaryWeapon(Weapon.Type wt)
    {
        _playerData.racerList[_playerData.selectedRacer].equippedSecondaryWeapon = wt;
    }

    public void HandleGadget(Weapon.Type wt)
    {
        _playerData.racerList[_playerData.selectedRacer].equippedGadget = wt;
    }

}
