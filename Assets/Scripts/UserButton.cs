using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private bool isOwner;

    public User user;

    private void Start()
    {
        if (isOwner)
        {
            user = GameSettings.GameConfig.owner;
        }

        MessageSystem.Instance.onUserChanged += ClearBackGround;
        GetComponent<Button>().onClick.AddListener(ChangeUser);

        icon.sprite = user.icon;
        text.text = user.userName;
    }


    private void ChangeUser()
    {
        MessageSystem.Instance.onUserChanged.Invoke();
        MessageSystem.ActiveUser = user;
        bg.enabled = true;
    }

    private void ClearBackGround()
    {
        bg.enabled = false;
    }
}
