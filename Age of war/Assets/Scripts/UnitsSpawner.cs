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
    private bool damageBoostsBool;

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

        // Compter le nombre d'unités alliées sur le terrain
        int allyCount = GameObject.FindGameObjectsWithTag("Ally").Length;
        // Compter le nombre d'unités ennemies sur le terrain
        int enemyCount = GameObject.FindGameObjectsWithTag("Ennemy").Length;

        // Vérifier s'il y a déjà 10 unités alliées ou ennemies sur le terrain
        if (allyCount >= 10)
        {
            // Debug.LogWarning("Nombre maximum d'unités alliées atteint !");
            return;
        }

        if (enemyCount >= 10)
        {
            // Debug.LogWarning("Nombre maximum d'unités ennemies atteint !");
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
            currentPlayer.transform.localScale = new Vector3(Mathf.Abs(currentPlayer.transform.localScale.x) * horizontalScale, currentPlayer.transform.localScale.y, currentPlayer.transform.localScale.z);

            // Attacher le composant DirectionComponent au joueur
            DirectionComponent directionComponent = currentPlayer.GetComponent<DirectionComponent>();
            if (directionComponent != null)
            {
                directionComponent.moveRight = moveRight;

                // Mettre à jour la direction du sprite
                directionComponent.SetSpriteDirection(moveRight);
            }

            // Définir la santé pour le nouveau joueur
            UnitHealthManager playerHealth = currentPlayer.GetComponent<UnitHealthManager>();
            if (playerHealth != null)
            {
                playerHealth.SetMaxHealth(100f); // Ajustez selon les besoins
            }
        }
        else
        {
            // Si un obstacle est détecté, mettre l'unité dans la file d'attente
            AddPrefabToSpawnQueue(unitPrefab);
        }
    }

    public void SpawnPlayerBoosted(GameObject unitPrefab)
    {
        // Vérifier le nombre d'unités alliées et ennemies avant de faire spawn
        int allyCount = GameObject.FindGameObjectsWithTag("Ally").Length;
        int enemyCount = GameObject.FindGameObjectsWithTag("Ennemy").Length;

        // Vérifier s'il y a déjà 10 unités alliées ou ennemies sur le terrain
        if (allyCount >= 10)
        {
            // Debug.LogWarning("Nombre maximum d'unités alliées atteint !");
            return;
        }

        if (enemyCount >= 10)
        {
            // Debug.LogWarning("Nombre maximum d'unités ennemies atteint !");
            return;
        }

        // Appeler la fonction SpawnPlayer pour faire spawn l'unité
        SpawnPlayer(unitPrefab);

        // Appliquer le boost de statistiques si nécessaire
        if (damageBoostsBool  && activePlayers.Count > 0)
        {
            // Récupérer la dernière unité ajoutée
            GameObject currentPlayer = activePlayers[activePlayers.Count - 1];
            ApplyStatBoost(currentPlayer);
        }
    }



private void ApplyStatBoost(GameObject unit)
{
    // Vérifier si l'unité est un tank ou un archer
    if (unit.CompareTag("Ennemy"))
    {
        // Modifier les statistiques de l'unité
        Unit unitStats = unit.GetComponent<Unit>();
        if (unitStats != null)
        {
            unitStats.maxHealth *= 1.2f; // Augmenter les points de vie max
            unitStats.damageDealt *= 1.2f; // Augmenter les dégâts infligés
            // Ajoutez d'autres modifications au besoin
        }
        // else
        // {
        //     Debug.LogWarning("L'unité " + unit.name + " n'a pas de composant Bases pour appliquer le boost de statistiques.");
        // }
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
            // Compter le nombre d'unités alliées et ennemies sur le terrain
            int allyCount = GameObject.FindGameObjectsWithTag("Ally").Length;
            int enemyCount = GameObject.FindGameObjectsWithTag("Ennemy").Length;
            // Vérifier s'il y a déjà 10 unités alliées ou ennemies sur le terrain
            if (allyCount >= 10)
            {
                // Debug.LogWarning("Nombre maximum d'unités alliées atteint !");
                break; // Sortir de la boucle si le nombre maximum d'unités alliées est atteint
            }
            if (enemyCount >= 10)
            {
                // Debug.LogWarning("Nombre maximum d'unités ennemies atteint !");
                break; // Sortir de la boucle si le nombre maximum d'unités ennemies est atteint
            }
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
