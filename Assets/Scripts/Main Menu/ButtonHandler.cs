using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private List<GameObject> _items;

    private void Start() 
    {
        _items = new List<GameObject>();        
    }

    public void ToggleMenu(GameObject _menu) 
    {
        if (_menu != null)
        {
            if (!_menu.activeInHierarchy)
            {
                _menu.SetActive(true);
            } 
            else 
            {
                _menu.SetActive(false);
            }
        }
    }

    public void SetItemList(Transform _menu) 
    {
        Debug.Log(_items.Count);
        if (_items.Count == 0) 
        {
            _items = new List<GameObject>();
            Debug.Log("incomming for loop");
            for (int i = 0; i < _menu.GetChildCount(); i++) 
            {
                Debug.Log("loop " + i);
                _items.Add(_menu.GetChild(i).gameObject);
            }

            Debug.Log(_items.Count);
        }
    }

    public void ToggleSelectionMenu(int _targetItemValue) 
    {
        foreach (GameObject _item in _items) 
        {
            Debug.Log("yesh");

            if (_item != null) 
            {
                _item.SetActive(false);

                if (_item == _items[_targetItemValue]) 
                {
                    _item.SetActive(true);
                }
            }
        }
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
