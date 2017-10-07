using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(VehiclePlayerController)), CanEditMultipleObjects]
public class VPCEditor : Editor
{
    private VehiclePlayerController _target;
    private Texture2D wheelIcon;
    private Texture2D engineIcon;
    private Texture2D generalConfigIcon;
    private Texture2D weaponIcon;

    private bool wheelSettings;
    private bool engineSettings;
    private bool generalConfig;
    private bool weaponSettings;

    private SerializedObject soVehicleVars;
    void OnEnable()
    {
        _target = (VehiclePlayerController)target;

        wheelIcon = Resources.Load("Icons/VCIcons/WheelIcon", typeof(Texture2D)) as Texture2D;
        engineIcon = Resources.Load("Icons/VCIcons/EngineIcon", typeof(Texture2D)) as Texture2D;
        generalConfigIcon = Resources.Load("Icons/VCIcons/GeneralConfigIcon", typeof(Texture2D)) as Texture2D;
        weaponIcon = Resources.Load("Icons/VCIcons/WeaponIcon", typeof(Texture2D)) as Texture2D;

    }

    public override void OnInspectorGUI()
    {
     //   DrawDefaultInspector();

        _target.vehicleVars = (VehicleVars)EditorGUILayout.ObjectField("VehicleVars: ", _target.vehicleVars, typeof(VehicleVars), true);
        
        if (_target.vehicleVars == null) return;

        ShowValues();
        FixValues();
        Repaint();

        if (GUI.changed) Undo.RegisterUndo(_target, "VPC Change");

    }

    private void ShowValues()
    {
        serializedObject.Update();

        
        if (_target.vehicleVars != null) soVehicleVars = new SerializedObject(_target.vehicleVars);
       // SerializedProperty vehiclesPlayer = so.FindProperty("vehicleData");

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        if (generalConfig) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(generalConfigIcon)) generalConfig = !generalConfig;

        if (wheelSettings) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(wheelIcon)) wheelSettings = !wheelSettings;

        if (engineSettings) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(engineIcon)) engineSettings = !engineSettings;

        if (weaponSettings) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(weaponIcon)) weaponSettings = !weaponSettings;


        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.gray;

        if (generalConfig)
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("General Settings");
            GUI.color = Color.white;

            _target.vehicleVars.vehicleType = (VehicleVars.Type)EditorGUILayout.EnumPopup("Vehicle Type", _target.vehicleVars.vehicleType);
            _target.name = EditorGUILayout.TextField("Name", _target.name);
            _target.vehicleVars.downForce = EditorGUILayout.FloatField("Down Force", _target.vehicleVars.downForce);
            _target.vehicleVars.stuckForceVehicle = EditorGUILayout.FloatField("Stuck Force", _target.vehicleVars.stuckForceVehicle);
            _target.vehicleVars.stuckToTheFloor = EditorGUILayout.Toggle("Stuck To The Floor", _target.vehicleVars.stuckToTheFloor);
            _target.centerOfMass = EditorGUILayout.Vector3Field("Center Of Mass", _target.centerOfMass);
            _target.vehicleVars.resetTime = EditorGUILayout.FloatField("Reset Time", _target.vehicleVars.resetTime);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("backDust"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("debugInput"), true);
        }

        if (wheelSettings)
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("Wheel Settings");
            GUI.color = Color.white;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelMeshList"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("wheelColliderList"), true);
            _target.vehicleVars.brakeForce = EditorGUILayout.FloatField("Brake Force", _target.vehicleVars.brakeForce);
            _target.vehicleVars.handBrakeForce = EditorGUILayout.FloatField("Hand Brake Force", _target.vehicleVars.handBrakeForce);
            _target.vehicleVars.minSteerAngle = EditorGUILayout.FloatField("Min Steer Angle", _target.vehicleVars.minSteerAngle);
            _target.vehicleVars.maxSteerAngle = EditorGUILayout.FloatField("Max Steer Angle", _target.vehicleVars.maxSteerAngle);
            _target.vehicleVars.minSidewaysFriction = EditorGUILayout.FloatField("Min Sideways Friction", _target.vehicleVars.minSidewaysFriction);
            _target.vehicleVars.maxSidewaysFriction = EditorGUILayout.FloatField("Max Sideways Friction", _target.vehicleVars.maxSidewaysFriction);

        }

        if (engineSettings)
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("Engine Settings");
            GUI.color = Color.white;

            _target.vehicleVars.wheelDriveType = (WheelDriveType)EditorGUILayout.EnumPopup("Wheel Drive Type", _target.vehicleVars.wheelDriveType);
            _target.vehicleVars.torque = EditorGUILayout.FloatField("Torque", _target.vehicleVars.torque);
            _target.vehicleVars.topSpeed = EditorGUILayout.FloatField("Top Speed", _target.vehicleVars.topSpeed);
            _target.vehicleVars.topReverseSpeed = EditorGUILayout.FloatField("Top Reverse Speed", _target.vehicleVars.topReverseSpeed);
            _target.vehicleVars.nitroPower = EditorGUILayout.FloatField("Nitro Power", _target.vehicleVars.nitroPower);
            _target.vehicleVars.nitroTimer = EditorGUILayout.FloatField("Nitro Timer", _target.vehicleVars.nitroTimer);
            _target.vehicleVars.rechargeNitro = EditorGUILayout.FloatField("Recharge Nitro", _target.vehicleVars.rechargeNitro);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("totalGears"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("visualNitro"), true);
        }

        if (weaponSettings)
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("Weapon Settings");
            GUI.color = Color.white;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("primaryWeaponPlaceholder"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("secondaryWeaponPlaceholder"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("gadgetPlaceholder"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponManager"), true);
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUI.changed) EditorUtility.SetDirty(_target);

        serializedObject.ApplyModifiedProperties();
    }
    private void FixValues()
    {
        if (_target.vehicleVars.topSpeed < 0) _target.vehicleVars.topSpeed = 0;
        if (_target.vehicleVars.downForce < 0) _target.vehicleVars.downForce = 0;
        if (_target.vehicleVars.torque < 0) _target.vehicleVars.torque = 0;
        if (_target.vehicleVars.brakeForce < 0) _target.vehicleVars.brakeForce = 0;
        if (_target.vehicleVars.maxSteerAngle < 0) _target.vehicleVars.maxSteerAngle = 0;
        if (_target.vehicleVars.minSteerAngle < 0) _target.vehicleVars.minSteerAngle = 0;
        if (_target.vehicleVars.nitroPower < 0) _target.vehicleVars.nitroPower = 0;
    }
}
