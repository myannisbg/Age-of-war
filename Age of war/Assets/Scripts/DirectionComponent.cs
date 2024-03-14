using UnityEngine;

public class DirectionComponent : MonoBehaviour
{
    // Ajoutez d'autres membres ou méthodes selon vos besoins

    public bool moveRight = true;

    private float health = 100f; // Ajoutez la gestion de la santé si elle n'est pas déjà présente

    public void LoseHealth(float amount)
    {
        // Implémentez la logique de perte de vie ici
        health -= amount;
    }

    public float GetHealth()
    {
        // Implémentez la logique pour obtenir la vie actuelle ici
        return health;
    }

    public bool IsDead()
    {
        // Implémentez la logique pour vérifier si le joueur est mort ici
        return health <= 0f;
    }
}
