using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] Vector2[] _availableResolutions;
    [SerializeField] TextMeshProUGUI _resolutionText;
    [SerializeField] TextMeshProUGUI _fullScreenText;
    [SerializeField] Slider _volumeSlider;
    private float _tempVolume;
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        UpdateOptions();
    }

    public void UpdateOptions()
    {
        if (_availableResolutions.Contains(new Vector2(PlayerPrefs.GetInt("ResolutionX", 0), PlayerPrefs.GetInt("ResolutionY", 0))))
        {
            _resolutionText.text = "" + PlayerPrefs.GetInt("ResolutionX", 1920) + " X " + PlayerPrefs.GetInt("ResolutionY", 1080);
        }
        else
        {
            PlayerPrefs.SetInt("ResolutionX", (int)_availableResolutions[0].x);
            PlayerPrefs.SetInt("ResolutionX", (int)_availableResolutions[0].y);
            _resolutionText.text = "" + PlayerPrefs.GetInt("ResolutionX", 1920) + " X " + PlayerPrefs.GetInt("ResolutionY", 1080);
            SetResolution();
        }
        if (PlayerPrefs.GetInt("FullScreen", 2) < 2)
        {
            if (PlayerPrefs.GetInt("FullScreen", 2) < 1)
                _fullScreenText.text = "Windowed";
            else
                _fullScreenText.text = "FullScreen";

        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 1);
            SetResolution();
        }

        SetVolume(PlayerPrefs.GetFloat("Volume", -20f));
        _volumeSlider.value = PlayerPrefs.GetFloat("Volume", -20f);



    }

    void SetResolution()
    {
        bool fullScreen;
        if (PlayerPrefs.GetInt("FullScreen", 2) > 0)
            fullScreen = true;
        else
            fullScreen = false;

        Screen.SetResolution(PlayerPrefs.GetInt("ResolutionX", 1920), PlayerPrefs.GetInt("ResolutionY", 1080), fullScreen);
    }

    public void ToggleFullScreen()
    {
        if (PlayerPrefs.GetInt("FullScreen", 2) < 1)
        {
            PlayerPrefs.SetInt("FullScreen", 1);
            _fullScreenText.text = "FullScreen";
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);
            _fullScreenText.text = "Windowed";
        }
        SetResolution();
    }

    public void NextResolution()
    {
        Vector2 currentResolution = new Vector2(PlayerPrefs.GetInt("ResolutionX", 0), PlayerPrefs.GetInt("ResolutionY", 0));
        if (_availableResolutions.Contains(currentResolution))
        {
            currentResolution = _availableResolutions[(Array.IndexOf(_availableResolutions, currentResolution) + 1) % _availableResolutions.Length];
        }
        else
        {
            currentResolution = _availableResolutions[0];
        }

        PlayerPrefs.SetInt("ResolutionX", (int)currentResolution.x);
        PlayerPrefs.SetInt("ResolutionY", (int)currentResolution.y);
        _resolutionText.text = "" + PlayerPrefs.GetInt("ResolutionX", 1920) + " X " + PlayerPrefs.GetInt("ResolutionY", 1080);
        SetResolution();
    }

    public void SetVolume(float volume)
    {
        Debug.Log("Volume: " + volume);
        _audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

}
