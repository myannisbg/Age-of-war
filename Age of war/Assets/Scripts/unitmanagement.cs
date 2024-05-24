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
    public Vector3 offset = new Vector3(0.2f, 0.0f, 0.0f); 




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

void FixedUpdate(){
    detectObject();
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
    



//     void OnTriggerStay2D(Collider2D other)
// {
//     string currentTag = transform.root.tag; // Obtenir le tag du joueur

//     if (other.CompareTag("Ennemy") && currentTag == "Ally" || other.CompareTag("Ally") && currentTag == "Ennemy")
//     {
//         // Vérifier si suffisamment de temps s'est écoulé depuis le dernier coup
//         if (Time.time - lastDamageTime >= attackCooldown)
//         {
//             Unit unit = other.GetComponent<Unit>();
//             if (unit != null)
//             {
//                 // Effectuer une attaque sur l'ennemi
//                 unit.TakeDamage(damageDealt);
//                 lastDamageTime = Time.time;
//             }
//         }
//     }
//     else if ( other.CompareTag("BaseEnnemy") && currentTag == "Ally" || (other.CompareTag("Base") && currentTag == "Ennemy"))
//     {
//         Bases baseObject = other.GetComponent<Bases>();
//         if (baseObject != null)
//         {
//             // Vérifier si suffisamment de temps s'est écoulé depuis la dernière attaque
//             if (Time.time - baseObject.lastAttackTime >= attackCooldown)
//             {
//                 // Effectuer une attaque sur la base
//                 baseObject.DealDamage(this, damageDealt);
//                 baseObject.lastAttackTime = Time.time; // Mettre à jour le temps de la dernière attaque
//             }
//         }
//     }
// }
void detectObject()
{
    string currentTag = transform.root.tag;
    Vector3 offset = new Vector3(0.5f, 0, 0); // Vous pouvez ajuster ce décalage selon vos besoins

    // Raycast vers l'avant pour détecter une base ennemie
    RaycastHit2D hitEnemyBase = Physics2D.Raycast(transform.position + offset, Vector2.right, 1f);
    Debug.DrawRay(transform.position + offset, Vector2.right * 1f, Color.red);
    // Raycast vers l'avant pour détecter une base alliée
    RaycastHit2D hitBase = Physics2D.Raycast(transform.position - offset, Vector2.left, 1f);
    Debug.DrawRay(transform.position - offset, Vector2.left * 1f, Color.blue);
    // Raycast vers l'avant pour détecter une unité ennemie
    RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position + offset, Vector2.right, 2f);
    Debug.DrawRay(transform.position + offset, Vector2.right * 2f, Color.green);
    // Raycast vers l'avant pour détecter une unité alliée
    RaycastHit2D hitAlly = Physics2D.Raycast(transform.position - offset, Vector2.left, 2f);
    Debug.DrawRay(transform.position - offset, Vector2.left * 2f, Color.yellow);

    // Vérifier les collisions avec les unités ennemies
    if (hitEnemy.collider != null && hitEnemy.collider.CompareTag("Ennemy") && currentTag == "Ally")
    {
        Debug.Log("Hit an enemy unit");
        // Vérifier si suffisamment de temps s'est écoulé depuis le dernier coup
        if (Time.time - lastDamageTime >= attackCooldown)
        {
            Unit unit = hitEnemy.collider.GetComponent<Unit>();
            if (unit != null)
            {
                // Effectuer une attaque sur l'ennemi
                unit.TakeDamage(damageDealt);
                lastDamageTime = Time.time;
            }
        }
    }

    if (hitAlly.collider != null && hitAlly.collider.CompareTag("Ally") && currentTag == "Ennemy")
    {
        Debug.Log("Hit an ally unit");
        // Vérifier si suffisamment de temps s'est écoulé depuis le dernier coup
        if (Time.time - lastDamageTime >= attackCooldown)
        {
            Unit unit = hitAlly.collider.GetComponent<Unit>();
            if (unit != null)
            {
                // Effectuer une attaque sur l'allié
                unit.TakeDamage(damageDealt);
                lastDamageTime = Time.time;
            }
        }
    }

    // Vérifier les collisions avec les bases ennemies
    if (hitEnemyBase.collider != null && hitEnemyBase.collider.CompareTag("BaseEnnemy") && currentTag == "Ally")
    {
        Debug.Log("Hit an enemy base");
        Bases baseObject = hitEnemyBase.collider.GetComponent<Bases>();
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

    // Vérifier les collisions avec les bases alliées
    if (hitBase.collider != null && hitBase.collider.CompareTag("Base") && currentTag == "Ennemy")
    {
        Debug.Log("Hit an ally base");
        Bases baseObject = hitBase.collider.GetComponent<Bases>();
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



    // public void DealDamage(Collider2D collision)
    // {
    //     string currentTag = transform.root.tag; // Obtenir le tag du joueur
        
        


    //     if (collision.gameObject.CompareTag("Ennemy") && currentTag == "Ally"|| collision.gameObject.CompareTag("Ally") && currentTag == "Ennemy" )
    //     {
    //         TakeDamage(damageDealt);
    //     }
    //     else if (collision.gameObject.CompareTag("Base") && currentTag == "Ennemy" || collision.gameObject.CompareTag("BaseEnnemy") && currentTag == "Ally")
    //     {
    //         Bases baseObject = collision.gameObject.GetComponent<Bases>();
    //         if (baseObject != null)
    //         {
    //             baseObject.currentHealthBase -= damageDealt;
    //             baseObject.healthBarBase.SetHealth(baseObject.currentHealthBase);
    //         }
    //     }
    // }

public void DealDamage()
{
    // Obtenir le tag du joueur
    string currentTag = transform.root.tag; 
    Vector3 offset = new Vector3(0.5f, 0, 0); // Assurez-vous que ce décalage est défini quelque part

    // Raycast vers l'avant pour détecter une base ennemie
    RaycastHit2D hitEnemyBase = Physics2D.Raycast(transform.position + offset, Vector2.right, 1f);
    // Raycast vers l'avant pour détecter une base alliée
    RaycastHit2D hitBase = Physics2D.Raycast(transform.position - offset, Vector2.left, 1f);
    // Raycast vers l'avant pour détecter une unité ennemie
    RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position + offset, Vector2.right, 2f);
    // Raycast vers l'avant pour détecter une unité alliée
    RaycastHit2D hitAlly = Physics2D.Raycast(transform.position - offset, Vector2.left, 2f);

    // Vérifier les collisions avec les unités ennemies
    if ((hitEnemy.collider != null && hitEnemy.collider.CompareTag("Ennemy") && currentTag == "Ally") ||
        (hitAlly.collider != null && hitAlly.collider.CompareTag("Ally") && currentTag == "Ennemy"))
    {
        print("la");

        // Vérifier si suffisamment de temps s'est écoulé depuis le dernier coup
        if (Time.time - lastDamageTime >= attackCooldown)
        {
            Unit unit = hitEnemy.collider != null ? hitEnemy.collider.GetComponent<Unit>() : hitAlly.collider.GetComponent<Unit>();
            if (unit != null)
            {
                // Effectuer une attaque sur l'unité
                unit.TakeDamage(damageDealt);
                lastDamageTime = Time.time;
            }
        }
    }
    // Vérifier les collisions avec les bases
    else if ((hitEnemyBase.collider != null && hitEnemyBase.collider.CompareTag("BaseEnnemy") && currentTag == "Ally") ||
             (hitBase.collider != null && hitBase.collider.CompareTag("Base") && currentTag == "Ennemy"))
    {
        print("oui");

        Bases baseObject = hitEnemyBase.collider != null ? hitEnemyBase.collider.GetComponent<Bases>() : hitBase.collider.GetComponent<Bases>();
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