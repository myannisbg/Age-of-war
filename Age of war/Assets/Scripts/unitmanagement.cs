using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{       
    private static float startDamage;
    private static float startHealth;
    public Bases baseObject;
    public float maxHealth = 100f;
    private float currentHealth;
    public float damageDealt = 1f; // dégats que l'unité inflige 
    public float expGain = 1f; //experience gagné en tuant cette unité
    public float moneyGain = 1f; //money gagné en tuant cette unité
    public float attackSpeed = 1f; //vitesse d'attaque 
    private float lastDamageTime; // Temps de la dernière application de dégâts
    private float attackCooldown; // Temps d'attente entre chaque attaque
    public static bool isFirstUse = true;
    public static bool UnitsSpawned { get; private set; } = false;


    public Healthbar healthBar; // Utilisation du composant Healthbar au lieu de PlayerHealth

// Méthode statique pour réinitialiser les valeurs initiales des unités
public static void ResetInitialValues(GameObject prefab)
{
    // Assurez-vous que le prefab est valide
    if (prefab != null)
    {
        Unit unitPrefab = prefab.GetComponent<Unit>();
        if (unitPrefab != null)
        {
            // Affectez les valeurs initiales des dégâts et de la santé aux variables statiques
            startDamage = unitPrefab.GetStartDamage();
            startHealth = unitPrefab.GetStartHealth();
            unitPrefab.SetCurrentDamage(startDamage);
            unitPrefab.SetCurrentHealth(startHealth);
            

        }
    }
}

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth); // Convertir maxHealth en int
        attackCooldown = 1f / attackSpeed; // Calcul de l'interval de temps entre chaque attaque
        
        if (isFirstUse)
        {
            firstUse();
            isFirstUse = false;
        }



}

void firstUse()
{

    SetStartDamage(damageDealt);
    SetStartHealth(maxHealth);
    UnitsSpawned = true;

}


    public float GetCurrentDamage()
    {
        return damageDealt;
    }

    public void SetCurrentDamage(float value)
    {
        damageDealt = value;
    }
    public void SetStartDamage(float value)
    {
        startDamage = value;
    }
    public void SetStartHealth(float value)
    {
        startHealth=value;
    }

    public float GetCurrentHealth()
    {
        return maxHealth;
    }

    public void SetCurrentHealth(float value)
    {
        maxHealth = value;
    }

    public float GetStartDamage()
    {
        return startDamage;
    }

    public float GetStartHealth()
    {
        return startHealth;
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
    string currentTag = transform.root.tag; // Obtenir le tag du joueur

    if (other.CompareTag("Ennemy") && currentTag == "Ally" || other.CompareTag("Ally") && currentTag == "Ennemy")
    {
        // Vérifier si suffisamment de temps s'est écoulé depuis le dernier coup
        if (Time.time - lastDamageTime >= attackCooldown)
        {
            Unit unit = other.GetComponent<Unit>();
            if (unit != null)
            {
                // Effectuer une attaque sur l'ennemi
                unit.TakeDamage(damageDealt);
                lastDamageTime = Time.time;
            }
        }
    }
    else if ( other.CompareTag("BaseEnnemy") && currentTag == "Ally" || (other.CompareTag("Base") && currentTag == "Ennemy"))
    {
        Bases baseObject = other.GetComponent<Bases>();
        if (baseObject != null)
        {
            // Vérifier si suffisamment de temps s'est écoulé depuis la dernière attaque
            if (Time.time - baseObject.lastAttackTime >= attackCooldown)
            {
                // Effectuer une attaque sur la base
                baseObject.DealDamage(this, damageDealt);
                baseObject.lastAttackTime = Time.time; // Mettre à jour le temps de la dernière attaque
            }
        }
    }
}

    public void DealDamage(Collider2D collision)
    {
        string currentTag = transform.root.tag; // Obtenir le tag du joueur

        if (collision.gameObject.CompareTag("Ennemy") && currentTag == "Ally"|| collision.gameObject.CompareTag("Ally") && currentTag == "Ennemy" )
        {
            TakeDamage(damageDealt);
        }
        else if (collision.gameObject.CompareTag("Base") && currentTag == "Ennemy" || collision.gameObject.CompareTag("BaseEnnemy") && currentTag == "Ally")
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
	