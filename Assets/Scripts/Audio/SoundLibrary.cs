using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour 
{

    public SoundGroup[] SoundGroups;

    private readonly Dictionary<string,AudioClip[]> _groupDictionary = new Dictionary<string, AudioClip[]>();

    void Awake() 
    {
        foreach (SoundGroup soundGroup in SoundGroups) 
        {
            _groupDictionary.Add(soundGroup.groupID, soundGroup.group);
        }
    }

    public AudioClip GetClipFromName(string name) 
    {
        if (_groupDictionary.ContainsKey(name)) 
        {
            AudioClip[] _sounds = _groupDictionary[name];
            return _sounds[Random.Range(0, _sounds.Length)];
        }
        return null;
    }

    [System.Serializable]
    public class SoundGroup 
    {
        public string groupID;
        public AudioClip[] group;
    }
}