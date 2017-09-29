using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;

public class WindowDebugPlayerData : EditorWindow
{
    private PlayerData _pd;
    private Vector2 _windowScrollPos;
    private bool _advancedMode;
    private float _racerExperience;

    [MenuItem("Deathrush/Player Data Debug")]
    static void GetDebugWindow()
    {
        GetWindow<WindowDebugPlayerData>().Show();
    }

    private void OnGUI()
    {
        title = "Player Info";
        minSize = new Vector2(900, 400);
        maxSize = minSize;
        if (!Application.isPlaying) return;
        if (!_pd) _pd = (PlayerData)EditorGUILayout.ObjectField("Player Data", _pd, typeof(PlayerData));
        if (!_pd) return;
        _windowScrollPos = EditorGUILayout.BeginScrollView(_windowScrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height));
        ShowPlayerInfo();
        EditorGUILayout.EndScrollView();
        Repaint();
    }

    private void FixRacerValues(RacerData r)
    {
        if (r.maxLife < 0) r.maxLife = 0;
        r.currentLife = Mathf.Clamp(r.currentLife, 0, r.maxLife);
    }

    private void ShowPlayerInfo()
    {
        StartHorizontal();
        StartVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("Player country", StringSplitter.Split(_pd.countryType.ToString()));
        _pd.resources = EditorGUILayout.IntField("Resources", _pd.resources);
        StartHorizontal();
        StartVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("Unlocked upgrades", EditorStyles.centeredGreyMiniLabel);
        for (int i = 0; i < _pd.playerUpgrades.Count; i++)
        {
            EditorGUILayout.LabelField(StringSplitter.Split(_pd.playerUpgrades[i].ToString()));
        }
        EndVertical();
        StartVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("Unlocked weapons", EditorStyles.centeredGreyMiniLabel);
        for (int i = 0; i < _pd.unlockedWeapons.Count; i++)
        {
            EditorGUILayout.LabelField(_pd.unlockedWeapons[i].ToString());
        }
        EndVertical();
        EndHorizontal();
        foreach (var item in _pd.countryChaos.Keys.ToList())
        {
            _pd.countryChaos[item] = EditorGUILayout.IntSlider(item.ToString() + " chaos", _pd.countryChaos[item], 0, 100);
        }
        _advancedMode = EditorGUILayout.Toggle("Advanced mode", _advancedMode);
        if (_advancedMode)
        {
            _pd.racesCompleted = EditorGUILayout.IntField("Races Completed", _pd.racesCompleted);
            _pd.selectedRacer = EditorGUILayout.IntField("Selected racer", _pd.selectedRacer);
            _pd.canHireNewRacers = EditorGUILayout.Toggle("Can hire new racers", _pd.canHireNewRacers);
        }
        EndVertical();
        ShowRacerInfo();
        EndHorizontal();
    }

    private void ShowRacerInfo()
    {
        StartVertical();
        foreach (var racer in _pd.racerList)
        {
            StartVertical(EditorStyles.helpBox);
            StartHorizontal();
            EditorGUILayout.LabelField(racer.racerClass.classType + " - " + racer.racerLevelType.ToString(), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Level: " + racer.level);
            EndHorizontal();
            racer.racerName = EditorGUILayout.TextField("Racer name", racer.racerName);
            racer.racerVehicleType = (VehicleVars.Type)EditorGUILayout.EnumPopup("Vehicle type", racer.racerVehicleType);
            _racerExperience = racer.racerExp;
            _racerExperience = EditorGUILayout.FloatField("Experience", _racerExperience);
            if (_racerExperience != racer.racerExp) racer.AddExperience(_racerExperience, true);
            racer.currentLife = EditorGUILayout.FloatField("Current life", racer.currentLife);
            racer.maxLife = EditorGUILayout.FloatField("Max life", racer.maxLife);
            racer.equippedPrimaryWeapon = (Weapon.Type)EditorGUILayout.EnumPopup("Equipped primary", racer.equippedPrimaryWeapon);
            racer.equippedSecondaryWeapon = (Weapon.Type)EditorGUILayout.EnumPopup("Equipped primary", racer.equippedSecondaryWeapon);
            racer.equippedGadget = (Weapon.Type)EditorGUILayout.EnumPopup("Equipped primary", racer.equippedGadget);
            EndVertical();
            FixRacerValues(racer);
        }
        EndVertical();
    }

    private void StartHorizontal(GUIStyle style = null)
    {
        if (style != null) EditorGUILayout.BeginHorizontal(style);
        else EditorGUILayout.BeginHorizontal();
    }

    private void StartVertical(GUIStyle style = null)
    {
        if (style != null) EditorGUILayout.BeginVertical(style);
        else EditorGUILayout.BeginVertical();
    }

    private void EndHorizontal()
    {
        EditorGUILayout.EndHorizontal();
    }

    private void EndVertical()
    {
        EditorGUILayout.EndVertical();
    }
}
