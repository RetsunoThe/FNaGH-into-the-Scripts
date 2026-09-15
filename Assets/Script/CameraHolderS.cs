using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraHolderS : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Transform Orientation;

    void Update()
    {
        transform.position = Orientation.transform.position;
    }

}
