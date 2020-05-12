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

        CreateLevelSelect();
    }

    public void CreateLevelSelect()
    {

    }

    public void CreateButtonGroup(LevelObject[] _levels, Vector2 size, Vector2 cellSize, Vector2 spacing)
    {
        GameObject group = new GameObject();
        group.transform.parent = transform;
        RectTransform rect = group.AddComponent<RectTransform>();
        GridLayoutGroup gridLayout = group.AddComponent<GridLayoutGroup>();
        gridLayout.SetLayoutHorizontal();

        for (int i = 0; i < _levels.Length; i++)
        {
            GameObject button = CreateButton(_levels[i], group.transform);
        }
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
