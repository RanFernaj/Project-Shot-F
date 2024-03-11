using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    static GameUi gameUI;

    private void Awake()
    {
        gameUI = FindObjectOfType<GameUi>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        gameUI.EndScreen();
    }
}
