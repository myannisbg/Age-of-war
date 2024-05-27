using UnityEngine;
using System.Collections;

public class SpecialSpawner : MonoBehaviour
{
    public GameObject projectilePrefab; // Le préfab du projectile
    public float spawnInterval = 0.1f; // Intervalle entre chaque projectile
    public int numberOfProjectiles = 10; // Nombre de projectiles à lancer
    public Vector2 spawnAreaSize = new Vector2(30f, 30f); // Taille de la zone de chute
    public float spawnHeight = 50f; // Hauteur de chute des projectiles

    private bool isSpawning = false; // Pour éviter de déclencher plusieurs fois le spawn

    void Start()
    {
        // Initialisation si nécessaire
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            Debug.Log("StartSpawning called"); // Ajoutez un message de débogage
            StartCoroutine(LaunchProjectiles());
        }
    }

    private IEnumerator LaunchProjectiles()
    {
        isSpawning = true;
        Quaternion fixedRotation = Quaternion.Euler(1, 0, 44); // Définition de la rotation fixe ici
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Calculer une position aléatoire dans la zone définie
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                spawnHeight,
                0 // Si vous utilisez un environnement 2D, gardez la position Z à 0
            ) + transform.position;

            // Instancier le projectile avec la rotation fixée
            Instantiate(projectilePrefab, spawnPosition, fixedRotation); // Utilisez fixedRotation au lieu de Quaternion.identity
            Debug.Log("Projectile instantiated at position: " + spawnPosition + " with fixed rotation: " + fixedRotation.eulerAngles); // Ajoutez un message de débogage

            // Attendre avant de lancer le prochain projectile
            yield return new WaitForSeconds(spawnInterval);
        }
        isSpawning = false;
    }
}
