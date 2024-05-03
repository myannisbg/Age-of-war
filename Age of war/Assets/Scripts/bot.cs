using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public enum BotFunction
    {
        BotIsDumb,
        BotIsTrying,
        BotIsPlaying,
        BotIsTooStrong
    }

    public BotFunction currentFunction;
    public List<BotFunction> availableFunctions;

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

                // Appeler la fonction correspondante en fonction de la fonction actuelle du bot
                switch (currentFunction)
                {
                    case BotFunction.BotIsDumb:
                        BotIsDumb();
                        break;
                    case BotFunction.BotIsTrying:
                        BotIsTrying();
                        break;
                    case BotFunction.BotIsPlaying:
                        // Appeler la fonction correspondante
                        break;
                    case BotFunction.BotIsTooStrong:
                        // Appeler la fonction correspondante
                        break;
                }
            }
        }
    }

    void BotIsDumb()
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


// Déclarer une variable pour stocker le temps écoulé depuis le dernier spawn pour chaque âge
private Dictionary<int, float> lastSpawnTimes = new Dictionary<int, float>();

    void BotIsTrying()
    {
        // Vérifier si le script PlayerSpawner est assigné
        if (spawner != null)
        {
            // Définir les bornes des tranches d'âge
            int ageIndex = ageValue.getAge();
            int lowerBound = ageIndex * 3;
            int upperBound = lowerBound + 2; 

            // Vérifier si l'âge est valide
            if (ageIndex >= 0 && upperBound < unitPrefabs.Count)
            {
                // Vérifier si la clé existe dans le dictionnaire
                if (!lastSpawnTimes.ContainsKey(ageIndex))
                {
                    // Ajouter la clé au dictionnaire avec le temps actuel comme valeur
                    lastSpawnTimes.Add(ageIndex, Time.timeSinceLevelLoad);
                }

                // Vérifier si suffisamment de temps s'est écoulé pour permettre le spawn de la quatrième unité
                if (Time.timeSinceLevelLoad - lastSpawnTimes[ageIndex] >= 20) // 20 secondes et plus depuis le dernier spawn de cet âge
                {
                    // Augmenter l'indice supérieur pour inclure la quatrième unité
                    upperBound++;

                    // Réinitialiser le temps écoulé pour cet âge
                    lastSpawnTimes[ageIndex] = Time.timeSinceLevelLoad;
                }

                // Générer un nombre aléatoire dans la plage de l'âge actuel
                int randomIndex = Random.Range(lowerBound, upperBound + 1);

                // Si randomIndex dépasse le nombre total d'unités, le ramener à la plage correcte
                if (randomIndex >= unitPrefabs.Count)
                {
                    randomIndex = Random.Range(lowerBound, upperBound);
                }

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

}
