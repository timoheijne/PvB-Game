using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip MainTheme;
    public AudioClip MenuTheme;

    void Start() 
    {
        AudioManager.instance.PlayMusic(MenuTheme, 2);
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            AudioManager.instance.PlayMusic(MainTheme, 3);
        }

    }
}
