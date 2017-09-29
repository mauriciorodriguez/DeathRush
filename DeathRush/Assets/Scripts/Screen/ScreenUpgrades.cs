using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScreenUpgrades : ScreenView
{
    public List<GameObject> upgradesList;

    private PlayerData _playerData;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
        ShowUpgrades();
    }

    public void ShowUpgrades()
    {
        foreach (var childList in upgradesList)
        {
            foreach (Transform child in childList.transform)
            {
                Upgrade upg = child.GetComponent<Upgrade>();
                if (_playerData.playerUpgrades.Contains(upg.type))
                {
                    child.GetComponent<Image>().color = Color.gray;
                    child.GetComponent<Upgrade>().activated = true;
                }
                else
                {
                    child.GetComponent<Image>().color = Color.white;
                    var c = child.GetComponent<Image>().color;
                    if (upg.cost > _playerData.resources) c.a = .1f;
                    else c.a = 1;
                    if (upg.doubleParent)
                    {
                        if ((upg.requiredUpgrade != null && _playerData.playerUpgrades.Contains(upg.requiredUpgrade.type)) ||
                        (upg.requiredUpgrade2 != null && _playerData.playerUpgrades.Contains(upg.requiredUpgrade2.type))) c.a = 1;
                        else c.a = .1f;
                    }
                    else
                    {
                        if ((upg.requiredUpgrade != null && _playerData.playerUpgrades.Contains(upg.requiredUpgrade.type)) ||
                            (upg.requiredUpgrade == null && upg.requiredUpgrade2 == null)) c.a = 1;
                        else c.a = .1f;
                    }
                    child.GetComponent<Image>().color = c;
                }
            }
        }
    }


}



