using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject groupPrefab;
    [SerializeField] private GameObject levelButtonPrefab;

    [SerializeField] private float xOffset = 1600;

    public int Groups;

    private LevelSystem levelSystem;
    
    void Start()
    {
        levelSystem = LevelSystem.Instance;

        CreateLevelSelect();
    }

    public void CreateLevelSelect()
    {
        List<LevelObject> _levels = levelSystem.GetAllLevels();
        for (int i = 0; _levels.Count > 0; i++)
        {
            int levelsOnNextBlock = Mathf.Min(_levels.Count, 8);
            LevelObject[] objects = new LevelObject[levelsOnNextBlock];
            for (int j = 0; j < levelsOnNextBlock; j++)
            {
                objects[j] = _levels[j];
            }
            _levels.RemoveRange(0, levelsOnNextBlock);

            CreateButtonGroup(objects, i * xOffset);
        }
    }

    public void CreateButtonGroup(LevelObject[] _levels, float _xOffset)
    {
        GameObject group = Instantiate(groupPrefab, transform.position + Vector3.right * _xOffset, transform.rotation, transform);
        

        for (int i = 0; i < _levels.Length; i++)
        {
            GameObject button = CreateButton(_levels[i], group.transform);
        }
    }


    public GameObject CreateButton(LevelObject _level, Transform _parent)
    {
        GameObject button = Instantiate(levelButtonPrefab, _parent);
        button.GetComponent<Button>()?.onClick.AddListener(() => levelSystem.ChangeLevel(_level));
        return button;
    }
}
