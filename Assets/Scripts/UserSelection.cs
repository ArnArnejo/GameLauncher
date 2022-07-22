using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserSelection : MonoBehaviour
{
    //Selection
    public string SelectURL;
    public string[] userData;

    //Insertion
    public string InsertURL;
    public string username, password, email, whereField, whereCondition;


    //Update
    public string UpdateURL;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SelectData());
    }



    IEnumerator SelectData() {
        WWW users = new WWW(SelectURL);
        yield return users;
        string userDataString = users.text;
        userData = userDataString.Split(';');

        print(GetValueData(userData[0], "username:"));
    }
    string GetValueData(string data, string index) {

        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) {
            value = value.Remove(value.IndexOf("|"));
        }

        return value;
    }

    IEnumerator AddUser(string username, string email, string password) {

            WWWForm form = new WWWForm();
            form.AddField("addUsername", username);
            form.AddField("addEmail", email);
            form.AddField("addPassword", password);

            UnityWebRequest webRequest = UnityWebRequest.Post(InsertURL, form);
            yield return webRequest.SendWebRequest();

    }

    IEnumerator UpdateUser(string username, string email, string whereField, string whereCondition)
    {

        WWWForm form = new WWWForm();
        form.AddField("editUsername", username);
        form.AddField("editEmail", email);

        form.AddField("whereField", whereField);
        form.AddField("whereCondition", whereCondition);

        UnityWebRequest webRequest = UnityWebRequest.Post(UpdateURL, form);
        yield return webRequest.SendWebRequest();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //StartCoroutine( AddUser(username, email, password));
            StartCoroutine(UpdateUser(username, email, whereField, whereCondition));
        }
    }
}
