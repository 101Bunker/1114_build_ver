using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{

    [Header("Other")]
    public GameObject loginRegisterBox;
    public GameObject loggedInText;

    [Header("Login/register box")]
    public Text messageText;
    public InputField emailInput;
    public InputField passwordInput;

    // Registering
    [SerializeField] UserInfos userInfos;
    public void RegisterButton() {
        var request = new RegisterPlayFabUserRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result) {
        messageText.text = "Registered and logged in!";
        loginRegisterBox.SetActive(false);
        loggedInText.SetActive(true);
    }

    // Logging in
    public void LoginButton() {
        var request = new LoginWithEmailAddressRequest {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        userInfos.userId = emailInput.text;
    }

    void OnLoginSuccess(LoginResult result) {
        messageText.text = "Logged in!";
        loginRegisterBox.SetActive(false);
        loggedInText.SetActive(true);
    }

    // Forgot password
    public void ResetPasswordButton() {
        var request = new SendAccountRecoveryEmailRequest {
            Email = emailInput.text,
            TitleId = PlayFabSettings.staticSettings.TitleId
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnForgotPasswordSuccess, OnError);
    }
    void OnForgotPasswordSuccess(SendAccountRecoveryEmailResult result) {
        messageText.text = "Sent password recovery link!";
    }
    

    void OnError(PlayFabError error) {
        messageText.text = "Error: " + error.ErrorMessage;
    }
}
