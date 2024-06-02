using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset = new Vector3(0.2f, 0.0f, 0.0f); // Exemple : décalage de 0.5 unité vers la droite
    float horizontalMovement = 0.5f; // Modification de la direction du mouvement
    public Animator animator;
    public float characterVelocity ;


    void FixedUpdate()
    {
        // Définir la direction dans laquelle le joueur doit se déplacer (horizontalement)
       

        // Déplacer le joueur
        MovePlayers(horizontalMovement);
        float characterVelocity =Mathf.Abs(rb.velocity.x);
        
        CheckForBase();
        // Debug.DrawRay(transform.position,Vector2.right * 1f, Color.red);
        // Debug.DrawRay(transform.position,Vector2.left * 1f, Color.green);
        // Définir le décalage (offset) à partir du centre de l'objet

        // Dessiner un rayon à partir du point de départ ajusté
        // Debug.DrawRay(transform.position + offset, Vector2.right , Color.blue);
        // Debug.DrawRay(transform.position  -offset, Vector2.left , Color.blue);
        Debug.DrawRay(transform.position - offset, Vector2.left * 0.5f, Color.red); // Pour les unités ennemies
    Debug.DrawRay(transform.position + offset, Vector2.right * 0.5f, Color.blue); // Pour les unités alliées

    }

    void MovePlayers(float horizontalMovement)
    {
        // Utilisez l'échelle locale pour déterminer la direction du mouvement
        float horizontalScale = Mathf.Sign(transform.localScale.x);
        horizontalMovement *= horizontalScale;

        // Calculer la nouvelle vélocité du joueur
        Vector3 targetVelocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
    }

    void CheckForBase()
    {
        bool foundSomethingToHit = false;
        // Récupérer le tag de l'unité
        string unitTag = gameObject.tag;

        // Raycast vers l'avant pour détecter une base ennemie
        RaycastHit2D hitEnemyBase = Physics2D.Raycast(transform.position+offset, Vector2.right, 1f);
        // Raycast vers l'avant pour détecter une base alliée
        RaycastHit2D hitBase = Physics2D.Raycast(transform.position-offset, Vector2.left, 1f);
        // Raycast vers l'avant pour détecter une unité ennemie
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position - offset, Vector2.left, 0.8f);
        // Raycast vers l'avant pour détecter une unité alliée
        RaycastHit2D hitAlly = Physics2D.Raycast(transform.position + offset, Vector2.right, 0.8f);

        // Si une base ennemie est détectée et que l'unité actuelle est alliée
        if (hitEnemyBase.collider != null && hitEnemyBase.collider.CompareTag("BaseEnnemy") && unitTag == "Ally")
        {
            // Arrêter le mouvement
            rb.velocity = Vector3.zero;
        }

    //      if (hitAlly.collider != null)
    // {
    //     Debug.Log($"Hit Ally: {hitAlly.collider.tag}");
    // }

    //     if (hitEnemy.collider != null)
    // {
    //     Debug.Log($"Hit Enemy: {hitEnemy.collider.tag}");
    // }

        // Si une base alliée est détectée et que l'unité actuelle est ennemie
        if (hitBase.collider != null && hitBase.collider.CompareTag("Base") && unitTag == "Ennemy")
        {
            // Arrêter le mouvement
            rb.velocity = Vector3.zero;
        }

        // Si une unité alliée est détectée et que l'unité actuelle est alliée
        if (hitAlly.collider != null && hitAlly.collider.CompareTag("Ally") && unitTag == "Ally" && hitAlly.collider.gameObject != gameObject)
        {
            foundSomethingToHit = true;
            // Arrêter le mouvement
            rb.velocity = Vector3.zero;
        }

        // Si une unité ennemie est détectée et que l'unité actuelle est ennemie
        if (hitEnemy.collider != null && hitEnemy.collider.CompareTag("Ennemy") && unitTag == "Ennemy" && hitEnemy.collider.gameObject != gameObject)
        {
            foundSomethingToHit = true;
            // Arrêter le mouvement
            rb.velocity = Vector3.zero;
        }
        if (foundSomethingToHit == true) {animator.SetBool("isWalking", false);} else {animator.SetBool("isWalking", true);}
    }


}


