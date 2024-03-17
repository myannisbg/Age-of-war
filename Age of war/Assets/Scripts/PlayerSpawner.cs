using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Slider healthSliderPrefab;
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
        foreach (GameObject player in activePlayers.ToArray())
        {
            if (player != null)
            {
                // Faites perdre de la vie à chaque joueur
                LoseHealth(player, amount);
            }
            else
            {
                // Supprimez le joueur de la liste s'il a été détruit
                activePlayers.Remove(player);
            }
        }
    }

    void LoseHealth(GameObject player, float amount)
    {
        // Vérifiez d'abord si le joueur est null ou s'il a été détruit
        if (player == null || !player.activeSelf)
        {
            return; // Sortir de la méthode si le joueur est détruit ou désactivé
        }

        // Ensuite, accédez au composant seulement si le joueur est valide
        DirectionComponent directionComponent = player.GetComponent<DirectionComponent>();
        if (directionComponent != null)
        {
            // Faites perdre de la vie au joueur
            directionComponent.LoseHealth(amount);

            // Vérifier si le joueur est mort
            if (directionComponent.IsDead())
            {
                // Détruire le joueur actuel
                DestroyPlayer(player);

                // Réapparition du joueur
                SpawnPlayer();
            }
        }
    }
}
