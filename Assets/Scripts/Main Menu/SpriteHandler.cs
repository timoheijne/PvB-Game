using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SpriteHandler : MonoBehaviour
{
    public Sprite CurrentSprite;

    private void Start()
    {
        if (gameObject.GetComponent<Image>() != null)
        {
            CurrentSprite = gameObject.GetComponent<Image>().sprite;
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.GetComponent<Image>().sprite != null)
        {
            ChangeSprite("Click");
        }
        else
        {
            print("Sprite not found");
        }
    }

    private void OnMouseOver()
    {
        if (gameObject.GetComponent<Image>().sprite != null)
        {
            print("ohai");
            ChangeSprite("Hover");
        }
        else
        {
            print("Sprite not found");
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.GetComponent<Image>().sprite != null)
        {
            print("obai");
            ChangeSprite("Idle");
        }
        else
        {
            print("Sprite not found");
        }
    }

    private string FindSprite()
    {
        return AssetDatabase.GetAssetPath(CurrentSprite);
    }

    private void ChangeSprite(string _newState)
    {
        Sprite _newSprite;
        string _currentState;

        switch (_newState)
        {
            case "Idle":
                _currentState = Resources.Load(FindSprite().Contains("Hover") ? "Hover" : "Click").ToString();
                _newSprite = Resources.Load(FindSprite().Replace(_currentState, "Idle")) as Sprite;
                CurrentSprite = _newSprite;
                break;

            case "Hover":
                _currentState = Resources.Load(FindSprite().Contains("Idle") ? "Idle" : "Click").ToString();
                _newSprite = Resources.Load(FindSprite().Replace(_currentState, "Hover")) as Sprite;
                CurrentSprite = _newSprite;
                break;

            case "Click":
                _currentState = Resources.Load(FindSprite().Contains("Hover") ? "Idle" : "Hover").ToString();
                _newSprite = Resources.Load(FindSprite().Replace(_currentState, "Click")) as Sprite;
                CurrentSprite = _newSprite;
                break;

            default:
                break;
        }
    }
}
