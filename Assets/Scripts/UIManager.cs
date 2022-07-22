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
    public TextMeshProUGUI errorMessage;

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
        errorMessage.text = "";
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
            errorMessage.text = "Please Enter User Information";
            return;
        }
        if (username.Length < 8 || password.Length < 8) {
            errorMessage.text = "Username or Password should be 8 or more characters";
            return;
        }
        if (password != confirmPass) {
            errorMessage.text = "Passwords do not match";
            return;
        }

        signupUser.username = username;
        signupUser.password = password;
        signupUser.email = email;

        _loginManager.CreateUser(signupUser);

    }
    public void SuccesSignup() {
        StartCoroutine(DisplaySuccess());
    }
    IEnumerator DisplaySuccess() {
        SuccesPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SuccesPanel.SetActive(false);
        EnableLogin();
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
