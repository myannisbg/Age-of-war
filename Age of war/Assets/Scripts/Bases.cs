using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bases : MonoBehaviour
{
    public float currentHealthBase;
    public float maxHealthBase = 100f;
    public Healthbar healthBarBase;
    public float damageInterval = 1f; // Intervalle entre chaque tic de dégâts
    public float lastAttackTime; // Temps de la dernière attaque
    private Dictionary<Unit, float> damageAccumulators = new Dictionary<Unit, float>(); // Accumulateurs de dégâts infligés par les ennemis
    private int ageCount = 0; // Compteur d'âge de la base
    private const int maxAgeCount = 5; // Nombre maximum d'augmentations d'âge autorisées

    void Start()
    {
        currentHealthBase = maxHealthBase;
        healthBarBase.SetMaxHealth((int)maxHealthBase);
        StartCoroutine(DealDamageOverTime());
    }
    void Update()
{
    // Vérifie si la touche J est enfoncée
    if (Input.GetKeyDown(KeyCode.J))
    {
        // Appelle la fonction AgeUp
        AgeUp();
    }
}


    public void DealDamage(Unit unit, float damageDealt)
    {
        // Ajouter les dégâts infligés à l'accumulateur de dégâts
        if (!damageAccumulators.ContainsKey(unit))
        {
            damageAccumulators.Add(unit, 0f);
        }
        damageAccumulators[unit] += damageDealt;
    }

    IEnumerator DealDamageOverTime()
    {
        while (true)
        {
            // Attendre l'intervalle de dégâts
            yield return new WaitForSeconds(damageInterval);

            // Appliquer les dégâts accumulés à la base
            foreach (var pair in damageAccumulators)
            {
                currentHealthBase -= pair.Value;
            }

            // Effacer les accumulateurs après avoir appliqué les dégâts
            damageAccumulators.Clear();

            // Mettre à jour la barre de santé de la base
            healthBarBase.SetHealth(currentHealthBase);

            // Vérifier si la base est toujours en vie
            if (currentHealthBase <= 0)
            {
                // La base est détruite, terminer la coroutine
                yield break;
            }
        }
    }

public void AgeUp()
{
    // Vérifie si l'âge de la base est inférieur au maximum autorisé
    if (ageCount < maxAgeCount)
    {
        // Calcule le pourcentage de points de vie actuels
        float currentHealthPercentage = currentHealthBase / maxHealthBase;

        // Augmente les points de vie maximum de la base
        maxHealthBase *= 1.5f; // Augmente les points de vie maximum de 50%

        // Applique le même pourcentage de points de vie actuels au nouveau maximum
        currentHealthBase = maxHealthBase * currentHealthPercentage;

        healthBarBase.SetMaxHealth((int)maxHealthBase); // Met à jour la barre de santé avec la nouvelle valeur maximale

        // Incrémente le compteur d'âge
        ageCount++;

        Debug.Log("Base has aged up. New max health: " + maxHealthBase);
    }
    else
    {
        Debug.Log("Base has reached maximum age.");
    }
}
}
