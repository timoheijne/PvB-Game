using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class SpriteHandler : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField]
    private Sprite[] _spriteState;

    public void OnPointerDown(PointerEventData eventData) 
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

    public void OnPointerUp(PointerEventData eventData) 
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
                gameObject.GetComponent<Image>().sprite = _spriteState[0];
                break;

            case "Hover":
                gameObject.GetComponent<Image>().sprite = _spriteState[1];
                break;

            case "Click":
                gameObject.GetComponent<Image>().sprite = _spriteState[2];
                break;

            default:
                break;
        }
    }
}