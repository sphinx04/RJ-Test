using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Message
{
    public readonly string sender;
    public readonly string text;
    public readonly string timestamp;

    public Message(string sender, string text)
    {
        this.sender = sender;
        this.text = text;
        timestamp = System.DateTime.Now.ToString("HH:mm:ss");
    }
}
