using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ScreenChaosMap : ScreenView
{
    public List<Text> chaosTextList;
    public List<Country> countries;

    private PlayerData _playerData;

    void OnEnable()
    {
        SetData();
    }

    public void SetData()
    {
        _playerData = PlayerData.instance;
        foreach (var country in _playerData.countryChaos.Keys.ToList()) _playerData.countryChaos[country] = Mathf.Clamp(_playerData.countryChaos[country], 0, 100);
        foreach (var country in countries) country.chaosLevel = _playerData.countryChaos[country.countryNameEnum];
        for (int i = 0; i < countries.Count; i++)
        {
            if (countries[i].chaosLevel <= 30)
            {
                countries[i].GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (countries[i].chaosLevel <= 70)
            {
                countries[i].GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (countries[i].chaosLevel < 100)
            {
                countries[i].GetComponent<SpriteRenderer>().color = Color.red;
            }
            else countries[i].GetComponent<SpriteRenderer>().color = Color.black;
            chaosTextList[i].text = "%" + countries[i].chaosLevel;
        }
    }
}
