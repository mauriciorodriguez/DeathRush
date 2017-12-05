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
        cameraMenu.setMount(cameraMenu.chaosMapMount);
        assistRobot.setMount(assistRobot.arChaosMapMount);
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
                countries[i].GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
            }
            else if (countries[i].chaosLevel <= 70)
            {
                countries[i].GetComponent<MeshRenderer>().sharedMaterial.color = Color.yellow;
            }
            else if (countries[i].chaosLevel < 100)
            {
                countries[i].GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
            }
            else countries[i].GetComponent<MeshRenderer>().sharedMaterial.color = Color.black;
            chaosTextList[i].text = "%" + countries[i].chaosLevel;

            if (_playerData.countryType == countries[i].countryNameEnum)
            {
                countries[i].GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_OpacityClip", 0.05f);
                countries[i].GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EdgeColor", Color.cyan);
            }
        }
    }
}
