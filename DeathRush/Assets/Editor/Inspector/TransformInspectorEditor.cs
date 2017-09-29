/*using UnityEngine;
using UnityEditor;
using System.Linq;
[CustomEditor(typeof(Transform))]
public class TransformInspectorEditor : Editor
{
    private Transform _target;
    private bool _showFoldoutChildren;
    private int numberOfCopies;
    void OnEnable()
    {
        _target = (Transform)target;
    }

    public override void OnInspectorGUI()
    {
        ShowValues();
        FixValues();
        Repaint();

    }
    private void ShowValues()
    {

        DrawPosition();
        DrawRotation();
        DrawScale();

        DrawShowChildren();

    }

    void DrawPosition()
    {
        GUILayout.BeginHorizontal();

        GUI.color = Color.white;
        GUIContent content = new GUIContent("R", "Reset");
        if (GUILayout.Button(content, GUILayout.Width(20f), GUILayout.Height(20f)))
        {
            Undo.RegisterUndo(_target, "Position Reset" + _target.name);
            _target.localPosition = Vector3.zero;
           
        }

        if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
        {
            //   GUI.tooltip = "Resdsdset";
        }

        GUI.color = Color.white;

        Vector3 position = EditorGUILayout.Vector3Field("Position", _target.position);
        _target.position = position;

        GUILayout.EndHorizontal();
    }
    void DrawRotation()
    {
        GUILayout.BeginHorizontal();

        GUI.color = Color.white;
        GUIContent content = new GUIContent("R", "Reset");
        if (GUILayout.Button(content, GUILayout.Width(20f)))
        {
            Undo.RegisterUndo(_target, "Rotation Reset" + _target.name);
            _target.transform.rotation = Quaternion.identity;       
        }


        Vector3 eulerAngles = EditorGUILayout.Vector3Field("Rotation", _target.localEulerAngles);
        _target.localEulerAngles = eulerAngles;

        GUILayout.EndHorizontal();
    }

    void DrawScale()
    {
        GUILayout.BeginHorizontal();

        GUI.color = Color.white;
        GUIContent content = new GUIContent("R", "Reset");
        if (GUILayout.Button(content, GUILayout.Width(20f)))
        {
            Undo.RegisterUndo(_target, "Scale Reset" + _target.name);
            _target.localScale = Vector3.one;
        }

        GUI.color = Color.white;
        Vector3 scaleSize = EditorGUILayout.Vector3Field("Scale", _target.localScale);
        _target.localScale = scaleSize;


        GUILayout.EndHorizontal();
    }
    public void CreateCopies()
    {
        GameObject emptyGameObject = new GameObject(_target.name + "'s Children");
        emptyGameObject.transform.parent = _target.parent;

        Undo.RegisterCreatedObjectUndo(emptyGameObject, "Create Copies" + _target.name);

        for (int i = 0; i < numberOfCopies; i++)
        {
            var copy = (GameObject)Instantiate(_target.gameObject, _target.position, Quaternion.identity);
            copy.transform.parent = emptyGameObject.transform;
            copy.name = _target.gameObject.name + " " + i;
        }
    }

    void DrawShowChildren()
    {
        GUILayout.BeginHorizontal();
        numberOfCopies = EditorGUILayout.IntField("Number Of Copies", numberOfCopies,GUILayout.Width(150));
        if (GUILayout.Button("Create Copies")) CreateCopies();
        GUILayout.EndHorizontal();

        var _children = _target.GetComponentsInChildren<Transform>().ToList();
        _children.RemoveAt(0);
        _showFoldoutChildren = EditorGUILayout.Foldout(_showFoldoutChildren, "GameObject's Children");
        if (_showFoldoutChildren) foreach (Transform child in _children) if (GUILayout.Button("     " + child.name)) Selection.activeGameObject = child.gameObject;
    }


    private void FixValues()
    {
        if (numberOfCopies < 1) numberOfCopies = 1;
        if (numberOfCopies > 50) numberOfCopies = 50;
    }
}
*/