using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenWeaponsShop : ScreenView
{
    private List<GaragePurchaseButtons> _listGarageButtons = new List<GaragePurchaseButtons>();
    private PlayerData _playerData;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
        InitButtons();

    }
    protected override void Update()
    {

    }
    private void InitButtons()
    {
        _listGarageButtons.Clear();
        _listGarageButtons.AddRange(GetComponentsInChildren<GaragePurchaseButtons>());
        foreach (var button in _listGarageButtons)
        {
            button.GetComponent<Image>().color = Color.white;

            if (_playerData.unlockedWeapons.Contains(button.weaponType))
            {
                button.GetComponent<Image>().color = Color.green;
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
    public void OnPurchaseButton(GaragePurchaseButtons btn)
    {
        if (!_playerData.unlockedWeapons.Contains(btn.weaponType))
        {
            _playerData.unlockedWeapons.Add(btn.weaponType);
            btn.GetComponent<Image>().color = Color.green;
        }
    }
}
