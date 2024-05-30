using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpecialSpawner : MonoBehaviour
{
    public List<GameObject> projectilePrefabs; // Liste des préfabs de projectiles
    public float spawnInterval = 0.1f; // Intervalle entre chaque projectile
    public int numberOfProjectiles = 10; // Nombre de projectiles à lancer
    public Vector2 spawnAreaSize = new Vector2(30f, 30f); // Taille de la zone de chute
    public float spawnHeight = 50f; // Hauteur de chute des projectiles
    public GlobalAge ageValue;

    private bool isSpawning = false;

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            Debug.Log("StartSpawning called");
            StartCoroutine(LaunchProjectiles());
        }
    }

    private IEnumerator LaunchProjectiles()
    {
        isSpawning = true;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            if (ageValue.getAge() >= 0 && ageValue.getAge() < projectilePrefabs.Count)
            {
                // Sélectionner le préfab de projectile correspondant à l'âge actuel
                GameObject projectilePrefab = projectilePrefabs[ageValue.getAge()];

                Vector3 spawnPosition = new Vector3(
                    Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), spawnHeight, 0) + transform.position;

                Instantiate(projectilePrefab, spawnPosition, Quaternion.identity); // Pas de rotation supplémentaire
                Debug.Log("Projectile instantiated at position: " + spawnPosition + " with default rotation.");

                // Attendre avant de lancer le prochain projectile
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                Debug.LogError("Age index out of range or projectile prefab list is empty!");
            }
        }
        isSpawning = false;
    }
}

