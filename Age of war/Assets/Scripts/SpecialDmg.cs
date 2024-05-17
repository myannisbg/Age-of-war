using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage = 50; // Quantité de dégâts infligés

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Assurez-vous que l'ennemi a le tag "Enemy"
        {
            // Appliquer des dégâts à l'ennemi, supposant qu'ils ont un script de santé
            Healthbar enemyHealth = other.GetComponent<Healthbar>();
            if (enemyHealth != null)
            {
                //enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Détruire le projectile après la collision
        }
    }
}
