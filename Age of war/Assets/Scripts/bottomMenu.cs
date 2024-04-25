using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bottomMenu : MonoBehaviour

{
    public GameObject Button;
    
    // Fonction appelée lorsque le bouton est cliqué
    public void ToggleButton()
    {
        // Activez ou désactivez la tourelle en fonction de l'état actuel
        Button.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ThaWarudo(){
        Time.timeScale = 0;
    }

    public void GoldExperience(){
        Time.timeScale = 1;
    }
    public void MadeInHaven(){
        Time.timeScale = 2;
    }
    public void kingCrimson(){
        SceneManager.LoadScene("ça marche");
    }
}
