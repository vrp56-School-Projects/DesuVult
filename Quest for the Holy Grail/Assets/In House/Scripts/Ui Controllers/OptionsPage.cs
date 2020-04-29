using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPage : MonoBehaviour
{
    public Slider SensitivitySlider, VolumeSlider, FOVSlider;

    float initialVolume;
    int initialSensitivity, initialFOV;

    GameObject MainCamera;

    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void OnEnable()
    {
        Initialize();
    }

    public void UpdateOptions()
    {
        // update options in static class
        Options.SetOptions(VolumeSlider.value, (int)SensitivitySlider.value, (int)FOVSlider.value);

        // update options in scene
        // FOV
        MainCamera.GetComponent<Camera>().fieldOfView = Options.GetFOV();

        // Volume
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = Options.GetVolume();
        }
    }

    public void ResetOptions()
    {
        VolumeSlider.value = initialVolume;
        SensitivitySlider.value = initialSensitivity;
        FOVSlider.value = initialFOV;
    }

    public void Initialize()
    {
        initialVolume = Options.GetVolume();
        initialSensitivity = Options.GetSensitivity();
        initialFOV = Options.GetFOV();

        ResetOptions();
    }
}
