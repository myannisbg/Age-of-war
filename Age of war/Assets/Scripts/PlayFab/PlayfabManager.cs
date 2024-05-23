using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField emailInput;

    public GameObject canvaRegister;
    public GameObject canvaLogin;
    public GameObject canvaProfil;
    public TMP_Text playerIdText;

    // Méthode appelée lorsque l'utilisateur appuie sur le bouton de connexion
    public void OnLoginButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        Login(username, password);
    }

    // Méthode appelée lorsque l'utilisateur appuie sur le bouton d'enregistrement
    public void OnRegisterButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        string email = emailInput.text;
        Register(username, password, email);
    }
    // Méthode pour se connecter à un compte PlayFab
    public void Login(string username, string password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = username,
            Password = password,

            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };


        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    // Méthode pour s'enregistrer avec un nouveau compte PlayFab
    private void Register(string username, string password, string email)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            Username = username,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }


    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in successfully!");
        string name = null;
        name = result.InfoResultPayload.PlayerProfile.DisplayName;
        Debug.Log(name);
        canvaLogin.SetActive(false);
        canvaProfil.SetActive(true);
        string id = result.PlayFabId;
        playerIdText.text = "Player ID: " + id;
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Login failed: " + error.ErrorMessage);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Registered successfully!");
        
        canvaRegister.SetActive(false);
        canvaProfil.SetActive(true);
        string id = result.PlayFabId;
        playerIdText.text = "Player ID: " + id;
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError("Registration failed: " + error.ErrorMessage);
    }
}
