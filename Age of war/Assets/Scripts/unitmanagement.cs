using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public Bases baseObject;
    public float maxHealth = 100f;
    private float currentHealth;
    public float damageDealt = 1f; // dégats que l'unité inflige 
    public float expGain = 1f; //experience gagné en tuant cette unité
    public float moneyGain = 1f; //money gagné en tuant cette unité
    public float attackSpeed = 1f; //vitesse d'attaque 
    private float lastDamageTime; // Temps de la dernière application de dégâts


    public Healthbar healthBar; // Utilisation du composant Healthbar au lieu de PlayerHealth

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth); // Convertir maxHealth en int


    }

 

    public void TakeDamage(float damageDealt)
    {
        currentHealth -= damageDealt;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

void OnTriggerStay2D(Collider2D other)
{
    if (other.CompareTag("Ennemy"))
    {
        float timeSinceLastDamage = Time.time - lastDamageTime;
        float damageInterval = 1f / attackSpeed; // Calcul de l'intervalle entre chaque application de dégâts en fonction de la vitesse d'attaque

        if (timeSinceLastDamage > damageInterval)
        {
            DealDamage(other);
            lastDamageTime = Time.time;
        }
    }

}

public void DealDamage(Collider2D collision)
{
    if (collision.gameObject.tag == "Ennemy")
    {
        TakeDamage(damageDealt);
    }
    else if (collision.gameObject.tag == "Chateaux")
    {
        Bases baseObject = collision.gameObject.GetComponent<Bases>();
        if (baseObject != null)
        {
            baseObject.currentHealthBase -= damageDealt;
            baseObject.healthBarBase.SetHealth(baseObject.currentHealthBase);
        }
    }
}

    


    void Die()
    {
        // Mettre ici le code pour gérer la mort du joueur
        Destroy(gameObject);
    }
}

