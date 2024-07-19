using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Customization")]
    public Animator cAnim;
    private int customizingHash;
    [SerializeField] private Toggle marchingHatT = null;
    
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text masterTextValue = null;
    [SerializeField] private Slider masterSlider = null;
    [SerializeField] private TMP_Text musicTextValue = null;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private TMP_Text sfxTextValue = null;
    [SerializeField] private Slider sfxSlider = null;
    [SerializeField] private TMP_Text ambienceTextValue = null;
    [SerializeField] private Slider ambienceSlider = null;
    [SerializeField] private float defaultMaster = 1.0f;
    [SerializeField] private float defaultMusic = 1.0f;
    [SerializeField] private float defaultSFX = 1.0f;
    [SerializeField] private float defaultAmbience = 1.0f;
    [SerializeField] private AudioMixer mixer;

    [Header("Graphics Settings")]
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private float defaultBrightnesss = 1.0f;
    [SerializeField] private Toggle fullScreenToggle = null;

    private int qualityLevel;
    [SerializeField] private TMP_Dropdown QualityDropdown;
    private bool isFullScreen;
    private float brightnessLevel;

    //Resolution
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    
    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text sensYTextValue = null;
    [SerializeField] private Slider sensYSlider = null;
    [SerializeField] private TMP_Text sensXTextValue = null;
    [SerializeField] private Slider sensXSlider = null;
    //[SerializeField] private TMP_Text fovTextValue = null;
    //[SerializeField] private Slider fovSlider = null;
    //[SerializeField] private float defaultFOV = 60.0f;
    //public float mainFOV = 60.0f;
    [SerializeField] private float defaultXSens = 0.5f;
    [SerializeField] private float defaultYSens = 0.5f;
    //public float mainSens = 20.0f;
    [SerializeField] private Toggle invertYToggle = null;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;


    [Header("Levels To Load")]
    public string _newMatch;
    //private string levelToLoad;

    private void Start()
    {
        //customizingHash = Animator.StringToHash("isCustomizing");

        //resolutions = Screen.resolutions;
        //resolutionDropdown.ClearOptions();

        /*List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();*/
      
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
            masterTextValue.text = masterSlider.value.ToString("0.0");
            SetMasterVolume();
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            musicTextValue.text = musicSlider.value.ToString("0.0");
            SetMusicVolume();
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            sfxTextValue.text = sfxSlider.value.ToString("0.0");
            SetSFXVolume();
        }
        if (PlayerPrefs.HasKey("ambienceVolume"))
        {
            ambienceSlider.value = PlayerPrefs.GetFloat("ambienceVolume");
            ambienceTextValue.text = ambienceSlider.value.ToString("0.0");
            SetAmbienceVolume();
        }
        
        /*if (PlayerPrefs.HasKey("masterSenY"))
        {
            sensYSlider.value = PlayerPrefs.GetFloat("masterSenY");
            sensYTextValue.text = sensYSlider.value.ToString("0.0");
            SetYSensitivity();
        }
        if (PlayerPrefs.HasKey("masterSenX"))
        {
            sensXSlider.value = PlayerPrefs.GetFloat("masterSenX");
            sensXTextValue.text = sensXSlider.value.ToString("0.0");
            SetXSensitivity();
        }*/

        else
        {
            //SetYSensitivity();
            //SetXSensitivity();
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
            SetAmbienceVolume();
        }
    }

    public void JoinMatchDialogYes()
    {
        StartCoroutine(EnteringGame());
    }

    public IEnumerator EnteringGame()
    {
        yield return new WaitForSeconds (6f);
        SceneManager.LoadScene(_newMatch);
    }

    public void Customization()
    {
        cAnim.SetBool(customizingHash, true);
    }
    
    public void CustomizationReturn()
    {
        cAnim.SetBool(customizingHash, false);
    }

    public void ToggleHat()
    {
        if (marchingHatT.isOn)
            PlayerPrefs.SetInt("masterMarchingHat", 1);
        else
            PlayerPrefs.SetInt("masterMarchingHat", 0);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        masterTextValue.text = volume.ToString("0.0"); 
        mixer.SetFloat("master", Mathf.Log10(volume)*20);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        musicTextValue.text = volume.ToString("0.0"); 
        mixer.SetFloat("music", Mathf.Log10(volume)*20);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        sfxTextValue.text = volume.ToString("0.0"); 
        mixer.SetFloat("sfx", Mathf.Log10(volume)*20);
    }

    public void SetAmbienceVolume()
    {
        float volume = ambienceSlider.value;
        ambienceTextValue.text = volume.ToString("0.0"); 
        mixer.SetFloat("ambience", Mathf.Log10(volume)*20);
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("ambienceVolume", ambienceSlider.value);
        StartCoroutine(ConfirmationBox());
    }

    public void ResetDefaults(string MenuType)
    {
        if (MenuType == "Audio")
        {
            mixer.SetFloat("master", defaultMaster);
            masterTextValue.text = defaultMaster.ToString("0.0");
            masterSlider.value = defaultMaster;
            mixer.SetFloat("music", defaultMusic);
            musicTextValue.text = defaultMusic.ToString("0.0");
            musicSlider.value = defaultMusic;
            mixer.SetFloat("sfx", defaultSFX);
            sfxTextValue.text = defaultSFX.ToString("0.0");
            sfxSlider.value = defaultSFX;
            mixer.SetFloat("ambience", defaultAmbience);
            ambienceTextValue.text = defaultAmbience.ToString("0.0");
            ambienceSlider.value = defaultAmbience;
            VolumeApply();
        }

        if (MenuType == "Gameplay")
        {

            GameplayApply();
        }

        if (MenuType == "Graphics")
        {
            brightnessTextValue.text = defaultBrightnesss.ToString("0.0");
            brightnessSlider.value = defaultBrightnesss;
            fullScreenToggle.isOn = false;
            isFullScreen = false;
            SetQuality(1);
            SetResolution(8);
            SetQuality(2);
            QualityDropdown.value = 2;
            resolutionDropdown.value = resolutions.Length;
            GameplayApply();
        }

    }

    public void SetBrightness(float brightness)
    {
        brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");

    }

    public void SetFullscreen(bool fullScreen)
    {
        isFullScreen = fullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        qualityLevel = qualityIndex;
    }

    public void SetYSensitivity()
    {
        float sens = sensYSlider.value;

        sensYTextValue.text = sens.ToString("0.0"); 

    }

    public void SetXSensitivity()
    {
        float sens = sensXSlider.value;

        sensXTextValue.text = sens.ToString("0.0"); 
    }

    /*public void SetFOV(float fov)
    {
        mainFOV = fov;
        fovTextValue.text = fov.ToString("0.0");
    }*/

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", brightnessLevel);
        //Sets post processing brightness
        
        PlayerPrefs.SetInt("masterQuality", qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("MasterFullscreen", (isFullScreen ? 1 : 0));
        Screen.fullScreen = isFullScreen;
        
        StartCoroutine(ConfirmationBox());
    }

    public void GameplayApply()
    {
        PlayerPrefs.SetFloat("masterSenY", sensYSlider.value);
        PlayerPrefs.SetFloat("masterSenX", sensXSlider.value);
        StartCoroutine(ConfirmationBox());
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }


    /*public void LoadGameDialogYes()
    {
        SceneManager.LoadScene(_NewGameLevel);
    }*/
    
    
}
