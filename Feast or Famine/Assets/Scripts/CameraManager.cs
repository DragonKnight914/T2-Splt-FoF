using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] allVCams;

    [Header("Controls lerping the Y daming for player in air")]
    [SerializeField] private float fallPanAmount  = 0.25f;
    [SerializeField] private float fallPanTime  = 0.35f;
    public float fallSpeedYDampChangeThresh = -15f;

    public bool isLerpingYDamping { get; private set;} 

    public bool lerpedFromPlayerFalling { get; set;} 

    private Coroutine lerpYPanCoroutine;
    private Coroutine panCameraCoroutine;


    private CinemachineVirtualCamera currentCam;
    private CinemachineFramingTransposer framingTransposer;

    private float normPanAmount;

    private Vector2 startingTrackedObjectOffest;



    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < allVCams.Length; i++)
        {
            if (allVCams[i].enabled)
            {
                //set the current active camera
                currentCam = allVCams[i];

                //set framing transposer
                framingTransposer = currentCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        //Sets YDamping amount based on inspector value
        normPanAmount = framingTransposer.m_YDamping;

        //Set the Starting position of the tracked object offset
        startingTrackedObjectOffest = framingTransposer.m_TrackedObjectOffset;
    }  

    #region Swap Cameras

    public void SwapCameras(CinemachineVirtualCamera cameraFromLeft, CinemachineVirtualCamera cameraFromRight, CinemachineVirtualCamera cameraFromUp, 
    CinemachineVirtualCamera cameraFromDown, Vector2 triggerExitDirection)
    {
        //if the current camera is the camera on the left and our trigger exit direction was on the right
        if (currentCam == cameraFromLeft && triggerExitDirection.x > 0f)
        {
            //active new cam
            cameraFromRight.enabled = true;

            //deactivaate old camera
            cameraFromLeft.enabled = false;

            //set new camera as current camera
            currentCam = cameraFromRight;

            //update our composer variable
            framingTransposer = currentCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        }

        //if the current camera is the camera on the right and our trigger exit direction was on the left
        else if (currentCam == cameraFromRight && triggerExitDirection.x < 0f)
        {
            //active new cam
            cameraFromLeft.enabled = true;

            //deactivaate old camera
            cameraFromRight.enabled = false;

            //set new camera as current camera
            currentCam = cameraFromLeft;

            //update our composer variable
            framingTransposer = currentCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        }

        //if the current camera is the camera that is up and our trigger exit direction was down
        else if (currentCam == cameraFromUp && triggerExitDirection.y > 0f)
        {
            //active new cam
            cameraFromDown.enabled = true;

            //deactivaate old camera
            cameraFromUp.enabled = false;

            //set new camera as current camera
            currentCam = cameraFromDown;

            //update our composer variable
            framingTransposer = currentCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        }

        //if the current camera is the camera that is down and our trigger exit direction was up
        else if (currentCam == cameraFromDown && triggerExitDirection.y < 0f)
        {
            //active new cam
            cameraFromUp.enabled = true;

            //deactivaate old camera
            cameraFromDown.enabled = false;

            //set new camera as current camera
            currentCam = cameraFromUp;

            //update our composer variable
            framingTransposer = currentCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        }
    }

    #endregion

    #region Lerp the Y Damping

    public void panCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
    }

    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos = Vector2.zero;

        if (!panToStartingPos)
        {
            //set the direction and distance 
            switch (panDirection)
            {
                case PanDirection.Up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down;
                    break;
                case PanDirection.Left:
                    endPos = Vector2.right;
                    break;
                case PanDirection.Right:
                    endPos = Vector2.left;
                    break;
                default:
                    break;
            }

            endPos *= panDistance;

            startingPos = startingTrackedObjectOffest;

            endPos += startingPos;
        }

        //Handles direction settings when moving back to the starting position
        else
        {
            startingPos = framingTransposer.m_TrackedObjectOffset;
            endPos = startingTrackedObjectOffest;
        }

        //handles panning of camera
        float elapsedTime = 0f;
        while(elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;

            //lerp the y rotation
           Vector3 panLerp = Vector3.Lerp(startingPos, endPos, (elapsedTime / panTime));
           framingTransposer.m_TrackedObjectOffset = panLerp;

            yield return null;
        }

    }

    #endregion

    #region Lerp the Y Damping

    public void LerpYDamping(bool isPlayerFalling)
    {
        lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        isLerpingYDamping = true;

        //grab the starting damping amount
        float startDampAmount = framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        if (isPlayerFalling)
        {
            endDampAmount = fallPanAmount;
            lerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = normPanAmount;
        }

        //lerp pan amount
        float elapsedTime = 0f;
        while (elapsedTime < fallPanTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / fallPanTime));
            framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }

        isLerpingYDamping = false;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
