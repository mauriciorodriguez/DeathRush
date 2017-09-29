using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScreenNewGame : ScreenView
{
    public event Action OnContinue = delegate { };
    public event Action OnNewCampaign = delegate { };
    public event Action OnCredits = delegate { };
    public event Action OnOptions = delegate { };

    public GameObject continueButton;
    public GameObject playerPrefab;
    private bool _canContinue;

    private void OnEnable()
    {
        if (PlayerPrefs.GetString(K.PREFS_PLAYER_COUNTRY) != "") _canContinue = true;
        else _canContinue = false;
        continueButton.SetActive(_canContinue);
    }

    public void OnStartCampaignButton()
    {
        PlayerPrefs.DeleteAll();
        var pd = CreatePlayerData();
        pd.canHireNewRacers = true;
        pd.hiredRacers = 0;
        pd.resources = 500;
        GetComponentInParent<ScreenManagerNuevo>().playerData = pd;
        OnNewCampaign();
    }

    public void OnContinueButton()
    {
        var pd = CreatePlayerData();
        pd.LoadPlayer();
        GetComponentInParent<ScreenManagerNuevo>().playerData = pd;
        OnContinue();
    }

    private PlayerData CreatePlayerData()
    {
        GameObject go = PlayerData.instance ? PlayerData.instance.gameObject : Instantiate(playerPrefab);
        var pd = go.GetComponent<PlayerData>();
        pd.ResetData();
        return pd;
    }

    public void OnOptionsButton()
    {
        OnOptions();
    }

    public void OnCreditsButton()
    {
        OnCredits();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}