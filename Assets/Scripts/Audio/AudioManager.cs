using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public enum AudioChannel { Master, Sfx, Music };

    public float masterVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }

    private AudioSource _sfx2DSource;
    private AudioSource[] _musicSources;
    private int _activeMusicSourceIndex;

    public static AudioManager instance;

    private Transform _audioListener;
    private Transform _playerTransform;

    SoundLibrary library;

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

            library = GetComponent<SoundLibrary>();

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

            masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
            sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
            musicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
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
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
        }

        _musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        _musicSources[1].volume = musicVolumePercent * masterVolumePercent;

        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
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
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
        }
    }

    public void PlaySound(string soundName, Vector3 pos) {
        PlaySound(library.GetClipFromName(soundName), pos);
    }


        public void PlaySound2D(string soundName) {
            _sfx2DSource.PlayOneShot(library.GetClipFromName(soundName), sfxVolumePercent * masterVolumePercent);
        }


        IEnumerator AnimateMusicCrossfade(float duration) {
            float _percent = 0;

            while (_percent < 1) {
                _percent += Time.deltaTime * 1 / duration;
                _musicSources[_activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, _percent);
                _musicSources[1 - _activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, _percent);
                yield return null;
            }
        }
}