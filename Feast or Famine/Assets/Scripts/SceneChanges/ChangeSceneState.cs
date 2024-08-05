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

    [SerializeField] GameObject fadeOut;
    [SerializeField] GameObject isCertainly;
    [SerializeField] TextMeshProUGUI textScene;

    public BuildTime bt;

    public float holdTime;
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
        if(canPress)
        {
            if (canGetOtherScenes)
            {
                if (bt.canBuild && PlayerPrefs.GetInt("CanEnterArea") == 1)
                {
                    
                    if (Input.GetKey(KeyCode.E))
                    {
                        holdTime += Time.deltaTime;
                        isHolding = true;


                        if (holdTime >= needHoldTime)
                        {
                            StartCoroutine(ChangeScene(sceneName));
                            isHolding = false;
                        }

                    }
                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        holdTime = 0;
                        isHolding = false;
                    }

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isCertainly.gameObject.SetActive(true);
            textScene.text = whatAppears;
            canPress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCertainly.gameObject.SetActive(false);
            canPress = false;
        }
    }

    public IEnumerator ChangeScene(string nameScene)
    {
        fadeOut.SetActive(true);
        //PlayerPrefs.Save();
        PlayerPrefs.SetInt("CanEnterArea", 0);
        /*string activeScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("BaseLevelSaved", activeScene);*/
        Debug.Log(sceneName);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nameScene);
    }
}
