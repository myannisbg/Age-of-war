using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour
{
    public GameObject tooltipPanel; // Panneau de l'infobulle
    public Vector3 offset = new Vector3(0, -200, 0); // Décalage par rapport à la souris
    private float fadeInDuration = 1f; // Durée du fade-in
    private CanvasGroup canvasGroup; // Le CanvasGroup pour contrôler l'opacité

    private void Awake()
    {
        // Obtenir le CanvasGroup associé au tooltipPanel
        canvasGroup = tooltipPanel.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            // Si le CanvasGroup n'existe pas, ajoutez-le
            canvasGroup = tooltipPanel.AddComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        // Masquer l'infobulle au départ
        HideTooltip();
    }

    public void ShowTooltip()
    {
        // Rendre le panneau actif mais transparent
        tooltipPanel.SetActive(true);
        StartCoroutine(FadeInTooltip()); // Commencer le fade-in
        MoveTooltip(); // Positionner l'infobulle
    }

    public void HideTooltip()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0; // Masquer l'infobulle immédiatement
        }
        tooltipPanel.SetActive(false);
    }

    private IEnumerator FadeInTooltip()
    {
        float elapsed = 0f;

        // Animation du fade-in sur la durée spécifiée
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime; // Accumuler le temps écoulé
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeInDuration); // Interpolation de l'opacité
            yield return null; // Attendre le prochain frame
        }

        canvasGroup.alpha = 1f; // Assurez-vous que l'opacité est à 1 à la fin
    }

    private void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            MoveTooltip(); // Déplacer l'infobulle avec la souris
        }
    }

    private void MoveTooltip()
    {
        // Positionner l'infobulle par rapport à la souris avec un décalage
        Vector3 mousePos = Input.mousePosition + offset;
        tooltipPanel.transform.position = mousePos;
    }
}
