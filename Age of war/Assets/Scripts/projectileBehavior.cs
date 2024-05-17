using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float specialAttackDamage = 1000f; // Dégâts infligés par l'attaque spéciale

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifier si le projectile touche un ennemi
        if (collision.CompareTag("Ennemy"))
        {
            Unit enemyUnit = collision.GetComponent<Unit>();
            if (enemyUnit != null)
            {
                // Infliger des dégâts à l'ennemi
                enemyUnit.TakeDamage(specialAttackDamage);
            }
        }

        // Détruire le projectile lorsqu'il touche n'importe quel objet
        Destroy(gameObject);
    }
}
