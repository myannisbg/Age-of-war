using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Faites glisser votre joueur préfabriqué ici
    public Slider healthSlider; // Faites glisser votre barre de vie ici

    private GameObject currentPlayer;
    private float playerHealth = 100f; // Valeur initiale de la vie

    void Start()
    {
        // Instancier le joueur au début du jeu
        SpawnPlayer();
    }

    void Update()
    {
        // Exemple : Appuyez sur la touche "P" pour réinstancier le joueur
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Détruire le joueur actuel
            DestroyPlayer();

            // Instancier un nouveau joueur
            SpawnPlayer();
        }

        // Exemple : Faites perdre de la vie au joueur chaque update
        LoseHealth(10f * Time.deltaTime); // Ajustez la valeur selon vos besoins
    }

    void SpawnPlayer()
    {
        // Instancier le joueur à la position actuelle du spawner
        currentPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        healthSlider.value = playerHealth; // Mettre à jour la barre de vie
    }

    void DestroyPlayer()
    {
        // Détruire le joueur actuel
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }
    }

 private bool isRespawning = false; // Nouvelle variable pour vérifier si le joueur est en train de respawn

void LoseHealth(float amount)
{
    // Si le joueur est en train de respawn, ne pas effectuer de perte de vie supplémentaire
    if (isRespawning)
    {
        return;
    }

    // Faites perdre de la vie au joueur
    playerHealth -= amount;

    // Mettre à jour la barre de vie
    healthSlider.value = playerHealth;

    // Vérifier si le joueur est mort (vie <= 0)
    if (playerHealth <= 0f)
    {
        // Vérifier si le joueur n'est pas déjà en train de respawn
        if (!isRespawning)
        {
            // Définir la variable isRespawning à true pour éviter le respawn en boucle
            isRespawning = true;

            // Réinstancer le joueur ou effectuer d'autres actions en conséquence
            RespawnPlayer();
        }
    }
}

void RespawnPlayer()
{
    // Détruire le joueur actuel
    DestroyPlayer();

    // Réinitialiser la vie du joueur
    playerHealth = 100f;
    healthSlider.value = playerHealth;

    // Réinstancer le joueur
    SpawnPlayer();

    // Réinitialiser la variable isRespawning
    isRespawning = false;
}


}
