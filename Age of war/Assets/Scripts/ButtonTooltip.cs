using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Tooltip tooltip;
    private float delay = 2f; // Délai en secondes
    private Coroutine showTooltipCoroutine; // La coroutine qui affiche l'infobulle

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltip != null)
        {
            showTooltipCoroutine = StartCoroutine(ShowTooltipAfterDelay());
            //tooltip.ShowTooltip(); // Afficher l'infobulle avec le message
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip != null  && showTooltipCoroutine != null)
        {
            StopCoroutine(showTooltipCoroutine); // Arrêter la coroutine
            showTooltipCoroutine = null; // Réinitialiser la référence
            tooltip.HideTooltip(); // Masquer l'infobulle
        }
    }
    // Coroutine qui affiche l'infobulle après un délai
    private IEnumerator ShowTooltipAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Attendre le délai

        if (tooltip != null)
        {
            tooltip.ShowTooltip(); // Afficher l'infobulle
        }
    }
}
