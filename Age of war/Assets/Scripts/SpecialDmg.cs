using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage = 50; // Quantit� de d�g�ts inflig�s

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Assurez-vous que l'ennemi a le tag "Enemy"
        {
            // Appliquer des d�g�ts � l'ennemi, supposant qu'ils ont un script de sant�
            Healthbar enemyHealth = other.GetComponent<Healthbar>();
            if (enemyHealth != null)
            {
                //enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // D�truire le projectile apr�s la collision
        }
    }
}
