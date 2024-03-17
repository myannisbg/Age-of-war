using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public float damageInterval = 1f; // Intervalles de dégâts en secondes

    public PlayerHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // Démarrer la coroutine pour infliger des dégâts à intervalles réguliers
        StartCoroutine(InflictDamageOverTime());
    }

    IEnumerator InflictDamageOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);

            // Appliquer des dégâts au joueur
            TakeDamage(10f); // Modifier le montant de dégâts selon vos besoins
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Mettre ici le code pour gérer la mort du joueur
        Destroy(gameObject);
    }
}
