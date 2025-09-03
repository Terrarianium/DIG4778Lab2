using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        SelectionButtons();
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

    public void SelectionButtons()
    {
        if (GUILayout.Button("Select all shapes"))
        {
            var allShapes = GameObject.FindObjectsOfType<ObjectBehavior>();
            var allShapeGameObjects = allShapes.Select(shape  => shape.gameObject).ToArray();
            Selection.objects = allShapeGameObjects;
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select all spheres"))
        {
            var allSpheres = GameObject.FindObjectsOfType<SphereCollider>();
            var allSphereGameObjets = allSpheres.Select(sphere => sphere.gameObject).ToArray();
            Selection.objects = allSphereGameObjets;
        }
        if (GUILayout.Button("Select all cubes"))
        {
            var allCubes = GameObject.FindObjectsOfType<BoxCollider>();
            var allCubeGameObjects = allCubes.Select(cube => cube.gameObject).ToArray();
            Selection.objects = allCubeGameObjects;
        }
        EditorGUILayout.EndHorizontal();
    }
}