using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class SpriteHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Sprite _currentSprite;

    [SerializeField]
    private Sprite[] _spriteState;

    private void Start()
    {
        if (gameObject.GetComponent<Image>() != null)
        {
            _currentSprite = gameObject.GetComponent<Image>().sprite;;
        } 
    }

    public void OnPointerClick(PointerEventData eventData) 
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

    public void OnPointerEnter(PointerEventData eventData) 
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

    public void OnPointerExit(PointerEventData eventData) 
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

    private void ChangeSprite(string _newState)
    {
        switch (_newState)
        {
            case "Idle":
                _currentSprite = _spriteState[0];
                break;

            case "Hover":
                _currentSprite = _spriteState[1];
                break;

            case "Click":
                _currentSprite = _spriteState[2];
                break;

            default:
                break;
        }
    }
}