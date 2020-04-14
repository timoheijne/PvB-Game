using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    public Slider[] volumeSliders;

    private void Start() 
    {
        volumeSliders[0].value = AudioManager.instance.MasterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.MusicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.SfxVolumePercent;
    }

    public static void SetMasterVolume(float value) 
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public static void SetMusicVolume(float value) 
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public static void SetSfxVolume(float value) 
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }
}
