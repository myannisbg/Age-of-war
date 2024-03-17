using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    public Healthbar healthbar;

    private float currentHealth;
    private float maxHealth;

    void FixedUpdate()
    {
        // Définir la direction dans laquelle le joueur doit se déplacer (horizontalement)
        float horizontalMovement = 0.5f; // Modification de la direction du mouvement

        // Déplacer le joueur
        MovePlayers(horizontalMovement);

        // Vérifier si le joueur est en collision avec un obstacle
        CheckForObstacle();

        // Exemple : Faites perdre de la vie au joueur chaque seconde (à titre de démonstration)
        LoseHealthOverTime(10f * Time.deltaTime);
if (healthbar != null)
{
    // Mettre à jour les variables de santé
    float currentHealth = 100f; // Remplacez par la valeur actuelle de la santé du joueur
    float maxHealth = 100f; // Remplacez par la valeur maximale de la santé du joueur

    // Mettre à jour la barre de vie
    healthbar.UpdateHealthBar(currentHealth, maxHealth);
}

    }

    void MovePlayers(float _horizontalMovement)
    {
        // Utilisez l'échelle locale pour déterminer la direction du mouvement
        float horizontalScale = Mathf.Sign(transform.localScale.x);
        _horizontalMovement *= horizontalScale;

        // Calculer la nouvelle vélocité du joueur
        Vector3 targetVelocity = new Vector2(_horizontalMovement * moveSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
    }

    void CheckForObstacle()
    {
        // Raycast vers le bas pour détecter un obstacle (layer "Obstacle" dans cet exemple)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("Obstacle"));

        // Si un obstacle est détecté, arrêter le mouvement
        if (hit.collider != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
void LoseHealthOverTime(float amount)
{
    if (healthbar != null)
    {
        // Obtenez la santé actuelle et maximale du Healthbar
        float currentHealth = healthbar.GetHealth();
        float maxHealth = healthbar.slider.maxValue;

        // Calculer la nouvelle santé
        float newHealth = currentHealth - amount;

        // Assurez-vous que la santé ne dépasse pas la santé maximale
        newHealth = Mathf.Clamp(newHealth, 0f, maxHealth);

        // Définissez la nouvelle santé sur le Healthbar
        healthbar.SetHealth(newHealth);

        // Mettez à jour la barre de santé
        healthbar.UpdateHealthBar(newHealth, maxHealth);
    }
}

}

