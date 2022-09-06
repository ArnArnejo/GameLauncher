using System;
using UnityEngine;


[CreateAssetMenu(menuName="AccountManager")]
public class AccountManager : ScriptableObject
{
    public string userID;
    public string username;
    public string email;
    public float walletBallance;
    public float Admin;
}
