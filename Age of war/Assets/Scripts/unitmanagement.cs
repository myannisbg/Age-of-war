using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{       
    private static float startDamage;
    private static float startHealth;
    private static float startMoney;
    private static float startAttackRange;
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
    public GameObject bullet;
    public Vector3 offset = new Vector3(0.2f, 0.0f, 0.0f); 
    public enum UnitType
    {
        Infantry,
        Archer,
        Tank,
        AntiArmor,
        None
    }
    public UnitType type; 
    public bool isWalking = false;
    public bool isAttacking = false;
    public Animator animator;
    

    




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
            startAttackRange=unitPrefab.GetStartRange();
            unitPrefab.SetCurrentDamage(startDamage);
            unitPrefab.SetCurrentHealth(startHealth);
            unitPrefab.SetCurrentMoney(startMoney);
            unitPrefab.SetCurrentRange(startAttackRange);
            

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
    SetStartRange(attackRange);
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
    public float GetStartRange(){
        return attackRange;
    }
    public void SetCurrentRange(float value)
    {
        attackRange = value;
    }
    public void SetStartRange(float value)
    {
        startAttackRange = value;
    }


    public void TakeDamage(float damageDealt, UnitType attackerType)
    {
        currentHealth -= damageDealt * GetDamageMultiplier(attackerType, type);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    

private float GetDamageMultiplier(UnitType attackerType, UnitType targetType)
    {
        switch (attackerType)
        {
            case UnitType.Infantry:
                return targetType == UnitType.Archer ? 1.5f : 1.0f;
            case UnitType.Archer:
                return targetType == UnitType.AntiArmor ? 1.5f : 1.0f;
            case UnitType.Tank:
                return targetType == UnitType.Infantry ? 1.5f : 1.0f;
            case UnitType.AntiArmor:
                return targetType == UnitType.Tank ? 1.5f : 1.0f;
            case UnitType.None:
            default:
                return 1.0f;
        }
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
    bool foundSomethingToHit = false;
    string currentTag = transform.root.tag;


    // Cercle de détection pour les unités ennemies
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, attackRange);
    // Cercle de détection pour les unités alliées
    Collider2D[] hitAllies = Physics2D.OverlapCircleAll(transform.position - offset, attackRange);
    // Cercle de détection pour les bases ennemies
    Collider2D[] hitEnemyBases = Physics2D.OverlapCircleAll(transform.position + offset, attackRange);
    // Cercle de détection pour les bases alliées
    Collider2D[] hitBases = Physics2D.OverlapCircleAll(transform.position - offset, attackRange);

    // Vérifier les collisions avec les unités ennemies
    foreach (var hit in hitEnemies)
    {
        if (hit != null && hit.CompareTag("Ennemy") && currentTag == "Ally")
        {
            foundSomethingToHit = true;
            // Vérifier si suffisamment de temps s'est écoulé depuis le dernier coup
            if (Time.time - lastDamageTime >= attackCooldown)
            {
                Unit unit = hit.GetComponent<Unit>();
                if (unit != null)
                {
                    // Effectuer une attaque sur l'ennemi
                    unit.TakeDamage(damageDealt,type);
                    lastDamageTime = Time.time;
                }
            }
        }
    }

    // Vérifier les collisions avec les unités alliées
    foreach (var hit in hitAllies)
    {
        if (hit != null && hit.CompareTag("Ally") && currentTag == "Ennemy")
        {
            foundSomethingToHit = true;
            // Vérifier si suffisamment de temps s'est écoulé depuis le dernier coup
            if (Time.time - lastDamageTime >= attackCooldown)
            {
                Unit unit = hit.GetComponent<Unit>();
                if (unit != null)
                {
                    // Effectuer une attaque sur l'allié
                    unit.TakeDamage(damageDealt,type);
                    lastDamageTime = Time.time;
                }
            }
        }
    }

    // Vérifier les collisions avec les bases ennemies
    foreach (var hit in hitEnemyBases)
    {
        if (hit != null && hit.CompareTag("BaseEnnemy") && currentTag == "Ally")
        {
            foundSomethingToHit = true;
            Bases baseObject = hit.GetComponent<Bases>();
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

    // Vérifier les collisions avec les bases alliées
    foreach (var hit in hitBases)
    {
        if (hit != null && hit.CompareTag("Base") && currentTag == "Ennemy")
        {
            foundSomethingToHit = true;
            Bases baseObject = hit.GetComponent<Bases>();
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
    if (foundSomethingToHit == true) {
        animator.SetBool("isAttacking", true);
        animator.SetBool("isWalking", false);
        } else {animator.SetBool("isAttacking", false);}
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
    string currentTag = transform.root.tag;
   

    // Cercle de détection pour les unités ennemies
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, attackRange);
    // Cercle de détection pour les unités alliées
    Collider2D[] hitAllies = Physics2D.OverlapCircleAll(transform.position - offset, attackRange);
    // Cercle de détection pour les bases ennemies
    Collider2D[] hitEnemyBases = Physics2D.OverlapCircleAll(transform.position + offset, attackRange);
    // Cercle de détection pour les bases alliées
    Collider2D[] hitBases = Physics2D.OverlapCircleAll(transform.position - offset, attackRange);

    // Vérifier les collisions avec les unités ennemies
    foreach (var hit in hitEnemies)
    {
        if (hit != null && hit.CompareTag("Ennemy") && currentTag == "Ally")
        {
            // Vérifier si suffisamment de temps s'est écoulé depuis le dernier coup
            if (Time.time - lastDamageTime >= attackCooldown)
            {
                Unit unit = hit.GetComponent<Unit>();
                if (unit != null)
                {
                    // Effectuer une attaque sur l'ennemi
                    unit.TakeDamage(damageDealt,type);
                    isAttacking=true;
                    lastDamageTime = Time.time;
                }
            }
        isAttacking=false;
        }
    }

    // Vérifier les collisions avec les unités alliées
    foreach (var hit in hitAllies)
    {
        if (hit != null && hit.CompareTag("Ally") && currentTag == "Ennemy")
        {
            // Vérifier si suffisamment de temps s'est écoulé depuis le dernier coup
            if (Time.time - lastDamageTime >= attackCooldown)
            {
                Unit unit = hit.GetComponent<Unit>();
                if (unit != null)
                {
                    // Effectuer une attaque sur l'allié
                    unit.TakeDamage(damageDealt,type);
                    isAttacking = true;
                    lastDamageTime = Time.time;
                }
            }
            isAttacking=false;
        }
    }

    // Vérifier les collisions avec les bases ennemies
    foreach (var hit in hitEnemyBases)
    {
        if (hit != null && hit.CompareTag("BaseEnnemy") && currentTag == "Ally")
        {
            Bases baseObject = hit.GetComponent<Bases>();
            if (baseObject != null)
            {
                // Vérifier si suffisamment de temps s'est écoulé depuis la dernière attaque
                if (Time.time - baseObject.lastAttackTime >= attackCooldown)
                {
                    // Effectuer une attaque sur la base
                    baseObject.DealDamage(this, damageDealt);
                    isAttacking=true;
                    baseObject.lastAttackTime = Time.time; // Mettre à jour le temps de la dernière attaque
                }
            }
            isAttacking=false;
        }
    }

    // Vérifier les collisions avec les bases alliées
    foreach (var hit in hitBases)
    {
        if (hit != null && hit.CompareTag("Base") && currentTag == "Ennemy")
        {
            Bases baseObject = hit.GetComponent<Bases>();
            if (baseObject != null)
            {
                // Vérifier si suffisamment de temps s'est écoulé depuis la dernière attaque
                if (Time.time - baseObject.lastAttackTime >= attackCooldown)
                {
                    // Effectuer une attaque sur la base
                    baseObject.DealDamage(this, damageDealt);
                    isAttacking=true;
                    baseObject.lastAttackTime = Time.time; // Mettre à jour le temps de la dernière attaque
                }
            }
            isAttacking=false;
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