using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneState : MonoBehaviour
{

    [SerializeField] string sceneName;
    [SerializeField] string whatAppears;

    [SerializeField] GameObject isCertainly;
    [SerializeField] TextMeshProUGUI textScene;

    float holdTime;
    const float needHoldTime = 5f;
    bool isHolding = false;
    bool canPress = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(canPress == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                holdTime += Time.time;
                isHolding = true;


                if(holdTime == needHoldTime)
                {
                    ChangeScene(sceneName);
                    isHolding = false;
                }
                
            }
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isCertainly.gameObject.SetActive(true);
            textScene.text = whatAppears;
            canPress = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCertainly.gameObject.SetActive(false);
            canPress = false;
        }
    }

    private void ChangeScene(string nameScene)
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(nameScene);
    }
}
