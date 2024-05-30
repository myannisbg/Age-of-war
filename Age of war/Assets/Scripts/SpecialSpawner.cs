using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpecialSpawner : MonoBehaviour
{
    public List<GameObject> projectilePrefabs; // Liste des préfabs de projectiles
    public float spawnInterval = 0.1f; // Intervalle entre chaque projectile
    public int numberOfProjectiles = 10; // Nombre fixe de projectiles à lancer
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
        int age = ageValue.getAge();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");

        if (enemies.Length == 0)
        {
            Debug.LogWarning("No enemies found.");
            isSpawning = false;
            yield break;
        }

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            if (age >= 0 && age < projectilePrefabs.Count)
            {
                // Sélectionner le prefab de projectile correspondant à l'âge actuel
                GameObject projectilePrefab = projectilePrefabs[age];

                // Sélectionner une position de cible dynamique pour les âges 6 et 7
                Vector3 targetPosition;
                if (age == 6 || age == 7)
                {
                    // Recalculer les positions des ennemis pour chaque projectile
                    enemies = GameObject.FindGameObjectsWithTag("Ennemy");
                    targetPosition = enemies[i % enemies.Length].transform.position + new Vector3(0, spawnHeight, 0);
                }
                else
                {
                    // Utiliser les positions initiales des ennemis
                    targetPosition = enemies[i % enemies.Length].transform.position + new Vector3(0, spawnHeight, 0);
                }

                Instantiate(projectilePrefab, targetPosition, Quaternion.identity); // Pas de rotation supplémentaire
                Debug.Log("Projectile instantiated at position: " + targetPosition);

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
