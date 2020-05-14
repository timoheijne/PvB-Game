using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectSlider : MonoBehaviour
{
    private int position;
    private int maxPosition;

    private void Start()
    {
        if (LevelSystem.Instance != null)
        {
            maxPosition = Mathf.FloorToInt(LevelSystem.Instance.GetAllLevels().Count / 8f);
        }
    }

    public void PrevLevelGroup()
    {
        if (position > 0)
        {
            position--;
            //TODO: add an animation
            transform.position += Vector3.right * 1600;
        }
    }

    public void NextLevelGroup()
    {
        if (position < maxPosition)
        {
            position++;
            //TODO: add an animation
            transform.position += Vector3.left * 1600;
        }
    }
}
