using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterConstructionMode : MonoBehaviour
{
    [SerializeField] Camera cam;
    public void EnterContructionCamera()
    {
        cam.backgroundColor = Color.yellow;
    }
}
