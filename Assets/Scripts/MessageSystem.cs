using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem Instance { get; private set; }

    public static User ActiveUser;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject myMessagePrefab;
    [SerializeField] private GameObject othersMessagePrefab;
    [SerializeField] private Button sendButton;
    [SerializeField] private Button showDeletesButton;
    [SerializeField] private Button hideDeletesButton;
    [SerializeField] private GameObject deleteConfirmationPanel;

    [HideInInspector] public List<MessageElement> messageElements;
    [HideInInspector] public List<GameObject> deleteButtons;

    public Action onUserChanged;
    private User owner;

    private void Awake()
    {
        DOTween.Init();

        if (Instance == null) Instance = this;

        messageElements = new List<MessageElement>();
        deleteButtons = new List<GameObject>();

        inputField.onSubmit.AddListener(arg0 => { Send(); });
        sendButton.onClick.AddListener(Send);
        showDeletesButton.onClick.AddListener(() => ShowDeletes(true));
        hideDeletesButton.onClick.AddListener(() => ShowDeletes(false));
    }

    private void Start()
    {
        ActiveUser = owner = GameSettings.GameConfig.owner;
    }
    
    public void CheckMessages(MessageElement element)
    {
        var prevMessage = GetPrevMessageElement(element);
        var nextMessage = GetNextMessageElement(element);

        if (prevMessage && element.IsHead && prevMessage.Message.sender == element.Message.sender)
        {
            prevMessage.ShowAdditionalInfo(true);
        }

        if (prevMessage && nextMessage && prevMessage.Message.sender == nextMessage.Message.sender)
        {
            prevMessage.ShowAdditionalInfo(false);
        }
    }

    private MessageElement GetNextMessageElement(MessageElement current)
    {
        int currentIndex = messageElements.IndexOf(current);
        return currentIndex + 1 < messageElements.Count ? messageElements[currentIndex + 1] : null;
    }

    private MessageElement GetPrevMessageElement(MessageElement current)
    {
        int currentIndex = messageElements.IndexOf(current);
        return currentIndex > 0 ? messageElements[currentIndex - 1] : null;
    }

    public MessageElement GetLastMessageElement()
    {
        return messageElements.Count > 0 ? messageElements[messageElements.Count - 1] : null;
    }
    
    private void Send()
    {
        if (inputField.text == string.Empty)
        {
            return;
        }
        
        GameObject messageObject = ActiveUser == owner ? myMessagePrefab : othersMessagePrefab;

        FormMessage(ActiveUser.userName, inputField.text, messageObject);
        inputField.ActivateInputField();
    }

    private void FormMessage(string sender, string text, GameObject messageObject)
    {
        Message message = new Message(sender, text);
        inputField.text = string.Empty;
        MessageElement messageElement = Instantiate(messageObject, content).GetComponent<MessageElement>();
        messageElement.SetMessage(message);
        messageElements.Add(messageElement);
    }

    private void ShowDeletes(bool value)
    {
        deleteConfirmationPanel.SetActive(value);
        
        foreach (GameObject button in deleteButtons)
        {
            button.SetActive(value);
        }
    }
}