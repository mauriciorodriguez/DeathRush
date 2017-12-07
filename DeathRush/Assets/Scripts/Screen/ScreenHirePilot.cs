using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ScreenHirePilot : ScreenView
{
    public RacerDetailedInfoForHire racerDetailedInfo;
    private ButtonHireRacer _pilotHired;
    private PlayerData _playerData;
    private List<ButtonHireRacer> _hireButtons;


    public Sprite buggyIcon;
    public Sprite crusherIcon;
    public Sprite truckIcon;
    public Sprite alienIcon;

    public ScreenGarage screenGarage;

    private void OnEnable()
    {
        cameraMenu.setMount(cameraMenu.hirePilotMount);
        assistRobot.setMount(assistRobot.arHirePilotMount);
        if (!_playerData) _playerData = PlayerData.instance;
        _hireButtons = new List<ButtonHireRacer>();
        _hireButtons.AddRange(GetComponentsInChildren<ButtonHireRacer>(true));
        _pilotHired = null;
        InitRacers();
    }

    public void InitRacers()
    {
        foreach (var btn in _hireButtons) btn.gameObject.SetActive(false);
        if (_playerData.canHireNewRacers)
        {
            int posInHire = 0;
            _playerData.canHireNewRacers = false;
            _playerData.racersForHire.Clear();
            foreach (var btn in _hireButtons)
            {
                btn.gameObject.SetActive(true);
                btn.portrait.transform.localScale = Vector3.one * .5f;
                btn.portrait.Randomize();
                GameObject.FindGameObjectWithTag(K.TAG_PORTRAIT_BUILDER).GetComponent<PortraitBuilder>().Randomize(btn.portrait);
                btn.racerName.text = btn.portrait.racerName;
                btn.racerClass.text = btn.portrait.racerClass.ToString();
                btn.racerCost.text = "$" + btn.portrait.cost;

                if (btn.portrait.racerVehicle == VehicleVars.Type.Buggy)
                {
                    btn.vehicleIcon.sprite = buggyIcon;
                }
                else if (btn.portrait.racerVehicle == VehicleVars.Type.Bigfoot)
                {
                    btn.vehicleIcon.sprite = crusherIcon;
                }
                else if (btn.portrait.racerVehicle == VehicleVars.Type.Truck)
                {
                    btn.vehicleIcon.sprite = truckIcon;
                }
                else if (btn.portrait.racerVehicle == VehicleVars.Type.Alien)
                {
                    btn.vehicleIcon.sprite = alienIcon;
                }


                btn.portrait.positionInHire = posInHire;
                var data = btn.portrait;
                RacerData rd = new RacerData(data.racerName, data.gender, data.racerVehicle, data.racerClass, data.headNumber, data.hairNumber, data.extraNumber, data.hairColorNumber, data.skinColorNumber, data.maxLife, data.cost);
                rd.positionInHire = posInHire++;
                _playerData.racersForHire.Add(rd);

            }
        }
        else
        {
            for (int i = 0; i < _playerData.racersForHire.Count; i++)
            {
                int posInHire = _playerData.racersForHire[i].positionInHire;
                _hireButtons[posInHire].gameObject.SetActive(true);
                _hireButtons[posInHire].portrait.transform.localScale = Vector3.one * .5f;
                _hireButtons[posInHire].portrait.Build(_playerData.racersForHire[i]);
                _hireButtons[posInHire].racerName.text = _playerData.racersForHire[i].racerName;
                _hireButtons[posInHire].racerClass.text = _playerData.racersForHire[i].racerClass.ToString();
                _hireButtons[posInHire].racerCost.text = "$" + _playerData.racersForHire[i].cost;
            }
        }
    }

    public void OnHireRacer(ButtonHireRacer pilotHired)
    {
        _pilotHired = pilotHired;
        int cost = int.Parse(_pilotHired.racerCost.text.Remove(0, 1));
        if (cost <= _playerData.resources)
        {
            HandleHiredRacer(_pilotHired);
            _playerData.selectedRacer = _playerData.racerList.Count - 1;
            _playerData.resources -= cost;
            _playerData.hiredRacers++;

            screenGarage.EnableVehicle(_pilotHired.portrait.racerVehicle);
            screenGarage.SelectVehicle(_pilotHired.portrait.racerVehicle);
        }
    }

    private void HandleHiredRacer(ButtonHireRacer btn)
    {
        var racer = btn.portrait;
        var rd = new RacerData(racer.racerName, racer.gender, racer.racerVehicle, racer.racerClass, racer.headNumber, racer.hairNumber, racer.extraNumber, racer.hairColorNumber, racer.skinColorNumber, racer.maxLife);
        _playerData.AddRacer(rd);
        _playerData.racersForHire.RemoveAll(x => x.positionInHire == racer.positionInHire);
        btn.transform.parent.gameObject.SetActive(false);
    }

}
