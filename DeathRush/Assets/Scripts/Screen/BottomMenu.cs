using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class BottomMenu : ScreenView
{
    public event Action OnShowUpgrade = delegate { };
    public event Action OnShowChaosMap = delegate { };
    public event Action OnShowHUB = delegate { };
    public event Action OnShowGarage = delegate { };
    public event Action OnShowHireRacer = delegate { };
    public event Action OnShowSearchForRace = delegate { };
    public event Action OnGameOver = delegate { };

    public Text textResources;
    public Text textChaos;

    public Button hireButton, getResourcesButton;
    public int resourcesChaos, chaosResources;
    public ScreenChaosMap screenChaosMap;
    public List<Button> buttonList;

    private PlayerData _playerData;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
        if (_playerData.countryChaos[_playerData.countryType] + chaosResources < 100)
        {
            getResourcesButton.gameObject.SetActive(true);
            textChaos.text = "Chaos: " + _playerData.countryChaos[_playerData.countryType] + "%";
        }
        else getResourcesButton.gameObject.SetActive(false);
    }

    private void EnableButtons()
    {
        if (_playerData.racerList.Count == 0)
        {
            hireButton.GetComponent<Image>().color = Color.green;
            foreach (var btn in buttonList)
            {
                btn.enabled = false;
                btn.GetComponent<Image>().color = btn.colors.disabledColor;
            }
        }
        else
        {
            hireButton.GetComponent<Image>().color = Color.white;
            foreach (var btn in buttonList)
            {
                btn.enabled = true;
                btn.GetComponent<Image>().color = btn.colors.normalColor;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        textResources.text = "$ " + _playerData.resources;
        EnableButtons();

        if (Input.GetKeyDown(KeyCode.F11)) PlayerData.instance.resources = 9999999;
        bool canHireRacer = _playerData.racerList.Count > 0 ? true : _playerData.canHireNewRacers;
        if (_playerData.racerList.Count == 0 && !_playerData.canHireNewRacers)
        {
            foreach (var racer in _playerData.racersForHire)
            {
                if (racer.cost < _playerData.resources)
                {
                    canHireRacer = true;
                    break;
                }
            }
        }
        if (_playerData.countryChaos[_playerData.countryType] >= 100 ||
            _playerData.countryChaos.Keys.Where(x => x != _playerData.countryType).ToList().Aggregate(0, (a, c) => a += _playerData.countryChaos[c]) >= 500 ||
            !canHireRacer
            )
        {
            OnGameOver();
        }
    }

    public void OnGetResourcesButton()
    {
        _playerData.resources += resourcesChaos;
        _playerData.countryChaos[_playerData.countryType] += chaosResources;
        screenChaosMap.SetData();
        if (_playerData.countryChaos[_playerData.countryType] + chaosResources < 100) getResourcesButton.gameObject.SetActive(true);
        else getResourcesButton.gameObject.SetActive(false);

        //Update chaos
        textChaos.text = "Chaos: " + _playerData.countryChaos[_playerData.countryType] + "%";
    }

    public void BTNShowUpgrade()
    {
        OnShowUpgrade();
    }

    public void BTNShowChaosMap()
    {
        OnShowChaosMap();
    }

    public void BTNShowHUB()
    {
        OnShowHUB();
    }

    public void BTNShowGarage()
    {
        if (_playerData.selectedRacer != -1)
        {
            OnShowGarage();
            OnShowGarage();
        }
        else
        {
            OnShowHireRacer();
        }
    }

    public void BTNShowHireRacer()
    {
        OnShowHireRacer();
    }

    public void BTNSearchForRace()
    {
        if (_playerData.racerList.Count > 0) OnShowSearchForRace();
        else OnShowHireRacer();
    }
}
