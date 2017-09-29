using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GarageManager : MonoBehaviour {

    public GameObject panelPrimary;
    public GameObject panelSide;
    public GameObject panelGadget;
    public GameObject buttonBlockGadgets;
    public Text resourcesText;
    public Text gadgetExpansion;
    public int costGadgetExpansion;
    public List<GameObject> vehiclesList;

    private PlayerData _playerData;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
    }

    void Awake()
    {
        for (int i = vehiclesList.Count - 1; i >= 0; i--)
        {
            if (i == PlayerPrefs.GetInt("MyVehicle")) vehiclesList[i].gameObject.SetActive(true);
            else vehiclesList[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
            resourcesText.text = "Resources: " + _playerData.resources;        
    }

    public void ReturnHub()
    {
        SceneManager.LoadScene((int)SCENES_NUMBER.HUB);
    }

    public void ActivePanelPrimary()
    {
        panelPrimary.SetActive(true);
        panelSide.SetActive(false);
        panelGadget.SetActive(false);
        buttonBlockGadgets.SetActive(false);
    }
    public void ActivePanelSide()
    {
        panelPrimary.SetActive(false);
        panelSide.SetActive(true);
        panelGadget.SetActive(false);
        buttonBlockGadgets.SetActive(false);
    }
    public void ActivePanelGadget()
    {
        panelPrimary.SetActive(false);
        panelSide.SetActive(false);

        /*if (PlayerPrefs.GetString("GadgetActive") == "false")
        {
            buttonBlockGadgets.SetActive(true);
            gadgetExpansion.text = "Unlock Gadgets cost: " + costGadgetExpansion;
        }
        else
            panelGadget.SetActive(true);*/
    }

    public void PurchaseGadget()
    {
        if (_playerData.resources > costGadgetExpansion)
        {
            _playerData.resources -= costGadgetExpansion;
            resourcesText.text = "Resources: " + _playerData.resources;
            //PlayerPrefs.SetInt("Resources", _playerData.resources);
            //PlayerPrefs.SetString("GadgetActive", "true");
            buttonBlockGadgets.SetActive(false);
            ActivePanelGadget();
        }
    }
}
