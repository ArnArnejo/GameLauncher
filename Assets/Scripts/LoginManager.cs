using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System;


[Serializable]
public class CreateUser {
    public string username;
    public string password;
    public string email;
}

[Serializable]
public class LoginUser {
    public string username;
    public string password;
}

public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance;
    [SerializeField]private GameManager _gameManager => GameManager.Instance;
    [SerializeField] private LoadManager _loadManager;

    //[Header("Links")]
    //public string SignupURL;
    //public string LoginURL;

    [Header("UIManager")]
    public UIManager uiManager;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void CreateUser(CreateUser user) {
        StartCoroutine(AddUser(user));
    }

    public IEnumerator AddUser(CreateUser user)
    {

        print("ADD USER");
        WWWForm form = new WWWForm();
        form.AddField("addUsername", user.username);
        form.AddField("addEmail", user.email);
        form.AddField("addPassword", user.password);

        UnityWebRequest webRequest = UnityWebRequest.Post(_gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.SignupURL.ToString()), form);
        yield return webRequest.SendWebRequest();

        if (webRequest.downloadHandler.text.Contains("Exists"))
        {
            uiManager.SuccesSignup("User Already Exists!", false);
        }
        else if (webRequest.downloadHandler.text.Contains("Failed"))
        {
            uiManager.SuccesSignup("Failed to Register Please Try Again", false);
        }
        else if (webRequest.downloadHandler.text.Contains("Success")) {
            uiManager.SuccesSignup("User Created Succesfully!", true);
        }

        print(webRequest.downloadHandler.text);
        
    }

    public void Login(LoginUser user) {
        StartCoroutine(LoginUser(user));
    }

    IEnumerator LoginUser(LoginUser user) {
        WWWForm form = new WWWForm();
        form.AddField("loginUsername", user.username);
        form.AddField("loginPassword", user.password);

        UnityWebRequest webRequest = UnityWebRequest.Post(_gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.LoginURL.ToString()), form);
        print(_gameManager.GetURL(eURLS.Root.ToString()) + _gameManager.GetURL(eURLS.LoginURL.ToString()));
        yield return webRequest.SendWebRequest();
        print(webRequest.downloadHandler.text);
        string[] result = webRequest.downloadHandler.text.Split(';');
        CheckLoginResult(result);
    }

    public void CheckLoginResult(string[] data) {

        string result = HelperScript.GetValueData(data[0], "Result:");

        if (result.Contains("Success"))
        {
            print(result);
            _gameManager.AccountManager.userID = HelperScript.GetValueData(data[0], "ID:");
            _gameManager.AccountManager.username = HelperScript.GetValueData(data[0], "username:");
            _gameManager.AccountManager.email = HelperScript.GetValueData(data[0], "email:");
            _gameManager.AccountManager.walletBallance = float.Parse(HelperScript.GetValueData(data[0], "walletBalance:"));
            _loadManager.LoadScene(1);
        }
        else if (result.Contains("Invalid")) {
            print(result);
            uiManager.LoginErrorMessage.text = result;
        }


    }
}
