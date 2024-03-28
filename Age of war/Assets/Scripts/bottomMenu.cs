using UnityEngine;
using UnityEngine.UI;

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
}
