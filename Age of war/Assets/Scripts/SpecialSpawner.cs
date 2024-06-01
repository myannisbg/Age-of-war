using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpecialSpawner : MonoBehaviour
{
    public List<GameObject> projectilePrefabs; // Liste des préfabs de projectiles
    public float spawnInterval = 0.1f; // Intervalle entre chaque projectile
    public int baseNumberOfProjectiles = 10; // Nombre de projectiles de base
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
        int currentAge = ageValue.getAge();
        int numberOfProjectiles = Mathf.CeilToInt(baseNumberOfProjectiles * Mathf.Pow(1.1f, currentAge));
        float precisionFactor = 1.0f / (currentAge + 1);

        bool isLastAge = (currentAge == projectilePrefabs.Count - 1);

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            if (currentAge >= 0 && currentAge < projectilePrefabs.Count)
            {
                // Sélectionner le préfab de projectile correspondant à l'âge actuel
                GameObject projectilePrefab = projectilePrefabs[currentAge];

                Vector3 spawnPosition;

                if (isLastAge)
                {
                    // Si c'est le dernier âge, les projectiles couvrent toute la scène
                    spawnPosition = new Vector3(
                        Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                        spawnHeight,
                        Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2))
                        + transform.position;
                }
                else
                {
                    // Sinon, cibler les ennemis
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");

                    if (enemies.Length > 0)
                    {
                        GameObject targetEnemy = enemies[Random.Range(0, enemies.Length)];

                        spawnPosition = new Vector3(
                            Random.Range(-spawnAreaSize.x / 2 * precisionFactor, spawnAreaSize.x / 2 * precisionFactor),
                            spawnHeight,
                            Random.Range(-spawnAreaSize.y / 2 * precisionFactor, spawnAreaSize.y / 2 * precisionFactor))
                            + targetEnemy.transform.position;
                    }
                    else
                    {
                        Debug.LogError("No enemies found with the tag 'Enemy'!");
                        continue;
                    }
                }

                Instantiate(projectilePrefab, spawnPosition, Quaternion.identity); // Pas de rotation supplémentaire
                Debug.Log("Projectile instantiated at position: " + spawnPosition + (isLastAge ? " covering whole area." : " targeting enemy."));

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
