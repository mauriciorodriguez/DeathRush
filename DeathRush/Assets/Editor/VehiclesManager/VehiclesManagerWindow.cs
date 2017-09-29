using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class VehiclesManagerWindow : EditorWindow
{
    private Texture2D _preview;
    public VehiclesManagerData vehiclesManagerData;
    private string _lastPath;
    private bool playerVehicles;
    private bool npcVehicles;

    private GameObject[] vpc;
    private GameObject[] viac;

    private Texture2D wheelIcon;
    private Texture2D engineIcon;
    private Texture2D generalConfigIcon;
    private Texture2D weaponIcon;

    private bool[] wheelSettings;
    private bool[] engineSettings;
    private bool[] generalConfig;
    private bool[] weaponSettings;

    private SerializedObject soVpcVars;
    private SerializedObject soVpc;

    private SerializedObject soViacVars;
    private SerializedObject soViac;

    private Rect[] windows;
    private Vector2[] scrollPos;
    private Vector2 scrollWindowPos;

    private List<int> lastIds;
    private bool filter;

    [MenuItem("Deathrush/Vehicles Manager")]

    static void CreateWindow()
    {
        ((VehiclesManagerWindow)GetWindow(typeof(VehiclesManagerWindow))).Show();
    }

    void OnEnable()
    {
         vehiclesManagerData = (VehiclesManagerData)AssetDatabase.LoadAssetAtPath(PlayerPrefs.GetString("vehicleManagerDataSaved"), typeof(ScriptableObject));
         lastIds = new List<int>();
    }

    void OnGUI()
    {
        scrollWindowPos = EditorGUILayout.BeginScrollView(scrollWindowPos,false,true, GUILayout.Width(position.width), GUILayout.Height(position.height));

        minSize = new Vector2(800, 599);
        maxSize = new Vector2(800, 600);

        ShowValues();
        FixValues();

        if (GUI.changed) Undo.RegisterUndo(this, "Vehicles Manager Change");

        for (int i = 278; i >= 0; i--) EditorGUILayout.Space();

        EditorGUILayout.EndScrollView();

        Repaint();
    }


    private void ShowValues()
    {
        EditorUtility.SetDirty(this);

        vehiclesManagerData = (VehiclesManagerData)EditorGUILayout.ObjectField("VehiclesManagerData: ", vehiclesManagerData, typeof(VehiclesManagerData), true);

        if (vehiclesManagerData != null)
        {
            vpc = vehiclesManagerData._vehiclesPlayerController.ToArray();
            viac = vehiclesManagerData._vehiclesIAController.ToArray();
            
            if (wheelIcon == null)
            {
                wheelIcon = Resources.Load("Icons/VCIcons/WheelIcon", typeof(Texture2D)) as Texture2D;
                engineIcon = Resources.Load("Icons/VCIcons/EngineIcon", typeof(Texture2D)) as Texture2D;
                generalConfigIcon = Resources.Load("Icons/VCIcons/GeneralConfigIcon", typeof(Texture2D)) as Texture2D;
                weaponIcon = Resources.Load("Icons/VCIcons/WeaponIcon", typeof(Texture2D)) as Texture2D;
            }
            
            var vehiclesManagerPath = AssetDatabase.GetAssetPath(vehiclesManagerData);

            if (vehiclesManagerPath != _lastPath)
            {
                PlayerPrefs.SetString("vehicleManagerDataSaved", vehiclesManagerPath);
                _lastPath = vehiclesManagerPath;
            }

            EditorGUILayout.BeginHorizontal();

            if (playerVehicles) GUI.backgroundColor = Color.gray;
            else GUI.backgroundColor = Color.white;
            if (GUILayout.Button("Player"))
            {
                playerVehicles = !playerVehicles;
                npcVehicles = false;

                wheelSettings = new bool[vpc.Length];
                engineSettings = new bool[vpc.Length];
                generalConfig = new bool[vpc.Length];
                weaponSettings = new bool[vpc.Length];
                windows = new Rect[vpc.Length];
                scrollPos = new Vector2[vpc.Length];
                lastIds.Clear();
                filter = false;

            }

            if (npcVehicles) GUI.backgroundColor = Color.gray;
            else GUI.backgroundColor = Color.white;
            if (GUILayout.Button("IA"))
            {
                npcVehicles = !npcVehicles;
                playerVehicles = false;

                wheelSettings = new bool[viac.Length];
                engineSettings = new bool[viac.Length];
                generalConfig = new bool[viac.Length];
                weaponSettings = new bool[viac.Length];

                windows = new Rect[viac.Length];
                scrollPos = new Vector2[viac.Length];
                lastIds.Clear();
                filter = false;
            }


            GUI.backgroundColor = Color.white;

            EditorGUILayout.EndHorizontal();

            if (playerVehicles)
            {
                BeginWindows();
                for (int i = 0; i < vpc.Length; i++) windows[i] = GUILayout.Window(i, windows[i], PlayerVehicles, vpc[i].name);
                EndWindows();
            }
            else if (npcVehicles)
            {
                BeginWindows();
                for (int i = 0; i < viac.Length; i++) windows[i] = GUILayout.Window(i, windows[i], IAVehicles, viac[i].name);
                EndWindows();
            }
        }

    }

    private void PlayerVehicles(int i)
    {
        if (i == 0 && lastIds.Contains(i))
        {
            lastIds.Clear();
            filter = true;
        }
        if (!lastIds.Contains(i) && !filter)
        {
            lastIds.Add(i);
            if (windows[i].position.y == 0)
            {
                if (i % 2 == 0)
                {
                    if (i == 0) windows[i] = new Rect(15, 50, 360, 240);
                    else windows[i] = new Rect(15, windows[i - 2].position.y + 277, 360, 240);

                }
                else
                {
                    if (i == 1) windows[i] = new Rect(400, 50, 360, 240);
                    else windows[i] = new Rect(400, windows[i - 2].position.y + 277, 360, 240);
                }
            }
        }

        filter = false;
        scrollPos[i] = EditorGUILayout.BeginScrollView(scrollPos[i], GUILayout.Width(360), GUILayout.Height(240));

        var vehicleController = vpc[i].GetComponentInChildren<VehiclePlayerController>();

        if (vehicleController != null)
        {
            soVpc = new SerializedObject(vehicleController);
            if (vehicleController.vehicleVars != null) soVpcVars = new SerializedObject(vehicleController.vehicleVars);
        }

        soVpcVars.Update();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.BeginHorizontal();


        if (generalConfig[i]) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(generalConfigIcon)) generalConfig[i] = !generalConfig[i];

        if (wheelSettings[i]) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(wheelIcon)) wheelSettings[i] = !wheelSettings[i];

        if (engineSettings[i]) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(engineIcon)) engineSettings[i] = !engineSettings[i];

        if (weaponSettings[i]) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(weaponIcon)) weaponSettings[i] = !weaponSettings[i];

        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.gray;

        if (generalConfig[i])
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("General Settings");
            GUI.color = Color.white;

            vehicleController.vehicleVars.vehicleType = (VehicleVars.Type)EditorGUILayout.EnumPopup("Vehicle Type", vehicleController.vehicleVars.vehicleType);
            vehicleController.name = EditorGUILayout.TextField("Name", vehicleController.name);
            vehicleController.vehicleVars.downForce = EditorGUILayout.FloatField("Down Force", vehicleController.vehicleVars.downForce);
            vehicleController.centerOfMass = EditorGUILayout.Vector3Field("Center Of Mass", vehicleController.centerOfMass);
            vehicleController.vehicleVars.resetTime = EditorGUILayout.FloatField("Reset Time", vehicleController.vehicleVars.resetTime);
            EditorGUILayout.PropertyField(soVpc.FindProperty("backDust"), true);
            EditorGUILayout.PropertyField(soVpc.FindProperty("debugInput"), true);
            vehicleController.vehicleVars.stuckToTheFloor = EditorGUILayout.Toggle("Stuck To The Floor", vehicleController.vehicleVars.stuckToTheFloor);
        }

        if (wheelSettings[i])
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("Wheel Settings");
            GUI.color = Color.white;

            EditorGUILayout.PropertyField(soVpc.FindProperty("wheelMeshList"), true);
            EditorGUILayout.PropertyField(soVpc.FindProperty("wheelColliderList"), true);
            vehicleController.vehicleVars.brakeForce = EditorGUILayout.FloatField("Brake Force", vehicleController.vehicleVars.brakeForce);
            vehicleController.vehicleVars.handBrakeForce = EditorGUILayout.FloatField("Hand Brake Force", vehicleController.vehicleVars.handBrakeForce);
            vehicleController.vehicleVars.minSteerAngle = EditorGUILayout.FloatField("Min Steer Angle", vehicleController.vehicleVars.minSteerAngle);
            vehicleController.vehicleVars.maxSteerAngle = EditorGUILayout.FloatField("Max Steer Angle", vehicleController.vehicleVars.maxSteerAngle);
            vehicleController.vehicleVars.minSidewaysFriction = EditorGUILayout.FloatField("Min Sideways Friction", vehicleController.vehicleVars.minSidewaysFriction);
            vehicleController.vehicleVars.maxSidewaysFriction = EditorGUILayout.FloatField("Max Sideways Friction", vehicleController.vehicleVars.maxSidewaysFriction);

        }

        if (engineSettings[i])
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("Engine Settings");
            GUI.color = Color.white;

            vehicleController.vehicleVars.wheelDriveType = (WheelDriveType)EditorGUILayout.EnumPopup("Wheel Drive Type", vehicleController.vehicleVars.wheelDriveType);
            vehicleController.vehicleVars.torque = EditorGUILayout.FloatField("Max Force", vehicleController.vehicleVars.torque);
            vehicleController.vehicleVars.topSpeed = EditorGUILayout.FloatField("Top Speed", vehicleController.vehicleVars.topSpeed);
            vehicleController.vehicleVars.topReverseSpeed = EditorGUILayout.FloatField("Top Reverse Speed", vehicleController.vehicleVars.topReverseSpeed);
            vehicleController.vehicleVars.nitroPower = EditorGUILayout.FloatField("Nitro Power", vehicleController.vehicleVars.nitroPower);
            vehicleController.vehicleVars.nitroTimer = EditorGUILayout.FloatField("Nitro Timer", vehicleController.vehicleVars.nitroTimer);
            vehicleController.vehicleVars.rechargeNitro = EditorGUILayout.FloatField("Recharge Nitro", vehicleController.vehicleVars.rechargeNitro);
            EditorGUILayout.PropertyField(soVpc.FindProperty("totalGears"), true);
            EditorGUILayout.PropertyField(soVpc.FindProperty("visualNitroInBuggy"), true);
        }

        if (weaponSettings[i])
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("Weapon Settings");
            GUI.color = Color.white;

            EditorGUILayout.PropertyField(soVpc.FindProperty("primaryWeaponPlaceholder"), true);
            EditorGUILayout.PropertyField(soVpc.FindProperty("secondaryWeaponPlaceholder"), true);
            EditorGUILayout.PropertyField(soVpc.FindProperty("gadgetPlaceholder"), true);
            EditorGUILayout.PropertyField(soVpc.FindProperty("weaponManager"), true);
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUI.changed) EditorUtility.SetDirty(vehicleController);

        soVpc.ApplyModifiedProperties();
        EditorGUILayout.EndScrollView();

       // GUI.DragWindow();
    }
    private void IAVehicles(int i)
    {
        if (i == 0 && lastIds.Contains(i))
        {
            lastIds.Clear();
            filter = true;
        }
        if (!lastIds.Contains(i) && !filter)
        {
            lastIds.Add(i);
            if (windows[i].position.y == 0)
            {
                if (i % 2 == 0)
                {
                    if (i == 0) windows[i] = new Rect(15, 50, 360, 240);
                    else windows[i] = new Rect(15, windows[i - 2].position.y + 277, 360, 240);

                }
                else
                {
                    if (i == 1) windows[i] = new Rect(400, 50, 360, 240);
                    else windows[i] = new Rect(400, windows[i - 2].position.y + 277, 360, 240);
                }
            }
        }

        filter = false;
        scrollPos[i] = EditorGUILayout.BeginScrollView(scrollPos[i], GUILayout.Width(360), GUILayout.Height(240));

        var vehicleController = viac[i].GetComponentInChildren<VehicleIAController>();

        if (vehicleController != null)
        {
            soViac = new SerializedObject(vehicleController);
            if (vehicleController.vehicleVars != null) soViacVars = new SerializedObject(vehicleController.vehicleVars);
        }

        soViacVars.Update();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.BeginHorizontal();


        if (generalConfig[i]) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(generalConfigIcon)) generalConfig[i] = !generalConfig[i];

        if (wheelSettings[i]) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(wheelIcon)) wheelSettings[i] = !wheelSettings[i];

        if (engineSettings[i]) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(engineIcon)) engineSettings[i] = !engineSettings[i];

        if (weaponSettings[i]) GUI.backgroundColor = Color.gray;
        else GUI.backgroundColor = Color.white;
        if (GUILayout.Button(weaponIcon)) weaponSettings[i] = !weaponSettings[i];

        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.gray;

        if (generalConfig[i])
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("General Settings");
            GUI.color = Color.white;

            EditorGUILayout.PropertyField(soViacVars.FindProperty("vehicleType"), true);
            vehicleController.name = EditorGUILayout.TextField("Name", vehicleController.name);
            vehicleController.vehicleVars.downForce = EditorGUILayout.FloatField("Down Force", vehicleController.vehicleVars.downForce);
            EditorGUILayout.PropertyField(soViac.FindProperty("centerOfMass"), true);
            vehicleController.vehicleVars.resetTime = EditorGUILayout.FloatField("Reset Time", vehicleController.vehicleVars.resetTime);
            EditorGUILayout.PropertyField(soViac.FindProperty("backDust"), true);
            EditorGUILayout.PropertyField(soViac.FindProperty("_nextCheckpoint"), true);
        }

        if (wheelSettings[i])
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("Wheel Settings");
            GUI.color = Color.white;

            EditorGUILayout.PropertyField(soVpc.FindProperty("wheelMeshList"), true);
            EditorGUILayout.PropertyField(soVpc.FindProperty("wheelColliderList"), true);
            vehicleController.vehicleVars.brakeForce = EditorGUILayout.FloatField("Brake Force", vehicleController.vehicleVars.brakeForce);
            vehicleController.vehicleVars.handBrakeForce = EditorGUILayout.FloatField("Hand Brake Force", vehicleController.vehicleVars.handBrakeForce);
            vehicleController.vehicleVars.minSteerAngle = EditorGUILayout.FloatField("Min Steer Angle", vehicleController.vehicleVars.minSteerAngle);
            vehicleController.vehicleVars.maxSteerAngle = EditorGUILayout.FloatField("Max Steer Angle", vehicleController.vehicleVars.maxSteerAngle);
            vehicleController.vehicleVars.minSidewaysFriction = EditorGUILayout.FloatField("Min Sideways Friction", vehicleController.vehicleVars.minSidewaysFriction);
            vehicleController.vehicleVars.maxSidewaysFriction = EditorGUILayout.FloatField("Max Sideways Friction", vehicleController.vehicleVars.maxSidewaysFriction);
        }

        if (engineSettings[i])
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("Engine Settings");
            GUI.color = Color.white;

            vehicleController.vehicleVars.wheelDriveType = (WheelDriveType)EditorGUILayout.EnumPopup("Wheel Drive Type", vehicleController.vehicleVars.wheelDriveType);
            vehicleController.vehicleVars.torque = EditorGUILayout.FloatField("Max Force", vehicleController.vehicleVars.torque);
            vehicleController.vehicleVars.topSpeed = EditorGUILayout.FloatField("Top Speed", vehicleController.vehicleVars.topSpeed);
            vehicleController.vehicleVars.topReverseSpeed = EditorGUILayout.FloatField("Top Reverse Speed", vehicleController.vehicleVars.topReverseSpeed);
            vehicleController.vehicleVars.nitroPower = EditorGUILayout.FloatField("Nitro Power", vehicleController.vehicleVars.nitroPower);
            vehicleController.vehicleVars.nitroTimer = EditorGUILayout.FloatField("Nitro Timer", vehicleController.vehicleVars.nitroTimer);
            vehicleController.vehicleVars.rechargeNitro = EditorGUILayout.FloatField("Recharge Nitro", vehicleController.vehicleVars.rechargeNitro);
            EditorGUILayout.PropertyField(soViac.FindProperty("totalGears"), true);
            vehicleController.firstUpGearValor = EditorGUILayout.FloatField("First Up Gear", vehicleController.firstUpGearValor);
            vehicleController.maximunGearUpValor = EditorGUILayout.FloatField("Maximum Gear", vehicleController.maximunGearUpValor);
            vehicleController.minimunGearValor = EditorGUILayout.FloatField("Minimum Gear", vehicleController.minimunGearValor);
        }

        if (weaponSettings[i])
        {
            EditorGUILayout.Space();
            GUI.color = Color.cyan;
            EditorGUILayout.LabelField("Weapon Settings");
            GUI.color = Color.white;

            EditorGUILayout.PropertyField(soViac.FindProperty("primaryWeaponPlaceholder"), true);
            EditorGUILayout.PropertyField(soViac.FindProperty("secondaryWeaponPlaceholder"), true);
            EditorGUILayout.PropertyField(soViac.FindProperty("gadgetPlaceholder"), true);
            EditorGUILayout.PropertyField(soViac.FindProperty("weaponManager"), true);
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUI.changed) EditorUtility.SetDirty(vehicleController);

        soViac.ApplyModifiedProperties();
        EditorGUILayout.EndScrollView();

    }

    private void FixValues()
    {
      if (vpc != null)
      {
          for (int i = 0; i < vpc.Length; i++)
          {
              var vehicleController = vpc[i].GetComponentInChildren<VehiclePlayerController>();

              if (vehicleController.vehicleVars.topSpeed < 0) vehicleController.vehicleVars.topSpeed = 0;
              if (vehicleController.vehicleVars.downForce < 0) vehicleController.vehicleVars.downForce = 0;
              if (vehicleController.vehicleVars.torque < 0) vehicleController.vehicleVars.torque = 0;
              if (vehicleController.vehicleVars.brakeForce < 0) vehicleController.vehicleVars.brakeForce = 0;
              if (vehicleController.vehicleVars.maxSteerAngle < 0) vehicleController.vehicleVars.maxSteerAngle = 0;
              if (vehicleController.vehicleVars.minSteerAngle < 0) vehicleController.vehicleVars.minSteerAngle = 0;
              if (vehicleController.vehicleVars.nitroPower < 0) vehicleController.vehicleVars.nitroPower = 0;
          }
      }

        if (viac != null)
        {
            for (int i = 0; i < viac.Length; i++)
            {
                var vehicleController = viac[i].GetComponentInChildren<VehicleIAController>();

                if (vehicleController.vehicleVars == null) return;
                if (vehicleController.vehicleVars.topSpeed < 0) vehicleController.vehicleVars.topSpeed = 0;
                if (vehicleController.vehicleVars.downForce < 0) vehicleController.vehicleVars.downForce = 0;
                if (vehicleController.vehicleVars.torque < 0) vehicleController.vehicleVars.torque = 0;
                if (vehicleController.vehicleVars.brakeForce < 0) vehicleController.vehicleVars.brakeForce = 0;
                if (vehicleController.vehicleVars.maxSteerAngle < 0) vehicleController.vehicleVars.maxSteerAngle = 0;
                if (vehicleController.vehicleVars.minSteerAngle < 0) vehicleController.vehicleVars.minSteerAngle = 0;
                if (vehicleController.vehicleVars.nitroPower < 0) vehicleController.vehicleVars.nitroPower = 0;

            }
        }
    }
}