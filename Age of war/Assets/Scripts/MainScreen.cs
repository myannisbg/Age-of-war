using UnityEngine;

public class MainScreen : MonoBehaviour
{
    public GameObject settingsScreen;
    
    void Start()
    {
        // Au démarrage de la scène, cache l'écran des paramètres
        gameObject.SetActive(false);
    }
    public void ShowSettingsScreen()
    {
        settingsScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
