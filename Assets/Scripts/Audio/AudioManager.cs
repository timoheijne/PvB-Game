using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public enum AudioChannel { Master, Sfx, Music };

    public float MasterVolumePercent { get; private set; }
    public float SfxVolumePercent { get; private set; }
    public float MusicVolumePercent { get; private set; }

    private AudioSource _sfx2DSource;
    private AudioSource[] _musicSources;
    private int _activeMusicSourceIndex;

    public static AudioManager instance;

    private Transform _audioListener;
    private Transform _playerTransform;

    private SoundLibrary _library;

    void Awake() 
    {

        if (instance != null) 
        {
            Destroy(gameObject);
        } 
        else 
        {

            instance = this;
            DontDestroyOnLoad(gameObject);

            _library = GetComponent<SoundLibrary>();

            _musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++) 
            {
                GameObject _newMusicSource = new GameObject("Music source " + (i + 1));
                _musicSources[i] = _newMusicSource.AddComponent<AudioSource>();
                _newMusicSource.transform.parent = transform;
            }
            GameObject _newSfx2Dsource = new GameObject("2D sfx source");
            _sfx2DSource = _newSfx2Dsource.AddComponent<AudioSource>();
            _newSfx2Dsource.transform.parent = transform;

            _audioListener = FindObjectOfType<AudioListener>().transform;
            //if (FindObjectOfType<Player>() != null) {
            //    playerTransform = FindObjectOfType<Player>().transform;
            //}

            MasterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
            SfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
            MusicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
        }
    }

    void Update() 
    {
        if (_playerTransform != null) 
        {
            _audioListener.position = _playerTransform.position;
        }
    }

    public void SetVolume(float volumePercent, AudioChannel channel) 
    {
        switch (channel) 
        {
            case AudioChannel.Master:
                MasterVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                SfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                MusicVolumePercent = volumePercent;
                break;

            default:
                throw new NotSupportedException();
        }

        _musicSources[0].volume = MusicVolumePercent * MasterVolumePercent;
        _musicSources[1].volume = MusicVolumePercent * MasterVolumePercent;

        PlayerPrefs.SetFloat("master vol", MasterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", SfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", MusicVolumePercent);
        PlayerPrefs.Save();
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1) 
    {
        _activeMusicSourceIndex = 1 - _activeMusicSourceIndex;
        _musicSources[_activeMusicSourceIndex].clip = clip;
        _musicSources[_activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos) 
    {
        if (clip != null) 
        {
            AudioSource.PlayClipAtPoint(clip, pos, SfxVolumePercent * MasterVolumePercent);
        }
    }

    public void PlaySound(string soundName, Vector3 pos) {
        PlaySound(_library.GetClipFromName(soundName), pos);
    }


        public void PlaySound2D(string soundName) {
            _sfx2DSource.PlayOneShot(_library.GetClipFromName(soundName), SfxVolumePercent * MasterVolumePercent);
        }


        IEnumerator AnimateMusicCrossfade(float duration) {
            float _percent = 0;

            while (_percent < 1) {
                _percent += Time.deltaTime * 1 / duration;
                _musicSources[_activeMusicSourceIndex].volume = Mathf.Lerp(0, MusicVolumePercent * MasterVolumePercent, _percent);
                _musicSources[1 - _activeMusicSourceIndex].volume = Mathf.Lerp(MusicVolumePercent * MasterVolumePercent, 0, _percent);
                yield return null;
            }
        }
}