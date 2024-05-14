using UnityEngine;

public class BackgroundBouncer : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement
    public Vector2 direction = Vector2.left; // Direction initiale
    public float leftBoundary = -11f; // Limite gauche
    public float rightBoundary = 10f; // Limite droite

    void Update()
    {
        // Déplacer l'arrière-plan
        transform.Translate(direction * speed * Time.deltaTime);

        // Vérifier les limites et inverser la direction si nécessaire
        if (transform.position.x <= leftBoundary)
        {
            direction = Vector2.right; // Changer de direction vers la droite
        }
        else if (transform.position.x >= rightBoundary)
        {
            direction = Vector2.left; // Changer de direction vers la gauche
        }
    }
}
