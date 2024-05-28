using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float specialAttackDamage = 1000f; // D�g�ts inflig�s par l'attaque sp�ciale
    public Unit unitType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // V�rifier si le projectile touche un ennemi
        if (collision.CompareTag("Ennemy"))
        {
            Unit enemyUnit = collision.GetComponent<Unit>();
            if (enemyUnit != null)
            {
                // Infliger des d�g�ts � l'ennemi
                enemyUnit.TakeDamage(specialAttackDamage,unitType.type);
            }
        }

        // D�truire le projectile lorsqu'il touche n'importe quel objet
        Destroy(gameObject);
    }
}
