using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectBehavior : MonoBehaviour
{
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

<<<<<<< Updated upstream
=======
        SizeWarnings();
        SelectionButtons();
    }

    public void SizeWarnings()
    {
        // This is reading the object's size float value
        var size = serializedObject.FindProperty("size");
        // If it is equal to or larger than 2 it will give a warning
        if (size.floatValue >= 2)
        {
            EditorGUILayout.HelpBox("Size is too big!", MessageType.Warning);
        }
        // If it is equal to or smaller than 0 it will give an error
        else if (size.floatValue <= 0)
        {
            EditorGUILayout.HelpBox("Size is too small!", MessageType.Error);
        }
    }

    public void EnableDisable()
    {
        if (GUILayout.Button("Disable/Enable Objecys", GUILayout.Height(40)))
        {

        }
    }
    
    public void SelectionButtons()
    {
        // This will select all shapes regardless if they're spheres or cubes
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
        if (GUILayout.Button("Select all shapes"))
        {
            var allShapes = GameObject.FindObjectsOfType<ObjectBehavior>();
            var allShapeGameObjects = allShapes.Select(shape  => shape.gameObject).ToArray();
            Selection.objects = allShapeGameObjects;
        }
        EditorGUILayout.BeginHorizontal();
        // This will select all spheres and no cubes
        if (GUILayout.Button("Select all spheres"))
        {
            var allSpheres = GameObject.FindObjectsOfType<SphereCollider>();
            var allSphereGameObjets = allSpheres.Select(sphere => sphere.gameObject).ToArray();
            Selection.objects = allSphereGameObjets;
        }
        // This will select all cubes and not spheres
        if (GUILayout.Button("Select all cubes"))
        {
            var allCubes = GameObject.FindObjectsOfType<BoxCollider>();
            var allCubeGameObjects = allCubes.Select(cube => cube.gameObject).ToArray();
            Selection.objects = allCubeGameObjects;
        }
        EditorGUILayout.EndHorizontal();
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
        // This will deselect all objects
        if (GUILayout.Button("Clear selection"))
        {
            Selection.objects = new Object[] { (target as ObjectBehavior).gameObject };
        }
>>>>>>> Stashed changes
    }
}