using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ButtonHandler : MonoBehaviour
{
    private Sprite _currentSprite;

    private void Start()
    {
        if (gameObject.GetComponent<Image>() != null)
        {
            _currentSprite = gameObject.GetComponent<Image>().sprite;
        }
    }

    public void ToggleMenu(GameObject _menu) 
    {
        if (!_menu.activeInHierarchy) {
            _menu.SetActive(true);
        } 
        else 
        {
            _menu.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        if (gameObject.GetComponent<Image>() != null)
        {
            ChangeSprite("Click");
        }
    }

    public void OnMouseOver()
    {
        if (gameObject.GetComponent<Image>() != null)
        {
            print("ohai");
            ChangeSprite("Hover");
        }
    }

    public void OnMouseExit()
    {
        if (gameObject.GetComponent<Image>() != null)
        {
            print("obai");
            ChangeSprite("Idle");
        }
    }

    public string FindSprite()
    {
        return AssetDatabase.GetAssetPath(_currentSprite);
    }

    public void ChangeSprite(string _newState)
    {
        Sprite _newSprite;
        string _currentState;

        switch (_newState)
        {
            case "Idle":
                _currentState = Resources.Load(FindSprite().Contains("Hover") ? "Hover" : "Click").ToString();
                _newSprite = Resources.Load(FindSprite().Replace(_currentState,"Idle")) as Sprite;
                _currentSprite = _newSprite;
                break;

            case "Hover":
                _currentState = Resources.Load(FindSprite().Contains("Idle") ? "Idle" : "Click").ToString();
                _newSprite = Resources.Load(FindSprite().Replace(_currentState, "Hover")) as Sprite;
                _currentSprite = _newSprite;
                break;

            case "Click":
                _currentState = Resources.Load(FindSprite().Contains("Hover") ? "Idle" : "Hover").ToString();
                _newSprite = Resources.Load(FindSprite().Replace(_currentState, "Click")) as Sprite;
                _currentSprite = _newSprite;
                break;

            default:
                break;
        }
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
