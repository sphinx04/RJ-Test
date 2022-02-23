using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageElement : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private TextMeshProUGUI username;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI timestamp;
    [SerializeField] private Image bubbleImage;
    [SerializeField] private Image bubbleImageHead;
    [SerializeField] private Image avatar;
    [SerializeField] private VerticalLayoutGroup vlg;
    [SerializeField] private Button deleteButton;
    [SerializeField] private float delay = .15f;

    public bool IsHead { get; private set; }
    public Message Message { get; private set; }
    
    private void Start()
    {
        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(Delete);
            MessageSystem.Instance.deleteButtons.Add(deleteButton.gameObject);
        }

        IsHead = true;
        body.localScale = Vector3.zero;
        body.DOScale(Vector3.one, delay);
    }

    public void SetMessage(Message message)
    {
        Message = message;
        username.text = message.sender;
        text.text = message.text;
        timestamp.text = message.timestamp;
        avatar.sprite = MessageSystem.ActiveUser.icon;
        
        if (MessageSystem.Instance.messageElements.Count > 0 && MessageSystem.Instance.GetLastMessageElement().Message.sender == message.sender)
        {
            MessageSystem.Instance.GetLastMessageElement().ShowAdditionalInfo(false);
        }
    }

    public void ShowAdditionalInfo(bool value)
    {
        IsHead = value;
        username.gameObject.SetActive(value);
        bubbleImageHead.enabled = value;
        bubbleImage.enabled = !value;
        avatar.gameObject.SetActive(value);
        vlg.padding.bottom = value ? 32 : 0;
    }

    private void Delete()
    {
        MessageSystem.Instance.CheckMessages(this);
        MessageSystem.Instance.messageElements.Remove(this);
        MessageSystem.Instance.deleteButtons.Remove(deleteButton.gameObject);
        body.localScale = Vector3.one;
        body.DOScale(Vector3.zero, delay);
        Destroy(gameObject, delay);
    }
}
