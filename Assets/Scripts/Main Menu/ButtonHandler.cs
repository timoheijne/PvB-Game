﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public static void ToggleMenu(GameObject _menu) 
    {
        if (!_menu.activeInHierarchy) {
            _menu.SetActive(true);
        } 
        else 
        {
            _menu.SetActive(false);
        }
    }

    public static void ExitGame() 
    {
        Application.Quit();
    }
}
