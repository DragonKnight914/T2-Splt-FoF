using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class EndFade : MonoBehaviour
{
    public VideoPlayer m_VideoPlayer;
    public GameObject vidHolder;
    public GameObject Thanks;

    // Start is called before the first frame update
    void Start()
    {
        //m_VideoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((m_VideoPlayer.frame) > 0 && (m_VideoPlayer.isPlaying == false))
        {
            //Video has finshed playing!
            Debug.Log("Finished");
            Thanks.SetActive(true);
            //vidHolder.SetActive(false);
        }
    }
}
