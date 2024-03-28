using UnityEngine;

public class SettingsScreen : MonoBehaviour
{
    public GameObject mainScreen;

    public void ShowMainScreen()
    {
        mainScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
