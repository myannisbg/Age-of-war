using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitHealthManager : MonoBehaviour
{
    public Slider healthSlider;

    private float currentHealth;

    void Start()
    {
        // Démarrer une coroutine pour surveiller la présence de la barre de PV
        StartCoroutine(WaitForHealthSlider());
    }

    IEnumerator WaitForHealthSlider()
    {
        // Attendre que la barre de PV soit définie dans la scène
        while (healthSlider == null)
        {
            yield return null;
        }

        // Une fois que la barre de PV est définie, initialiser les valeurs de santé
        currentHealth = healthSlider.maxValue;
    }

    public void SetMaxHealth(float maxHealth)
    {
        // Vérifier si healthSlider est défini
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
            currentHealth = maxHealth;
        }

    }

    public void TakeDamage(float damage)
    {
        // Vérifier si healthSlider est défini
        if (healthSlider != null)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        
    }

    void Die()
    {

        Destroy(gameObject);
    }
}
