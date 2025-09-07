using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.EventSystems;

// This class handles changing the size of the shapes
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
        EnableDisable();
    }

    // This will give a warning in the editor when the size is above 2 and will give an error when the size is below 0
    public void SizeWarnings()
    {
        var size = serializedObject.FindProperty("size");
        if (size.floatValue >= 2)
        {
            EditorGUILayout.HelpBox("Size is too big!", MessageType.Warning);
        }
        else if (size.floatValue <= -0)
        {
            EditorGUILayout.HelpBox("Size is too small!", MessageType.Error);
        }
    }
    
    // This function contains the editor buttons
    public void SelectionButtons()
    {
        // We included a button to select all shapes
        if (GUILayout.Button("Select all shapes"))
        {
            var allShapes = GameObject.FindObjectsOfType<ObjectBehavior>();
            var allShapeGameObjects = allShapes.Select(shape => shape.gameObject).ToArray();
            Selection.objects = allShapeGameObjects;
        }
        EditorGUILayout.BeginHorizontal();
        // All spheres
        if (GUILayout.Button("Select all spheres"))
        {
            var allSpheres = GameObject.FindObjectsOfType<SphereCollider>();
            var allSphereGameObjets = allSpheres.Select(sphere => sphere.gameObject).ToArray();
            Selection.objects = allSphereGameObjets;
        }
        // All cubes
        if (GUILayout.Button("Select all cubes"))
        {
            var allCubes = GameObject.FindObjectsOfType<BoxCollider>();
            var allCubeGameObjects = allCubes.Select(cube => cube.gameObject).ToArray();
            Selection.objects = allCubeGameObjects;
        }
        EditorGUILayout.EndHorizontal();
        // And clears the selection
        // This does still leave one shape selected after clicking it, though.
        if (GUILayout.Button("Clear selection"))
        {
            Selection.objects = new UnityEngine.Object[] { (target as ObjectBehavior).gameObject };
        }
    }

    // And this is the function for disabling and enabling all objects, spheres, or cubes with the color reflecting their state
    public void EnableDisable()
    {
        // Get all objects
        var allShapes = GameObject.FindObjectsOfType<ObjectBehavior>(true);
        var allSpheres = GameObject.FindObjectsOfType<SphereCollider>(true);
        var allCubes = GameObject.FindObjectsOfType<BoxCollider>(true);

        // Determine if all are enabled/disabled
        bool allShapesEnabled = allShapes.Length > 0 && allShapes.All(obj => obj.gameObject.activeSelf);
        bool allSpheresEnabled = allSpheres.Length > 0 && allSpheres.All(obj => obj.gameObject.activeSelf);
        bool allCubesEnabled = allCubes.Length > 0 && allCubes.All(obj => obj.gameObject.activeSelf);

        // If both spheres and cubes are disabled, allShapes should be considered disabled. This affects the "all shapes" button because it should be greyed out if either spheres and cubes are disabled.
        bool bothSpheresAndCubesDisabled = !allSpheresEnabled && !allCubesEnabled;

        // All shapes button
        Color prevColor = GUI.backgroundColor;
        // This line is a bit complex, but it ensures that the "all shapes" button is greyed out if both spheres and cubes are disabled
        GUI.backgroundColor = (allShapesEnabled && !bothSpheresAndCubesDisabled) ? Color.green : Color.grey;
        if (GUILayout.Button("Disable/Enable Objects", GUILayout.Height(40)))
        {
            bool newState = !allShapesEnabled;
            foreach (var obj in allShapes)
            {
                obj.gameObject.SetActive(newState);
            }
        }
        GUI.backgroundColor = prevColor;

        EditorGUILayout.BeginHorizontal();

        // Spheres button
        // Similar logic as above, but only for spheres, it handles color based on their state
        GUI.backgroundColor = allSpheresEnabled ? Color.green : Color.grey;
        if (GUILayout.Button("Disable/Enable Spheres", GUILayout.Height(40)))
        {
            bool newState = !allSpheresEnabled;
            foreach (var obj in allSpheres)
            {
                obj.gameObject.SetActive(newState);
            }
        }
        GUI.backgroundColor = prevColor;

        // Cubes button
        // Similar logic as above, but only for cubes, it handles color based on their state
        GUI.backgroundColor = allCubesEnabled ? Color.green : Color.grey;
        if (GUILayout.Button("Disable/Enable Cubes", GUILayout.Height(40)))
        {
            bool newState = !allCubesEnabled;
            foreach (var obj in allCubes)
            {
                obj.gameObject.SetActive(newState);
            }
        }
        GUI.backgroundColor = prevColor;

        EditorGUILayout.EndHorizontal();
    }
}