using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneState : MonoBehaviour
{
    public static ChangeSceneState instance;

    [SerializeField] string sceneName;
    [SerializeField] string whatAppears;

    [SerializeField] GameObject isCertainly;
    [SerializeField] TextMeshProUGUI textScene;

    public BuildTime bt;

    float holdTime;
    const float needHoldTime = 1f;
    bool isHolding = false;
    public bool canPress = false;
    public bool canGetOtherScenes = false;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canGetOtherScenes)
        {
            if (bt.canBuild)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    holdTime += Time.deltaTime;
                    isHolding = true;


                    if (holdTime >= needHoldTime)
                    {
                        ChangeScene(sceneName);
                        isHolding = false;
                    }

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
