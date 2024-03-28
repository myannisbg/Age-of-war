using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Slider healthSliderPrefab;
    public bool moveRight = true;
    

    private List<GameObject> activePlayers = new List<GameObject>();

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

        // Set health for the new player
        UnitHealthManager playerHealth = currentPlayer.GetComponent<UnitHealthManager>();
        if (playerHealth != null)
        {
            playerHealth.SetMaxHealth(100f); // Adjust as needed
            
        }
    }

}
