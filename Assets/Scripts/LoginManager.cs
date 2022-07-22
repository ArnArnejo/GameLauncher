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
    [SerializeField]private AccountManager _accountManager;
    [SerializeField] private LoadManager _loadManager;

    [Header("Links")]
    public string SignupURL;
    public string LoginURL;

    [Header("UIManager")]
    public UIManager uiManager;


    public Image icon;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    string text = HelperScript.ImageToBase64(icon);
        //    print(text);
        //}
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void CreateUser(CreateUser user) {
        StartCoroutine(AddUser(user));
    }

    public IEnumerator AddUser(CreateUser user)
    {
        WWWForm form = new WWWForm();
        form.AddField("addUsername", user.username);
        form.AddField("addEmail", user.email);
        form.AddField("addPassword", user.password);

        UnityWebRequest webRequest = UnityWebRequest.Post(SignupURL, form);
        yield return webRequest.SendWebRequest();

        uiManager.SuccesSignup();
    }

    public void Login(LoginUser user) {
        StartCoroutine(LoginUser(user));
    }

    IEnumerator LoginUser(LoginUser user) {
        WWWForm form = new WWWForm();
        form.AddField("loginUsername", user.username);
        form.AddField("loginPassword", user.password);

        UnityWebRequest webRequest = UnityWebRequest.Post(LoginURL, form);
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
            _accountManager.userID = HelperScript.GetValueData(data[0], "ID:");
            _accountManager.username = HelperScript.GetValueData(data[0], "username:");
            _accountManager.email = HelperScript.GetValueData(data[0], "email:");
            _accountManager.walletBallance = float.Parse(HelperScript.GetValueData(data[0], "walletBalance:"));
            _loadManager.LoadScene(1);
        }
        else if (result.Contains("Invalid")) {
            print(result);
        }


    }
}
