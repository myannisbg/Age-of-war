using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset = new Vector3(0.5f, 0.0f, 0.0f); // Exemple : décalage de 0.5 unité vers la droite

    void FixedUpdate()
    {
        // Définir la direction dans laquelle le joueur doit se déplacer (horizontalement)
        float horizontalMovement = 0.5f; // Modification de la direction du mouvement

        // Déplacer le joueur
        MovePlayers(horizontalMovement);

        CheckForBase();
        // Debug.DrawRay(transform.position,Vector2.right * 0.5f, Color.red);
        // Debug.DrawRay(transform.position,Vector2.right * 0.5f, Color.green);
        // Définir le décalage (offset) à partir du centre de l'objet

        // Dessiner un rayon à partir du point de départ ajusté
        Debug.DrawRay(transform.position + offset, Vector2.right , Color.blue);

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



        // Convertir la position de l'unité en Vector2
        Vector2 unitPosition = new Vector2(transform.position.x, transform.position.y);

        // Raycast vers l'avant pour détecter une base ennemie
        RaycastHit2D hitEnemyBase = Physics2D.Raycast(unitPosition, Vector2.left, 0.5f);
        // Raycast vers l'avant pour détecter une base alliée
        RaycastHit2D hitBase = Physics2D.Raycast(unitPosition, Vector2.right, 0.5f);
        // Raycast vers l'avant pour détecter une unité ennemie
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position - offset, Vector2.left, 0.5f);
        // Raycast vers l'avant pour détecter une unité alliée
        RaycastHit2D hitAlly = Physics2D.Raycast(transform.position + offset, Vector2.right, 0.5f);

        // Si une base ennemie est détectée et que l'unité actuelle est alliée
        if (hitEnemyBase.collider != null && hitEnemyBase.collider.CompareTag("BaseEnnemy") && unitTag == "Ally")
        {
            // Arrêter le mouvement
            rb.velocity = Vector2.zero;
        }

        // Si une base alliée est détectée et que l'unité actuelle est ennemie
        if (hitBase.collider != null && hitBase.collider.CompareTag("Base") && unitTag == "Ennemy")
        {
            // Arrêter le mouvement
            rb.velocity = Vector2.zero;
        }

        // Si une unité alliée est détectée et que l'unité actuelle est alliée
        if (hitAlly.collider != null && hitAlly.collider.CompareTag("Ally") && unitTag == "Ally" && hitAlly.collider.gameObject != gameObject)
        {
            // Arrêter le mouvement
            rb.velocity = Vector2.zero;
        }

        // Si une unité ennemie est détectée et que l'unité actuelle est ennemie
        if (hitEnemy.collider != null && hitEnemy.collider.CompareTag("Ennemy") && unitTag == "Ennemy" && hitEnemy.collider.gameObject != gameObject)
        {
            // Arrêter le mouvement
            rb.velocity = Vector2.zero;
        }
    }

}


