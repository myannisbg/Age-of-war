using UnityEngine;
using UnityEngine.UI;

public class ScrollingButton : MonoBehaviour

{
    public GameObject Button;
    private bool isTurretVisible = false;
    
    // Fonction appelée lorsque le bouton est cliqué
    public void ToggleButton()
    {
        // Inversez l'état de la tourelle à chaque clic
        isTurretVisible = !isTurretVisible;

        // Activez ou désactivez la tourelle en fonction de l'état actuel
        Button.SetActive(isTurretVisible);
    }
}