using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    public float size;

    [ExecuteInEditMode]
    private void Update()
    {
        transform.localScale = new Vector3(size, size, size);
    }
}

public class ObjectBehaviorEditor : Editor
{

}