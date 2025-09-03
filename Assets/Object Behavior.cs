using System.Collections;
using System.Collections.Generic;
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

public class ObjectBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {

    }
}