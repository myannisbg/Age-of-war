using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    public Healthbar healthbar;

    void FixedUpdate()
    {
        // Obtenir le composant DirectionComponent attaché au joueur
        DirectionComponent directionComponent = GetComponent<DirectionComponent>();

        if (directionComponent != null)
        {
            // Déterminer la direction du mouvement en fonction du composant
            float horizontalMovement = directionComponent.moveRight ? 1f : -1f;

            // Déplacer le joueur
            MovePlayers(horizontalMovement);

            // Vérifier si le joueur est en collision avec un obstacle
            CheckForObstacle();
        }
        if (healthbar != null)
        {
            healthbar.transform.position = transform.position;
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
}
