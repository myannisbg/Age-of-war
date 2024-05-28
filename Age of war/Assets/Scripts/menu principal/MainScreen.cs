using UnityEngine;

public class MainScreen : MonoBehaviour
{
    public GameObject soloScreen;
    public GameObject multiScreen;
    public GameObject mainScreen;
    public GameObject LoginScreen;
    public GameObject RegisterScreen;
    
    void Start()
    {
        // Au démarrage de la scène, cache l'écran des paramètres
        ShowMainScreen();
    }

    public void ShowMainScreen(){
        soloScreen.SetActive(false);
        multiScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void ShowSoloScreen()
    {
        soloScreen.SetActive(true);
        mainScreen.SetActive(false);
    }

    public void ShowMultiScreen()
    {
        multiScreen.SetActive(true);
        mainScreen.SetActive(false);
    }

    
    public void ShowRegisterScreen()
    {
        LoginScreen.SetActive(false);
        RegisterScreen.SetActive(true);
    }

    public void ShowLoginScreen()
    {
        RegisterScreen.SetActive(false);
        LoginScreen.SetActive(true);
    }

    
}