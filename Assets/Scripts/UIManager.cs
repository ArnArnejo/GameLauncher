using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button CreateAccountBtn;
    public Button BackToLoginBtn;
    public Button SignupBtn;
    public Button loginBtn;
    public Button CancelLoginBtn;
    public Button CancelSignupBtn;

    [Header("Texts")]
    public TextMeshProUGUI SignupErrorMessage;
    public TextMeshProUGUI LoginErrorMessage;

    [Header("InputFields")]
    public TMP_InputField loginUsername;
    public TMP_InputField loginPassword;
    public TMP_InputField signupUsername;
    public TMP_InputField signupEmail;
    public TMP_InputField signupPassword;
    public TMP_InputField signupConfirmPassword;

    [Header("Gamobjects")]
    public GameObject LoginPanel;
    public GameObject SignupPanel;
    public GameObject SuccesPanel;


    private CreateUser signupUser = new CreateUser();
    private LoginUser loginUser = new LoginUser();
    private LoginManager _loginManager => LoginManager.Instance;

    private void Start()
    {
        SignupErrorMessage.text = "";
        LoginErrorMessage.text = "";
        SuccesPanel.SetActive(false);
        CreateAccountBtn.onClick.AddListener(EnableSignup);
        BackToLoginBtn.onClick.AddListener(EnableLogin);
        SignupBtn.onClick.AddListener(UserSignup);
        loginBtn.onClick.AddListener(userLogin);
        CancelLoginBtn.onClick.AddListener(clearFields);
        CancelSignupBtn.onClick.AddListener(clearFields);

    }
    private void EnableSignup()
    {
        SignupPanel.SetActive(true);
        LoginPanel.SetActive(false);
    }
    private void EnableLogin()
    {
        SignupPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
    private void UserSignup() {
        //SignupBtn.enabled = false;
        CheckSignupInformation(signupUsername.text, signupEmail.text, signupPassword.text, signupConfirmPassword.text);
    }
    private void userLogin() {
        loginUser.username = loginUsername.text;
        loginUser.password = loginPassword.text;
        _loginManager.Login(loginUser);
    }
    public void CheckSignupInformation(string username, string email, string password, string confirmPass) {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email)
            || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPass)) {
            SignupErrorMessage.text = "Please Enter User Information";
            return;
        }
        if (username.Length < 8 || password.Length < 8) {
            SignupErrorMessage.text = "Username or Password should be 8 or more characters";
            return;
        }
        if (password != confirmPass) {
            SignupErrorMessage.text = "Passwords do not match";
            return;
        }

        signupUser.username = username;
        signupUser.password = password;
        signupUser.email = email;
        print("Signup");
        _loginManager.CreateUser(signupUser);

    }
    public void SuccesSignup(string message, bool isSucces) {
        print("Sucsees Signup");
        StartCoroutine(DisplaySuccess(message, isSucces));
    }
    IEnumerator DisplaySuccess(string message , bool isSucces) {
        SuccesPanel.SetActive(true);
        TextMeshProUGUI text = SuccesPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = message;
        yield return new WaitForSeconds(1f);
        text.text = "";
        SuccesPanel.SetActive(false);
        if (isSucces) {
            EnableLogin();
            clearFields();
            SignupBtn.enabled = true;
        }
            

    }
    private void clearFields() {
        loginUsername.text = "";
        loginPassword.text = "";
        signupUsername.text = "";
        signupEmail.text = "";
        signupPassword.text = "";
        signupConfirmPassword.text = "";

    }
    

}
