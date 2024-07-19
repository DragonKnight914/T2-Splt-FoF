using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class CutsceneScript : MonoBehaviour
{

    private VideoPlayer m_VideoPlayer;
    public GameObject Canvas;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        m_VideoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((m_VideoPlayer.frame) > 0 && (m_VideoPlayer.isPlaying == false) || Input.anyKey)
        {
            //Video has finshed playing!
            Debug.Log("Finished");
            Canvas.SetActive(true);
            music.Play();
            this.gameObject.SetActive(false);
        }
    }
}
