using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{       
    private static float startDamage;
    private static float startHealth;
    private static float startMoney;
    public Bases baseObject;
    public float maxHealth = 100f;
    private float currentHealth;
    public float damageDealt = 1f; // dégats que l'unité inflige 
    public int expGain = 1; //experience gagné en tuant cette unité
    public float moneyGain = 1; //money gagné en tuant cette unité
    public float attackSpeed = 1f; //vitesse d'attaque 
    private float lastDamageTime; // Temps de la dernière application de dégâts
    public float attackRange = 1f; //Porté de l'attaque
    private float attackCooldown; // Temps d'attente entre chaque attaque
    public static bool isFirstUse = true;
    public static bool UnitsSpawned { get; private set; } = false;
    public Money moneyClass;
    public Xp xpClass;
    private string unitTag;
    public int type;
    public GameObject bullet;




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
            startMoney = unitPrefab.GetStartMoney();
            unitPrefab.SetCurrentDamage(startDamage);
            unitPrefab.SetCurrentHealth(startHealth);
            unitPrefab.SetCurrentMoney(startMoney);
            

        }
    }
}

    void Start()
    {
        unitTag = transform.root.tag;
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
    SetStartMoney(moneyGain);
    UnitsSpawned = true;

}

    public float GetStartDamage()
    {
        return startDamage;
    }
    public void SetCurrentDamage(float value)
    {
        damageDealt = value;
    }
    public void SetStartDamage(float value)
    {
        startDamage = value;
    }
    public float GetStartMoney()
    {
        return startMoney;
    }
    public void SetStartMoney(float value)
    {
        startMoney = value;
    }
    public void SetCurrentMoney(float value)
    {
        moneyGain = value;
    }
    public void SetStartHealth(float value)
    {
        startHealth=value;
    }
    public void SetCurrentHealth(float value)
    {
        maxHealth = value;
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
        // else
        // {
        //     Debug.Log("Unité ennemie non détectée. Aucun ajout d'argent.");
        // }
        
        
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
    // private void AttackClosestEnemy()
    // {
    //     Transform closestEnemy = FindClosestEnemy();
    //     if (closestEnemy != null)
    //     {
    //         float distanceToEnemy = Vector2.Distance(transform.position, closestEnemy.position);
    //         if (distanceToEnemy <= attackRange)
    //         {
    //             if (Time.time - lastDamageTime >= attackCooldown)
    //             {
    //                 if (type == 2)
    //                 {
    //                     Instantiate(bullet, transform.position, Quaternion.identity);
    //                 }
    //                 else
    //                 {
    //                     if (unitTag == "Ally") // Si l'unité est un allié
    //                     {
    //                         // Vérifier si l'ennemi n'est pas un allié
    //                         if (closestEnemy.CompareTag("Ennemy"))
    //                         {
    //                             DealDirectDamage(closestEnemy);
    //                             lastDamageTime = Time.time;
    //                         }
    //                     }
    //                     else // Si l'unité est un ennemi
    //                     {
    //                         // Vérifier si l'ennemi n'est pas un ennemi
    //                         if (closestEnemy.CompareTag("Ally"))
    //                         {
    //                             DealDirectDamage(closestEnemy);
    //                             lastDamageTime = Time.time;
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //     }
    // }

//     private void DealDirectDamage(Transform enemy)
// {
//     Unit enemyUnit = enemy.GetComponent<Unit>();
//     if (enemyUnit != null)
//     {
//         // Vérifier le type de l'unité
//         if (type == 2)
//         {
//             // L'unité tire un projectile
//             Instantiate(bullet, transform.position, Quaternion.identity);
//         }
//         else
//         {
//             // L'unité inflige des dégâts directs sans faire apparaître de projectile
//             enemyUnit.TakeDamage(damageDealt);
//         }
//     }

//     Bases enemyBase = enemy.GetComponent<Bases>();
//     if (enemyBase != null)
//     {
//         // Vérifier le type de l'unité
//         if (type == 2)
//         {
//             // L'unité tire un projectile
//             Instantiate(bullet, transform.position, Quaternion.identity);
//         }
//         else
//         {
//             // L'unité inflige des dégâts directs sans faire apparaître de projectile
//             enemyBase.DealDamage(this, damageDealt);
//         }
//     }
// }


//     private Transform FindClosestEnemy()
//     {
//         Transform closestEnemy = null;
//         float closestDistance = Mathf.Infinity;
//         GameObject[] enemies = GameObject.FindGameObjectsWithTag(unitTag == "Ally" ? "Ennemy" : "Ally"); // Sélection des ennemis en fonction du tag de l'unité

//         foreach (GameObject enemy in enemies)
//         {
//             float distance = Vector2.Distance(transform.position, enemy.transform.position);
//             if (distance < closestDistance)
//             {
//                 closestEnemy = enemy.transform;
//                 closestDistance = distance;
//             }
//         }
//         return closestEnemy;
//     }

    

    void Die()
    {
        
        Destroy(gameObject);
        if (gameObject.CompareTag("Ennemy"))
        {
            // Dans le script de vos unités, vous pouvez trouver l'EventSystem par son tag
            GameObject eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
            if (eventSystem != null)
            {
                Money moneyComponent = eventSystem.GetComponent<Money>();
                Xp xpComponent = eventSystem.GetComponent<Xp>();
                
                    // Vous avez maintenant accès au composant Money
                    moneyComponent.addGold(moneyGain);
                    xpComponent.addXp(expGain);
                    }

        }   
    }
}