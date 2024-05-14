using UnityEngine;
using System.Collections;

public class SpriteTransition : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Le SpriteRenderer pour le changement d'image
    public Sprite[] sprites; // Tableau des sprites entre lesquels vous souhaitez basculer
    public float fadeDuration = 2f; // Durée du fade
    public float waitTime = 5f; // Temps entre les changements
    private int currentIndex = 0; // Index actuel du sprite

    private void Start()
    {
        if (spriteRenderer == null || sprites.Length == 0)
        {
            Debug.LogError("Références incorrectes pour le SpriteRenderer ou les sprites.");
            return;
        }

        // Définir le sprite initial
        spriteRenderer.sprite = sprites[currentIndex];
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Assurez-vous que l'opacité est à 1

        StartCoroutine(ChangeSpriteWithFade());
    }

    private IEnumerator ChangeSpriteWithFade()
    {
        while (true) // Répéter indéfiniment
        {
            yield return new WaitForSeconds(waitTime); // Temps entre les changements

            // Fade-out
            yield return StartCoroutine(FadeOut());

            // Changer le sprite
            currentIndex = (currentIndex + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[currentIndex];

            // Fade-in
            yield return StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration); // Interpolation du fade-out
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha); // Changer l'opacité
            yield return null;
        }

        spriteRenderer.color = new Color(1f, 1f, 1f, 0f); // Assurez-vous que l'opacité est à 0
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration); // Interpolation du fade-in
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha); // Changer l'opacité
            yield return null;
        }

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Assurez-vous que l'opacité est à 1
    }
}
