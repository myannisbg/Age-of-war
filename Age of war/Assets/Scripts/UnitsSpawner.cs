using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
    public Slider healthSliderPrefab;
    public bool moveRight = true;
    private Queue<GameObject> unitsWaitingToSpawn = new Queue<GameObject>();
    private List<GameObject> activePlayers = new List<GameObject>();
    public int currentAge = 0;

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * 0.2f, Color.green); // Raycast à gauche
        Debug.DrawRay(transform.position, Vector2.right * 0.2f, Color.green); // Raycast à droite
        
        // Vérifier s'il y a de la place pour les unités en attente et les faire spawn si c'est le cas
        CheckSpawnQueue();
    }

    public void SpawnPlayer(GameObject unitPrefab)
    {
        // Vérifier si le prefab de l'unité est valide
        if (unitPrefab == null)
        {
            Debug.LogError("Unit prefab is null!");
            return;
        }

        // Vérifier s'il y a de la place pour faire spawn l'unité
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, LayerMask.GetMask("Default"));
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.2f, LayerMask.GetMask("Default")); // Raycast à gauche
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.2f, LayerMask.GetMask("Default")); // Raycast à droite

        // Si aucun obstacle n'est détecté en dessous, à gauche et à droite de l'unité, faire spawn l'unité
        if (hitDown.collider == null && hitLeft.collider == null && hitRight.collider == null)
        {
            // Instancier l'unité à la position actuelle du spawner en utilisant le prefab de l'unité fourni
            GameObject currentPlayer = Instantiate(unitPrefab, transform.position, Quaternion.identity);

            // Instancier une nouvelle barre de vie pour le joueur
            Slider healthSlider = Instantiate(healthSliderPrefab, currentPlayer.transform);

            // Positionner la barre de vie au-dessus du joueur
            healthSlider.transform.localPosition = new Vector3(0f, 1.5f, 1f); // Ajustez les valeurs selon vos besoins

            // Ajouter le joueur à la liste des joueurs actifs
            activePlayers.Add(currentPlayer);

            // Inverser la direction initiale en fonction de la propriété moveRight du spawner
            float horizontalScale = moveRight ? 1f : -1f;
            currentPlayer.transform.localScale = new Vector3(horizontalScale, 1f, 1f);

            // Attach the DirectionComponent to the player
            DirectionComponent directionComponent = currentPlayer.GetComponent<DirectionComponent>();
            if (directionComponent != null)
            {
                directionComponent.moveRight = moveRight;

                // Mettre à jour la direction du sprite
                directionComponent.SetSpriteDirection(moveRight);
            }

            // Set health for the new player
            UnitHealthManager playerHealth = currentPlayer.GetComponent<UnitHealthManager>();
            if (playerHealth != null)
            {
                playerHealth.SetMaxHealth(100f); // Adjust as needed
            }
        }
        else
        {
            // Si un obstacle est détecté, mettre l'unité dans la file d'attente
            AddPrefabToSpawnQueue(unitPrefab);
        }
    }

    // Fonction pour ajouter un prefab d'unité à la file d'attente
    public void AddPrefabToSpawnQueue(GameObject unitPrefab)
    {
        if (unitPrefab != null)
        {
            unitsWaitingToSpawn.Enqueue(unitPrefab);
        }
    }

    // Fonction pour vérifier s'il y a de la place pour spawner les unités en attente
    private void CheckSpawnQueue()
    {
        // Vérifier s'il y a des unités en attente et si l'espace est libre pour les spawner
        while (unitsWaitingToSpawn.Count > 0)
        {
            GameObject unitPrefab = unitsWaitingToSpawn.Peek(); // Obtenir le premier prefab d'unité en attente

            // Vérifier s'il y a de la place pour faire spawn l'unité
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, LayerMask.GetMask("Default"));
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.2f, LayerMask.GetMask("Default")); // Raycast à gauche
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.2f, LayerMask.GetMask("Default")); // Raycast à droite

            // Si aucun obstacle n'est détecté en dessous, à gauche et à droite de l'unité, faire spawn l'unité et la retirer de la file d'attente
            if (hitDown.collider == null && hitLeft.collider == null && hitRight.collider == null)
            {
                // Instancier l'unité à la position actuelle du spawner en utilisant le prefab de l'unité fourni
                GameObject currentPlayer = Instantiate(unitPrefab, transform.position, Quaternion.identity);

                // Ajouter le joueur à la liste des joueurs actifs
                activePlayers.Add(currentPlayer);

                // Retirer l'unité de la file d'attente
                unitsWaitingToSpawn.Dequeue();
            }
            else
            {
                // S'il y a un obstacle, sortir de la boucle car l'espace n'est pas libre
                break;
            }
        }
    }
}
