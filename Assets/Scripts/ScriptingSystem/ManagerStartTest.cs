using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStartTest : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.ResetNodeHolders();
        }
    }
}
