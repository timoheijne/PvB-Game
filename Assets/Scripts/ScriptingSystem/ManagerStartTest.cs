using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStartTest : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.Pause(false);
            gameManager.ResetNodeHolders();
        }
    }
}
