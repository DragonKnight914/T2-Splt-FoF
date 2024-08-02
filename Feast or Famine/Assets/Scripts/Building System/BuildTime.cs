using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildTime : MonoBehaviour
{
    public int time = 120;
    public bool canBuild = false;

    [SerializeField] TextMeshProUGUI timeToSpent;
    [SerializeField] GameObject buildText;
    [SerializeField] GameObject defenseText;
    [SerializeField] Button buildMode;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTimeText();
        //buildMode.interactable = false;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time--;

            if (time <= 0)
            {
                if (canBuild == false)
                {
                    canBuild = true;
                    ChangeSceneState.instance.canGetOtherScenes = true;
                    buildMode.interactable = true;
                    buildText.SetActive(true);
                    defenseText.SetActive(false);
                    time = 120;
                }
                else if (canBuild == true)
                {
                    canBuild = false;
                    ChangeSceneState.instance.canGetOtherScenes = false;
                    buildMode.interactable = false;
                    buildText.SetActive(false);
                    defenseText.SetActive(true);
                    time = 60;
                    GridBuildingSystem.instance.ExitBuildMode();
                }
            }

            UpdateTimeText();
        }
    }

    void UpdateTimeText()
    {
        int minutes = time / 60;
        int seconds = time % 60;
        timeToSpent.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
