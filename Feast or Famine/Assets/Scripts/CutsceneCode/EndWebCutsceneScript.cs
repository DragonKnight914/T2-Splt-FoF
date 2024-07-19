using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndWebCutsceneScript : MonoBehaviour
{
    [SerializeField] private string videoFileName;
    public VideoPlayer m_VideoPlayer;
    public TriggerEndCutscene T;
    private bool vidStarted = false;

    // Start is called before the first frame update
    void Awake()
    {
        //m_VideoPlayer = GetComponent<VideoPlayer>();
        
    }

    private void PlayVideo()
    {
        if (m_VideoPlayer && T.canPlayVid && !vidStarted)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath,videoFileName);
            Debug.Log(videoPath);
            m_VideoPlayer.url = videoPath;
            m_VideoPlayer.Play();
            vidStarted = true;
        }
    }

    /*private void OnVideoPrepared(VideoPlayer source)
    {
        m_VideoPlayer.Play();
    }*/

    // Update is called once per frame
    void Update()
    {
        PlayVideo();
    }
}
