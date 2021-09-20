using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
 

public class Options : MonoBehaviour
{

    public AudioMixer audioMixer;
    Resolution[] resolutions;

    [SerializeField]
    private Dropdown resolutionDropdown;
    [SerializeField]
    private Dropdown qualityIndex;
    [SerializeField]
    private Slider masterVolSlider;
    [SerializeField]
    private Slider musicVolSlider;
    [SerializeField]
    private Slider effectsVolSlider;
    [SerializeField]
    private Toggle fullscreenButton;

    private void Start()
    {

        setSettings();



        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> resolutionOptions = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            resolutionOptions.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetFloat("resolutionWidth", resolution.width);
        PlayerPrefs.SetFloat("resolutionHeight", resolution.height);
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
    }

    public void SetVolumeMaster(float volume)
    {
        audioMixer.SetFloat("VolumeMaster", volume);

        PlayerPrefs.SetFloat("volumeMaster", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("VolumeMusic", volume);

        PlayerPrefs.SetFloat("volumeMusic", volume);
    }

    public void SetVolumeEffects(float volume)
    {
        audioMixer.SetFloat("VolumeEffects", volume);

        PlayerPrefs.SetFloat("volumeEffects", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if(isFullscreen == true)
        {
            PlayerPrefs.SetInt("isFullscreen", 1);
        }
        else if(isFullscreen == false)
        {
            PlayerPrefs.SetInt("isFullscreen", 0);
        }
    }

    public void setSettings()
    {
        //set resolution
        Screen.SetResolution(PlayerPrefs.GetInt("resolutionWidth"), PlayerPrefs.GetInt("resolutionHeight"), Convert.ToBoolean(PlayerPrefs.GetInt("isFullscreen")));
        //resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex");

        //Set quality settings
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityIndex"));
        qualityIndex.value = PlayerPrefs.GetInt("qualityIndex");

        //set volume
        masterVolSlider.value = PlayerPrefs.GetFloat("volumeMaster");
        musicVolSlider.value = PlayerPrefs.GetFloat("volumeMusic");
        effectsVolSlider.value = PlayerPrefs.GetFloat("volumeEffects");
        audioMixer.SetFloat("VolumeMaster", masterVolSlider.value);
        audioMixer.SetFloat("VolumeMusic", musicVolSlider.value);
        audioMixer.SetFloat("VolumeEffects", effectsVolSlider.value);

        //set fullscreen
        if(PlayerPrefs.GetInt("isFullscreen") == 1)
        {
            Screen.fullScreen = true;
            fullscreenButton.isOn = true;
        }
        else if(PlayerPrefs.GetInt("isFullscreen") == 0)
        {
            Screen.fullScreen = false;
            fullscreenButton.isOn = false;
        }


    }

    public void SaveSettings()
    {
        Debug.Log(PlayerPrefs.GetFloat("volumeMusic"));
        PlayerPrefs.Save();

    }

   public void HideOptions()
    {
        CanvasGroup optionsUI = gameObject.GetComponent<CanvasGroup>();
        optionsUI.alpha = 0;
        optionsUI.interactable = false;
        optionsUI.blocksRaycasts = false;
    }

    public void ShowOptions()
    {
        CanvasGroup optionsUI = gameObject.GetComponent<CanvasGroup>();
        optionsUI.alpha = 1;
        optionsUI.interactable = true;
        optionsUI.blocksRaycasts = true;
    }

    



}
