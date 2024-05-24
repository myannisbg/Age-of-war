using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    public InputField usernameInput; // Champ pour le pseudonyme
    public InputField passwordInput;
    public InputField emailInput;

    public GameObject canvaRegister;
    public GameObject canvaLogin;
    public GameObject canvaProfil;
    public TMP_Text playerIdText;
    public TMP_Text playerNameText; // Pour afficher le pseudonyme

    // Méthode appelée lorsque l'utilisateur appuie sur le bouton de connexion
    public void OnLoginButtonClicked()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        Login(email, password);
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
    public void Login(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
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

        PlayFabClientAPI.RegisterPlayFabUser(request, result => OnRegisterSuccess(result, username), OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result, string username)
    {
        Debug.Log("Registered successfully!");

        // Mettre à jour le DisplayName du joueur
        UpdatePlayerDisplayName(username);

        canvaRegister.SetActive(false);
        canvaProfil.SetActive(true);

        string id = result.PlayFabId;
        playerIdText.text = "Player ID: " + id;
        playerNameText.text = "Player Name: " + username; // Afficher le nom d'utilisateur
    }

    private void UpdatePlayerDisplayName(string displayName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, result =>
        {
            Debug.Log("Display name updated successfully!");
        }, error =>
        {
            Debug.LogError("Failed to update display name: " + error.ErrorMessage);
        });
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in successfully!");

        string name = result.InfoResultPayload.PlayerProfile?.DisplayName ?? result.InfoResultPayload.PlayerProfile?.PlayerId;
        if (string.IsNullOrEmpty(name))
        {
            name = "Unknown";
        }
        Debug.Log(name);

        canvaLogin.SetActive(false);
        canvaProfil.SetActive(true);

        string id = result.PlayFabId;
        playerIdText.text = "Player ID: " + id;
        playerNameText.text = "Player Name: " + name;
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Login failed: " + error.ErrorMessage);
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError("Registration failed: " + error.ErrorMessage);
    }
}