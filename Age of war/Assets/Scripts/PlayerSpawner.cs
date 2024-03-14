using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; 
    public Slider healthSlider; 

    // Nouvelle propriété pour définir la direction dans l'éditeur Unity
    public bool moveRight = true;

    private List<GameObject> activePlayers = new List<GameObject>();
    private float playerHealth = 100f;

    void Start()
    {
        // Instancier un joueur au début du jeu
        SpawnPlayer();
    }

    void Update()
    {
        // Exemple : Appuyez sur la touche "P" pour instancier un nouveau joueur
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Instancier un nouveau joueur
            SpawnPlayer();
        }

        // Exemple : Faites perdre de la vie à tous les joueurs chaque update
        LoseHealthToAllPlayers(10f * Time.deltaTime); // Ajustez la valeur selon vos besoins
    }

    void SpawnPlayer()
    {
        // Instancier un joueur à la position actuelle du spawner
        GameObject currentPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        // Ajouter le joueur à la liste des joueurs actifs
        activePlayers.Add(currentPlayer);

        // Attach the DirectionComponent to the player
        DirectionComponent directionComponent = currentPlayer.GetComponent<DirectionComponent>();
        if (directionComponent != null)
        {
            // Définir la direction initiale en fonction de la propriété moveRight du spawner
            float horizontalScale = moveRight ? 1f : -1f;
            currentPlayer.transform.localScale = new Vector3(horizontalScale, 1f, 1f);
            directionComponent.SetDirection(moveRight);
        }

        healthSlider.value = playerHealth; // Mettre à jour la barre de vie
    }

    void DestroyPlayer(GameObject player)
    {
        // Retirer le joueur de la liste des joueurs actifs
        activePlayers.Remove(player);

        // Détruire le joueur
        Destroy(player);
    }

    void LoseHealthToAllPlayers(float amount)
    {
        foreach (GameObject player in activePlayers)
        {
            // Faites perdre de la vie à chaque joueur
            LoseHealth(player, amount);
        }
    }

    void LoseHealth(GameObject player, float amount)
    {
        DirectionComponent directionComponent = player.GetComponent<DirectionComponent>();
        if (directionComponent != null)
        {
            // Faites perdre de la vie au joueur
            directionComponent.LoseHealth(amount);

            // Mettre à jour la barre de vie
            healthSlider.value = directionComponent.GetHealth();

            // Vérifier si le joueur est mort
            if (directionComponent.IsDead())
            {
                // Réinstancer le joueur ou effectuer d'autres actions en conséquence
                RespawnPlayer(player);
            }
        }
    }

    void RespawnPlayer(GameObject player)
    {
        // Détruire le joueur actuel
        DestroyPlayer(player);

        // Instancier un nouveau joueur
        SpawnPlayer();
    }
}
