using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    
    public static GameSettings Instance { get; private set; }
    public static GameConfig GameConfig;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("GameSettings exists!");
        }

        if (GameConfig == null)
        {
            GameConfig = gameConfig;
        }
    }
}
