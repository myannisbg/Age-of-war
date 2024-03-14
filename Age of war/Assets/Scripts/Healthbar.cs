using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthText;

    private float maxHealth = 100f;

    void Start()
    {
        SetMaxHealth((int)maxHealth);
    }

    void Update()
    {
        // Mettre à jour le texte de la barre de santé
        healthText.text = string.Format("{0:0}/{1:0}", slider.value, slider.maxValue);
    }

    // Méthode pour définir les valeurs maximales de santé
    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        slider.maxValue = maxHealth;
        slider.value = maxHealth; // Assurez-vous que la valeur actuelle ne dépasse pas la valeur maximale
    }

    // Méthode pour définir la santé actuelle
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
