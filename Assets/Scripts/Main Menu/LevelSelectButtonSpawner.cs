using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonSpawner : MonoBehaviour
{
    LevelSystem levelSystem;

    // Start is called before the first frame update
    void Start()
    {
        levelSystem = FindObjectOfType<LevelSystem>();
    }


    public GameObject CreateButton(LevelObject _level, Transform parent)
    {
        GameObject button = new GameObject();
        button.transform.parent = parent;
        RectTransform rect = button.AddComponent<RectTransform>();
        button.AddComponent<Button>().onClick.AddListener(() => levelSystem.ChangeLevel(_level));
        return button;
    }
}
