using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(VehiclesManagerData))]
public class VehiclesManagerDataEditor : Editor
{
    private VehiclesManagerData _target;
    private bool helpBoxPlayer;
    private bool helpBoxIA;
    private bool helpBoxLimitPlayer;
    private bool helpBoxLimitIA;
    void OnEnable()
    {
        _target = (VehiclesManagerData)target;
        helpBoxPlayer = false;
        helpBoxIA = false;
        helpBoxLimitPlayer = false;
        helpBoxLimitIA = false;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ShowValues();
        FixValues();
        Repaint();
    }

    private void ShowValues()
    {
        if (_target != null)
        {
            EditorUtility.SetDirty(_target);

            if (_target._vehiclesPlayerController.Count > 12)
            {
                _target._vehiclesPlayerController.RemoveAt(_target._vehiclesPlayerController.Count - 1);
                helpBoxLimitPlayer = true;
            }
            if (_target._vehiclesIAController.Count > 12)
            {
                _target._vehiclesIAController.RemoveAt(_target._vehiclesIAController.Count - 1);
                helpBoxLimitIA = true;
            }

            for (int i = 0; i < _target._vehiclesPlayerController.Count; i++)
            {
                if (_target._vehiclesPlayerController[i] != null)
                {
                    if (_target._vehiclesPlayerController[i].GetComponentInChildren<VehiclePlayerController>() != null) continue;
                    else _target._vehiclesPlayerController[i] = null;
                    helpBoxPlayer = true;
                }
            }

            for (int i = 0; i < _target._vehiclesIAController.Count; i++)
            {
                if (_target._vehiclesIAController[i] != null)
                {
                    if (_target._vehiclesIAController[i].GetComponentInChildren<VehicleIAController>() != null) continue;
                    else _target._vehiclesIAController[i] = null;
                    helpBoxIA = true;
                }
            }

            if (helpBoxPlayer) EditorGUILayout.HelpBox("This gameObject doesn't contain VehiclePlayerController Script", MessageType.Error);
            if (helpBoxIA) EditorGUILayout.HelpBox("This gameObject doesn't contain VehicleIAController Script", MessageType.Error);
            if (helpBoxLimitPlayer) EditorGUILayout.HelpBox("Max 12 vehicles", MessageType.Warning);
            if (helpBoxLimitIA) EditorGUILayout.HelpBox("Max 12 vehicles", MessageType.Warning);
        
        }
    }

    private void FixValues()
    {

    }
}