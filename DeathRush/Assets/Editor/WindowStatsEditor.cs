using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Threading;

public class WindowStatsEditor : EditorWindow
{
    private GameObject _vehicleContainer;
    private List<Vehicle> _vehicleList = new List<Vehicle>();
    private Vector2 _scrollPos;

    [MenuItem("Deathrush/Vehicle stats editor")]
    static void CreateWindow()
    {
        ((WindowStatsEditor)GetWindow(typeof(WindowStatsEditor))).Show();
    }

    void OnGUI()
    {
        _vehicleContainer = (GameObject)EditorGUILayout.ObjectField("Vehicle Container", _vehicleContainer, typeof(GameObject));
        if (_vehicleContainer == null)
        {
            EditorGUILayout.HelpBox("No container selected", MessageType.Warning);
            return;
        }
        else if (_vehicleContainer.tag != K.TAG_VEHICLES)
        {
            EditorGUILayout.HelpBox("Wrong gameobject selected", MessageType.Error);
            return;
        }
        _vehicleList.Clear();
        _vehicleList.AddRange(_vehicleContainer.GetComponentsInChildren<Vehicle>());
        EditorGUILayout.Space();
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height - 25));
        EditorGUILayout.BeginHorizontal();
        foreach (var vehicle in _vehicleList)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            string prefabPath = AssetDatabase.GetAssetPath(PrefabUtility.GetPrefabParent(vehicle.gameObject));
            GameObject go = (GameObject)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
            Texture2D tex = AssetPreview.GetAssetPreview(go);
            if (tex) GUI.DrawTexture(GUILayoutUtility.GetRect(200, 200, 200, 200), tex, ScaleMode.ScaleToFit);
            string vName = vehicle.vehicleName.ToUpper();
            var isPlayer = vehicle.GetComponent<InputControllerPlayer>();
            var c = GUI.color;
            GUI.color = isPlayer ? Color.green:Color.red;
            EditorGUILayout.LabelField(vName+(isPlayer?"(Player)":"(IA)"), EditorStyles.centeredGreyMiniLabel);
            GUI.color = c;
            EditorGUILayout.Space();
            vehicle.vehicleName = EditorGUILayout.TextField("Vehicle Name", vehicle.vehicleName);
            var vd = vehicle.GetComponent<VehicleData>();
            vd.maxLife = EditorGUILayout.FloatField("Max Life", vd.maxLife);
            vd.currentLife = EditorGUILayout.FloatField("Current Life", vd.currentLife);
            vehicle.vehicleVars.topSpeed = EditorGUILayout.FloatField("Top Speed", vehicle.vehicleVars.topSpeed);
            vehicle.vehicleVars.downForce = EditorGUILayout.FloatField("Down Force", vehicle.vehicleVars.downForce);
            vehicle.vehicleVars.torque = EditorGUILayout.FloatField("Max Motor Force", vehicle.vehicleVars.torque);
            vehicle.vehicleVars.brakeForce = EditorGUILayout.FloatField("Max Brake Force", vehicle.vehicleVars.brakeForce);
            vehicle.vehicleVars.maxSteerAngle = EditorGUILayout.FloatField("Maximun Turn Angle", vehicle.vehicleVars.maxSteerAngle);
            vehicle.vehicleVars.minSteerAngle = EditorGUILayout.FloatField("Minimun Turn Angle", vehicle.vehicleVars.minSteerAngle);
            vehicle.vehicleVars.nitroPower = EditorGUILayout.FloatField("Nitro Power", vehicle.vehicleVars.nitroPower);
            ShowSuspension(vehicle);
            FixValues(vehicle);
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
        Repaint();
    }

    private void ShowSuspension(Vehicle v)
    {
        var suspArray = v.GetComponentsInChildren<Suspension>();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("Suspension", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Left", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.LabelField("Right", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Front", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        EditorGUILayout.BeginVertical();
        suspArray[0].springConstant = EditorGUILayout.FloatField("Spring Constant", suspArray[0].springConstant);
        suspArray[0].damperConstant = EditorGUILayout.FloatField("Damper Constant", suspArray[0].damperConstant);
        suspArray[0].restLenght = EditorGUILayout.FloatField("Rest Lenght", suspArray[0].restLenght);
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        suspArray[1].springConstant = EditorGUILayout.FloatField("Spring Constant", suspArray[1].springConstant);
        suspArray[1].damperConstant = EditorGUILayout.FloatField("Damper Constant", suspArray[1].damperConstant);
        suspArray[1].restLenght = EditorGUILayout.FloatField("Rest Lenght", suspArray[1].restLenght);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Back", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        EditorGUILayout.BeginVertical();
        suspArray[3].springConstant = EditorGUILayout.FloatField("Spring Constant", suspArray[3].springConstant);
        suspArray[3].damperConstant = EditorGUILayout.FloatField("Damper Constant", suspArray[3].damperConstant);
        suspArray[3].restLenght = EditorGUILayout.FloatField("Rest Lenght", suspArray[3].restLenght);
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        suspArray[2].springConstant = EditorGUILayout.FloatField("Spring Constant", suspArray[2].springConstant);
        suspArray[2].damperConstant = EditorGUILayout.FloatField("Damper Constant", suspArray[2].damperConstant);
        suspArray[2].restLenght = EditorGUILayout.FloatField("Rest Lenght", suspArray[2].restLenght);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void FixValues(Vehicle v)
    {
        var vd = v.GetComponent<VehicleData>();
        if (vd.maxLife < 0) vd.maxLife = 0;
        if (vd.currentLife < 0) vd.currentLife = 0;
        if (vd.currentLife > vd.maxLife) vd.currentLife = vd.maxLife;
        if (v.vehicleVars.topSpeed < 0) v.vehicleVars.topSpeed = 0;
        if (v.vehicleVars.downForce < 0) v.vehicleVars.downForce = 0;
        if (v.vehicleVars.torque < 0) v.vehicleVars.torque = 0;
        if (v.vehicleVars.brakeForce < 0) v.vehicleVars.brakeForce = 0;
        if (v.vehicleVars.maxSteerAngle < 0) v.vehicleVars.maxSteerAngle = 0;
        if (v.vehicleVars.minSteerAngle < 0) v.vehicleVars.minSteerAngle = 0;
        if (v.vehicleVars.nitroPower < 0) v.vehicleVars.nitroPower = 0;
        foreach (var suspension in v.GetComponentsInChildren<Suspension>())
        {
            if (suspension.springConstant < 0) suspension.springConstant = 0;
            if (suspension.damperConstant < 0) suspension.damperConstant = 0;
            if (suspension.restLenght < 0) suspension.restLenght = 0;
        }
    }
}
