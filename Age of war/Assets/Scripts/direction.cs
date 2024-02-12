using UnityEngine;

public class DirectionComponent : MonoBehaviour
{
    // Ajoutez une propriété pour définir la direction dans l'éditeur Unity
    public bool moveRight = true;

    private float health = 100f; // Ajoutez une variable de santé pour l'exemple

    // Autres propriétés et méthodes nécessaires
    // ...

    public void LoseHealth(float amount)
    {
        // Ajoutez le code pour faire perdre de la vie
        health -= amount;
    }

    public float GetHealth()
    {
        // Ajoutez le code pour obtenir la valeur de la vie
        return health;
    }

    public bool IsDead()
    {
        // Ajoutez le code pour vérifier si le joueur est mort
        return health <= 0f;
    }
}
