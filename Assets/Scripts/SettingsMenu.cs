using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resDropdown;

    private (int,int)[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions.Select(x=>(x.width, x.height)).Reverse().ToHashSet().ToArray();
        resDropdown.ClearOptions();
        resDropdown.AddOptions(resolutions.Select(x => $"{x.Item1}x{x.Item2}").ToList());
        resDropdown.value = Array.IndexOf(resolutions, (Screen.currentResolution.width, Screen.currentResolution.height));
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool isFS)
    {
        Screen.fullScreen = isFS;
    }

    public void SetResolution(int index)
    {
        (int,int) resolution = resolutions[index];
        Screen.SetResolution(resolution.Item1, resolution.Item2, Screen.fullScreen);
    }
}
