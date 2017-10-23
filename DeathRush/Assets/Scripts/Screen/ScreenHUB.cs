using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class ScreenHUB : ScreenView
{
    public event Action OnStatsSelect = delegate { };

    public List<ButtonRacerInfo> racerButtons;
    public VehicleExhibition exhibition;
    public Text textRepairAmount;
    public Button stats;

    private PlayerData _playerData;

    private void OnEnable()
    {
        cameraMenu.setMount(cameraMenu.hubMount);
        _playerData = PlayerData.instance;
        InitButtons();
        ShowSelectedInfo();
    }

    private void InitButtons()
    {
        HideButtons();
        for (int i = 0; i < _playerData.racerList.Count; i++)
        {
            racerButtons[i].UpdateInfo(_playerData.racerList[i]);
        }
    }

    protected override void Update()
    {
        var racer = _playerData.GetSelectedRacer();
        if (racer == null) return;
        textRepairAmount.text = "Vehicle's state: " + racer.currentLife + "/" + racer.maxLife;
    }

    private void HideButtons()
    {
        foreach (var btn in racerButtons)
        {
            btn.selectedRacerInfo.gameObject.SetActive(false);
            btn.gameObject.SetActive(false);
        }
        stats.gameObject.SetActive(false);
    }

    private void ShowSelectedInfo()
    {
        if (_playerData.selectedRacer < 0)
            if (_playerData.racerList.Count > 0) _playerData.selectedRacer = 0;
            else
            {
                exhibition.ExhibitVehicle(null);
                return;
            }
        foreach (var btn in racerButtons)
        {
            btn.selectedRacerInfo.gameObject.SetActive(false);
        }
        racerButtons[_playerData.selectedRacer].selectedRacerInfo.gameObject.SetActive(true);
        var aux = racerButtons[_playerData.selectedRacer].selectedRacerInfo.transform.parent.localPosition;
        aux.x -= 150;
        aux.y += 70;
        stats.transform.localPosition = aux;
        stats.gameObject.SetActive(true);
        exhibition.ExhibitVehicle(_playerData.racerList[_playerData.selectedRacer].racerVehicle);
    }

    public void OnSelectRacer1()
    {
        _playerData.selectedRacer = 0;
        ShowSelectedInfo();
    }

    public void OnSelectRacer2()
    {
        _playerData.selectedRacer = 1;
        ShowSelectedInfo();
    }

    public void OnSelectRacer3()
    {
        _playerData.selectedRacer = 2;
        ShowSelectedInfo();
    }

    public void OnSelectRacer4()
    {
        _playerData.selectedRacer = 3;
        ShowSelectedInfo();
    }

    public void OnSelectRacer5()
    {
        _playerData.selectedRacer = 4;
        ShowSelectedInfo();
    }

    public void OnStatsButton()
    {
        OnStatsSelect();
    }

    public void OnRepairButton()
    {
        var racer = _playerData.GetSelectedRacer();
        if (racer == null) return;
        if (racer.currentLife != racer.maxLife && _playerData.resources >= 10)
        {
            _playerData.resources -= 10;
        }
    }
}
