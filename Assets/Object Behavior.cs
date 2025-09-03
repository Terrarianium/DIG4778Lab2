using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectBehavior : MonoBehaviour
{
    [SerializeField]
    public float size;

    void Update()
    {
        transform.localScale = new Vector3(size, size, size);
    }
}

[CustomEditor(typeof(ObjectBehavior)), CanEditMultipleObjects]
public class ObjectBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SizeWarnings();
    }

    public void SizeWarnings()
    {
        var size = serializedObject.FindProperty("size");
        if (size.floatValue >= 2)
        {
            EditorGUILayout.HelpBox("Size is too big!", MessageType.Warning);
        }
        else if (size.floatValue <= -2)
        {
            EditorGUILayout.HelpBox("Size is too small! Also negative?", MessageType.Warning);
        }
        else if (size.floatValue == 0)
        {
            EditorGUILayout.HelpBox("Object has no size!", MessageType.Error);
        }
    }
}