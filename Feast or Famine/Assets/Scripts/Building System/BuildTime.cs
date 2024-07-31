using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildTime : MonoBehaviour
{
    float time = 60f;
    bool canBuild = false;

    [SerializeField] TextMeshProUGUI timeToSpent;
    [SerializeField] Button buildMode;

    // Start is called before the first frame update
    void Start()
    {
        timeToSpent.text = time.ToString();
        buildMode.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

        //@TODO: IGNORE CASES AFTER THE DOT

        time -= Time.deltaTime;
        timeToSpent.text = time.ToString();

        if (time <= 0f && canBuild == false)
        {
            canBuild = true;
            buildMode.interactable = true;
            time = 120f;
        }
        else if (time <= 0f && canBuild == true)
        {
            canBuild = false;
            buildMode.interactable = false;
            time = 60f;
            GridBuildingSystem.instance.ExitBuildMode();
        }
    }
}
