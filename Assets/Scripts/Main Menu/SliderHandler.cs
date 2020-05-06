using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SliderHandler : MonoBehaviour
{
    public Slider[] VolumeSliders;
    private Sprite _currentSprite;
    private string _currentSpritePath;

    private void Start() 
    {
        if (_currentSpritePath != null) 
        {
            _currentSprite = GameObject.Find("Image_Audio").GetComponent<Image>().sprite;
        }
        VolumeSliders[0].value = AudioManager.instance.MasterVolumePercent;
        VolumeSliders[1].value = AudioManager.instance.MusicVolumePercent;
        VolumeSliders[2].value = AudioManager.instance.SfxVolumePercent;
    }

    public void SetMasterVolume(float value) 
    {
        if (value >= 0f && _currentSpritePath == Resources.Load("Assets/Sprites/Buttons/Click/Button_Click_Geluid_Gedempt").ToString()) 
        {
            _currentSpritePath = Resources.Load("Assets/Sprites/Buttons/Click/Button_Click_Geluid").ToString();
            GameObject.Find("Image_Audio").gameObject.GetComponent<Image>().sprite = Resources.Load(_currentSpritePath.ToString()) as Sprite;
        } 
        else if (value == 0 && _currentSpritePath != Resources.Load("Assets/Sprites/Buttons/Click/Button_Click_Geluid_Gedempt").ToString()) 
        {
            _currentSpritePath = Resources.Load("Assets/Sprites/Buttons/Click/Button_Click_Geluid").ToString();
            GameObject.Find("Image_Audio").gameObject.GetComponent<Image>().sprite = Resources.Load(_currentSpritePath.ToString()) as Sprite;
        }

        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value) 
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value) 
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }
}
