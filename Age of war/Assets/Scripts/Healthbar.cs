using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    private float maxHealth = 100f;

    public float GetHealth()
    {
        return slider.value;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // Calculer le pourcentage de santé actuelle
        float healthPercentage = currentHealth / maxHealth;

        // Mettre à jour la valeur du Slider pour refléter la santé actuelle
        slider.value = healthPercentage;
    }

    void Start()
    {
        // Vérifie si le Slider est null
        if (slider == null)
        {
            // Instancie un Slider et l'attache à cet objet
            slider = gameObject.AddComponent<Slider>();
            slider.minValue = 0;
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }
        // Définit la santé maximale
        SetMaxHealth((int)maxHealth);
    }
    
    public void SetMaxHealth(int health)
    {
        // Assigne la valeur maximale au Slider
        slider.maxValue = health;
        slider.value = health;
    }
    
}
