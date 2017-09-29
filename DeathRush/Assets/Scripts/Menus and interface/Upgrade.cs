using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public enum Type
    {
        MediaControl,
        SawLauncher,
        Flamethrower,
        Sabotage,
        Oil,
        Smoke,
        BackupPlan,
        NextGenEngine,
        ElectromagneticMine,
        Shield,
        ExperimentalFuel,
        NuclearIgnition,
        SafetyMesures,
        CombatDrone,
        HybridWeapons,
        DeathRay,
        CrioBlast,
        NewAlloys,
        ColdFusion,
        Nanobots,
        AlienVehicle
    }

    public Type type;
    public string description;
    public Upgrade requiredUpgrade;
    public Upgrade requiredUpgrade2;
    public int cost;
    public bool activated;
    public bool doubleParent;
    public ScreenUpgrades screenUpgrades;

    private PlayerData _playerData;

    Image image;
    Color transparentColor;

    void Start()
    {
        //Debug.Log("Colorize"); 
        image = GetComponent<Image>();

        transparentColor = image.color;
        transparentColor.a = 0.2f;
    }

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
    }

    private void OnMouseEnter()
    {
        screenUpgrades.ShowTooltip(type.ToString(), description, cost);
    }

    private void OnMouseExit()
    {
        screenUpgrades.HideTooltip();
    }

    void OnMouseDown()
    {
        PurchaseUpgrade();
    }

    void PurchaseUpgrade()
    {
        if (doubleParent == false)
        {
            if (requiredUpgrade == null || requiredUpgrade.activated == true)
            {
                charge();
            }
        }
        else if (doubleParent == true)
        {
            if (requiredUpgrade.activated == true || requiredUpgrade2.activated == true)
            {
                charge();
            }
        }
    }

    void charge()
    {
        if (cost <= _playerData.resources && !_playerData.playerUpgrades.Contains(type))
        {
            _playerData.resources -= cost;
            activated = true;
            _playerData.playerUpgrades.Add(type);
            GetComponent<Image>().color = Color.gray;
            screenUpgrades.ShowUpgrades();
        }
    }
}
