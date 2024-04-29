using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        // Définir la direction dans laquelle le joueur doit se déplacer (horizontalement)
        float horizontalMovement = 0.5f; // Modification de la direction du mouvement

        // Déplacer le joueur
        MovePlayers(horizontalMovement);

        CheckForBase();
    Debug.DrawRay(transform.position,Vector2.right * 1.5f, Color.red);
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

    void CheckForBase()
{
    // Récupérer le tag de l'unité
    string unitTag = gameObject.tag;

    // Raycast vers l'avant pour détecter une base
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, LayerMask.GetMask("Base"));

    // Si une base est détectée
    if (hit.collider != null)
    {
        // Vérifier si l'unité peut passer à travers cette base en fonction de son tag
        if ((unitTag == "Ally" && hit.collider.CompareTag("BaseEnnemy")) ||
            (unitTag == "Ennemy" && hit.collider.CompareTag("Base")))
        {
            // Arrêter le mouvement
            rb.velocity = Vector2.zero;
        }
    }
}

}


