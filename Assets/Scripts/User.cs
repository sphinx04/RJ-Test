using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class User : ScriptableObject
{
    public string userName;
    public Sprite icon;
}
