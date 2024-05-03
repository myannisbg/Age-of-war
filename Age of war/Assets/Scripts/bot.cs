using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot : MonoBehaviour

{

    public bool isBot = false;
    public float botSpawnInterval = 5f; // Interval de spawn en secondes pour le bot
    public float timeSinceLastSpawn = 0f;
    public PlayerSpawner spawner;
    public List<GameObject> unitPrefabs; 
    public GlobalAge ageValue;
    void Start()
    {
        // Récupérer la référence au script PlayerSpawner s'il n'est pas attribué
        if (spawner == null)
        {
            spawner = FindObjectOfType<PlayerSpawner>();
        }

    }

void Update()
{
    SpawnUnits();
}




    void BotSpawnUnitByAge()
{
    // Vérifier si le script PlayerSpawner est assigné
    if (spawner != null)
    {
        // Définir les bornes des tranches d'âge
        int ageIndex = ageValue.getAge();
        int lowerBound = ageIndex * 3;
        int upperBound = ageIndex * 3 + 2;

        // Vérifier si l'âge est valide
        if (ageIndex >= 0 && upperBound < unitPrefabs.Count)
        {
            // Générer un nombre aléatoire dans la plage de l'âge actuel
            int randomIndex = Random.Range(lowerBound, upperBound + 1);

            // Récupérer la préfab d'unité correspondant au nombre aléatoire
            GameObject unitPrefab = unitPrefabs[randomIndex];

            // Appeler la méthode SpawnPlayer du PlayerSpawner en passant la préfab d'unité
            spawner.SpawnPlayer(unitPrefab);
            print(unitPrefab);
        }
        else
        {
            Debug.LogError("Invalid age index or unit prefab list is empty!");
        }
    }
    else
    {
        Debug.LogError("PlayerSpawner script is not assigned!");
    }
}

void SpawnUnits()
{
    // Vérifier si le bot est activé et s'il y a un spawner attribué
    if (isBot && spawner != null)
    {
        // Incrémenter le temps écoulé depuis le dernier spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Vérifier si le temps écoulé dépasse l'intervalle de spawn
        if (timeSinceLastSpawn >= botSpawnInterval)
        {
            // Réinitialiser le temps écoulé
            timeSinceLastSpawn = 0f;

            // Appeler la fonction pour faire spawn une unité en fonction de l'âge actuel
            BotSpawnUnitByAge();
        }
    }
}





}
