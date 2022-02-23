using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersPanelSetter : MonoBehaviour
{
    
    [SerializeField] private Transform content;
    [SerializeField] private GameObject userButtonPrefab;
    
    private List<User> users;

    private void Start()
    {
        users = GameSettings.GameConfig.users;
        
        foreach (var user in users)
        {
            UserButton button = Instantiate(userButtonPrefab, content).GetComponent<UserButton>();
            button.user = user;
        }
    }
}
