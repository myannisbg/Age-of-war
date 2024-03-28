using UnityEngine;
using System.Collections.Generic;

public class Bases : MonoBehaviour
{
    public float currentHealthBase;
    public float maxHealthBase = 100f;
    public Healthbar healthBarBase;
    public float damageInterval = 1f; // Intervalle entre chaque tic de dégâts
    private float lastDamageTime; // Temps du dernier tic de dégâts
    private Dictionary<Unit, float> damageAccumulators = new Dictionary<Unit, float>(); // Accumulateurs de dégâts infligés par les ennemis

    void Start()
    {
        currentHealthBase = maxHealthBase;
        healthBarBase.SetMaxHealth((int)maxHealthBase);
    }

    void Update()
    {
        // Vérifier si suffisamment de temps s'est écoulé depuis le dernier tic de dégâts
        if (Time.time - lastDamageTime >= damageInterval)
        {
            // Appliquer les dégâts infligés par les ennemis
            ApplyDamage();
            lastDamageTime = Time.time; // Mettre à jour le temps du dernier tic de dégâts
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
        {
            Unit unit = other.GetComponent<Unit>();
            if (unit != null)
            {
                // Ajouter les dégâts infligés par l'ennemi à l'accumulateur de dégâts correspondant
                if (!damageAccumulators.ContainsKey(unit))
                {
                    damageAccumulators.Add(unit, 0f);
                }
                damageAccumulators[unit] += unit.damageDealt;
            }
        }
    }

    void ApplyDamage()
    {
        foreach (var pair in damageAccumulators)
        {
            currentHealthBase -= pair.Value;
        }
        damageAccumulators.Clear(); // Effacer les accumulateurs après avoir appliqué les dégâts
        healthBarBase.SetHealth(currentHealthBase);
    }
}
