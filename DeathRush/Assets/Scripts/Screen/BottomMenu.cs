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
    public event Action OnShowMyWeapons = delegate { };
    public event Action OnShowVehiclesToBuy = delegate { };
    public event Action OnGameOver = delegate { };
    public event Action OnShowVehiclesShop = delegate { };
    public event Action OnShowWeaponsShop = delegate { };

    public Text textResources;
    public Text textChaos;

    public Button hireButton, getResourcesButton;
    public int resourcesChaos, chaosResources;
    public ScreenChaosMap screenChaosMap;
    public List<Button> buttonList;
    private int _index = -1;

    public List<GameObject> referencePoints;

    private PlayerData _playerData;
    public bool disableBottomMenu;

    public Text adviceText;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
        if (_playerData == null) return;
        if (_playerData.countryChaos[_playerData.countryType] + chaosResources < 100)
        {
            getResourcesButton.gameObject.SetActive(true);
            textChaos.text = "Chaos: " + _playerData.countryChaos[_playerData.countryType] + "%";
        }
        else getResourcesButton.gameObject.SetActive(false);
        _index = -1;
    }

    private void EnableButtons()
    {
        if (_playerData.racerList.Count == 0)
        {
            if (adviceText != null) adviceText.enabled = false;
            if (hireButton != null) hireButton.GetComponent<Image>().color = Color.yellow;
            foreach (var btn in buttonList)
            {
                btn.enabled = false;
                btn.GetComponent<Image>().color = btn.colors.disabledColor;
            }
            if (hireButton != null)
            {
                hireButton.enabled = true;
                hireButton.GetComponent<Image>().color = Color.yellow;
            }

            foreach (var refPoint in referencePoints) refPoint.GetComponent<SphereCollider>().enabled = false;
        }
        else
        {
            if (adviceText != null) adviceText.enabled = true;
            foreach (var refPoint in referencePoints) refPoint.GetComponent<SphereCollider>().enabled = true;
            for (int i = buttonList.Count - 1; i >= 0; i--)
            {
                if(i != _index)
                {
                    buttonList[i].enabled = true;
                    buttonList[i].GetComponent<Image>().color = buttonList[i].colors.normalColor;
                }
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        EnableButtons();

        if (disableBottomMenu) return;

        textResources.text = "$ " + _playerData.resources;
        if (Input.GetKeyDown(KeyCode.F11))
        {
            PlayerData.instance.resources = 9999999;
            PlayerData.instance.racesCompleted = 10;
        }
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

        InputChangeScreen();
    }

    private void InputChangeScreen()
    {
        if (_playerData.racerList.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.D))
        {
           if(_index != -1) buttonList[_index].GetComponent<Image>().color = Color.white;
           if (_index == 4) _index = -1;

            _index++;
            buttonList[_index].onClick.Invoke();
            buttonList[_index].GetComponent<Image>().color = Color.yellow;

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_index != 5 && _index != -1) buttonList[_index].GetComponent<Image>().color = Color.white;
            if (_index == -1 || _index == 0) _index = 5;

            _index--;
            buttonList[_index].onClick.Invoke();
            buttonList[_index].GetComponent<Image>().color = Color.yellow;
        }
    }

    public void OnGetResourcesButton()
    {
        if (_playerData.selectedRacer != -1)
        {
            _playerData.resources += resourcesChaos;
            _playerData.countryChaos[_playerData.countryType] += chaosResources;
            screenChaosMap.SetData();
            if (_playerData.countryChaos[_playerData.countryType] + chaosResources < 100) getResourcesButton.gameObject.SetActive(true);
            else getResourcesButton.gameObject.SetActive(false);

            //Update chaos
            textChaos.text = "Chaos: " + _playerData.countryChaos[_playerData.countryType] + "%";
        }
    }

    public void BTNShowWeapons(GameObject refPoint)
    {
        if (_playerData.selectedRacer != -1)
        {
            refPoint.SetActive(false);
            cameraMenu.setMount(cameraMenu.myWeaponsMount);
            assistRobot.setMount(assistRobot.arMyWeaponsMount);
            CheckStatusReferencePoints(refPoint);
            _index = 3;
        }
    }
    public void BTNShowUpgrade()
    {
        if (_playerData.selectedRacer != -1)
        {
            OnShowUpgrade();
            cameraMenu.setMount(cameraMenu.hirePilotMount);
            assistRobot.setMount(assistRobot.arHirePilotMount);
        }
    }

    public void BTNShowVehiclesShop()
    {
        if (_playerData.selectedRacer != -1)
        {
            OnShowVehiclesShop();
            cameraMenu.setMount(cameraMenu.hirePilotMount);
            assistRobot.setMount(assistRobot.arHirePilotMount);
        }
    }

    public void BTNShowWeaponsShop()
    {
        if (_playerData.selectedRacer != -1)
        {
            OnShowWeaponsShop();
            cameraMenu.setMount(cameraMenu.hirePilotMount);
            assistRobot.setMount(assistRobot.arHirePilotMount);
        }
    }

    public void BTNShowChaosMap()
    {
        if (_playerData.selectedRacer != -1)
        {
            OnShowChaosMap();
            cameraMenu.setMount(cameraMenu.hirePilotMount);
            assistRobot.setMount(assistRobot.arHirePilotMount);
            _index = 0;
        }
    }

    public void BTNShowHUB(GameObject refPoint)
    {
        if (_playerData.selectedRacer != -1)
        {
            refPoint.SetActive(false);
            //     OnShowHUB();
            cameraMenu.setMount(cameraMenu.hubMount);
            assistRobot.setMount(assistRobot.arHubMount);
            CheckStatusReferencePoints(refPoint);
            _index = 4;
        }
    }

    public void BTNShowGarage(GameObject refPoint)
    {
        refPoint.SetActive(false);
        if (_playerData.selectedRacer != -1)
        {
            cameraMenu.setMount(cameraMenu.garageMount);
            assistRobot.setMount(assistRobot.arGarageMount);
            _index = 1;
        }
        else
        {
            OnShowHireRacer();
        }
        CheckStatusReferencePoints(refPoint);
    }

    public void BTNShowHireRacer(GameObject refPoint)
    {
        refPoint.SetActive(false);
        OnShowHireRacer();
        CheckStatusReferencePoints(refPoint);
    }

    public void BTNSearchForRace(GameObject refPoint)
    {
        if (_playerData.selectedRacer != -1)
        {
            refPoint.SetActive(false);
            if (_playerData.racerList.Count > 0) OnShowSearchForRace();
            else OnShowHireRacer();
            CheckStatusReferencePoints(refPoint);
            _index = 2;
        }
    }

    private void CheckStatusReferencePoints(GameObject referencePoint)
    {
        foreach (var refPoint in referencePoints)
        {
            if (!refPoint.activeSelf && refPoint != referencePoint) refPoint.SetActive(true);
        }
    }

    public void OnMouseEnter(string title)
    {
        ShowTooltipTittle(title);
    }

    public void OnMouseExit()
    {
        HideTooltip();
    }
}
