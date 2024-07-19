using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects;

    private Collider2D coll;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (customInspectorObjects.panCameraOnContact)
            {
                //pan the camera
                CameraManager.instance.panCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - coll.bounds.center);
            if (customInspectorObjects.swapCameras && (customInspectorObjects.cameraOnLeft || customInspectorObjects.cameraOnRight
             || customInspectorObjects.cameraOnUp || customInspectorObjects.cameraOnDown))
            {
                //swap cameras
                CameraManager.instance.SwapCameras(customInspectorObjects.cameraOnLeft, customInspectorObjects.cameraOnRight, customInspectorObjects.cameraOnUp, customInspectorObjects.cameraOnDown, exitDirection);
            }

            if (customInspectorObjects.panCameraOnContact)
            {
                //pan the camera
                CameraManager.instance.panCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, true);
            }
        }
    }
}

[System.Serializable]

public class CustomInspectorObjects
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;
    [HideInInspector] public CinemachineVirtualCamera cameraOnUp;
    [HideInInspector] public CinemachineVirtualCamera cameraOnDown;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;

}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}
#if UNITY_EDITOR
[CustomEditor(typeof(CameraControlTrigger))]


public class MyScriptEditor : Editor
{
    CameraControlTrigger cameraControlTrigger;

    private void OnEnable()
    {
        cameraControlTrigger = (CameraControlTrigger)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (cameraControlTrigger.customInspectorObjects.swapCameras)
        {
            cameraControlTrigger.customInspectorObjects.cameraOnLeft = EditorGUILayout.ObjectField(
                "Camera on Left", cameraControlTrigger.customInspectorObjects.cameraOnLeft,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

            cameraControlTrigger.customInspectorObjects.cameraOnRight = EditorGUILayout.ObjectField(
                "Camera on Right", cameraControlTrigger.customInspectorObjects.cameraOnRight,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

            cameraControlTrigger.customInspectorObjects.cameraOnUp = EditorGUILayout.ObjectField(
                "Camera on Top", cameraControlTrigger.customInspectorObjects.cameraOnUp,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

            cameraControlTrigger.customInspectorObjects.cameraOnDown = EditorGUILayout.ObjectField(
                "Camera on Bottom", cameraControlTrigger.customInspectorObjects.cameraOnDown,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
            

        }

        if (cameraControlTrigger.customInspectorObjects.panCameraOnContact)
        {
            cameraControlTrigger.customInspectorObjects.panDirection = (PanDirection)EditorGUILayout.EnumPopup(
                "Camera Pan Directioin", cameraControlTrigger.customInspectorObjects.panDirection);

            cameraControlTrigger.customInspectorObjects.panDistance = EditorGUILayout.FloatField(
                "Pan Distance", cameraControlTrigger.customInspectorObjects.panDistance);
            
            cameraControlTrigger.customInspectorObjects.panTime = EditorGUILayout.FloatField(
                "Pan Time", cameraControlTrigger.customInspectorObjects.panTime);
        
        }

        if (GUI.changed)
            EditorUtility.SetDirty(cameraControlTrigger);
    }
}
#endif 
