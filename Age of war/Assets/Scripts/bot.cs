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
    private int tankCount =  0;
    private int archerCount =  0;
    private bool scenarioSpecific = true;
    private int countOfUnits=0;
    
    


private Dictionary<int, float> lastSpawnTimes = new Dictionary<int, float>();


    void Start()
    {
        string unitTag = gameObject.tag;
        gameObject.tag = "Ennemy";
        for (int i = 0; i < unitPrefabs.Count / 4; i++)
    {
        lastSpawnTimes.Add(i, 0f); // Initialiser tous les âges à 0 secondes
    }
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
                        BotIsPlaying();
                        break;
                    case BotFunction.BotIsTooStrong:
                        // Appeler la fonction correspondante
                        //BotIsTooStrong();
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
             int lowerBound = ageValue.getAge() * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge



            // Vérifier si l'âge est valide
            if (ageValue.getAge() >= 0 && upperBound < unitPrefabs.Count)
            {
                // Générer un nombre aléatoire dans la plage de l'âge actuel
                int randomIndex = Random.Range(lowerBound, upperBound);

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

void BotIsTrying()
{
    // Vérifier si le script PlayerSpawner est assigné
    if (spawner != null)
    {
        // Définir les bornes des tranches d'âge
        int ageIndex = ageValue.getAge();
        int lowerBound = ageIndex * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge

        // Vérifier si l'âge est valide
        if (ageIndex >= 0 && upperBound < unitPrefabs.Count)
        {
            // Générer un nombre aléatoire dans la plage de l'âge actuel
            int randomIndex = Random.Range(lowerBound, upperBound + 1);

            // Si le temps requis est écoulé pour l'unité du upperBound, ou si l'index correspond à une unité précédente, alors procéder au spawn
            if (Time.timeSinceLevelLoad - lastSpawnTimes[ageIndex] >= 60 || randomIndex != upperBound)
            {
                // Si le temps requis est écoulé pour l'unité du upperBound, réinitialiser le temps écoulé pour cet âge
                if (randomIndex == upperBound && Time.timeSinceLevelLoad - lastSpawnTimes[ageIndex] >= 60)
                {
                    lastSpawnTimes[ageIndex] = Time.timeSinceLevelLoad;
                }

                // Récupérer la préfab d'unité correspondant au nombre aléatoire
                GameObject unitPrefab = unitPrefabs[randomIndex];

                // Appeler la méthode SpawnPlayer du PlayerSpawner en passant la préfab d'unité
                spawner.SpawnPlayer(unitPrefab);
                
            }
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




void BotIsPlaying()
{
    // Vérifier si le script PlayerSpawner est assigné
    if (spawner != null)
    {
        // Définir les bornes des tranches d'âge
        int ageIndex = ageValue.getAge();
        int lowerBound = ageIndex * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge
        
        


        // Vérifier si l'âge est valide
        if (ageIndex >= 0 && upperBound < unitPrefabs.Count)
        {
            // Générer un nombre aléatoire dans la plage de l'âge actuel
            int randomIndex = Random.Range(lowerBound, upperBound + 1);

            if (scenarioSpecific==false){
            // Si le temps requis est écoulé pour l'unité du upperBound, ou si l'index correspond à une unité précédente, alors procéder au spawn
                    if (Time.timeSinceLevelLoad - lastSpawnTimes[ageIndex] >= 60 || randomIndex != upperBound)
                    {
                        // Si le temps requis est écoulé pour l'unité du upperBound, réinitialiser le temps écoulé pour cet âge
                        if (randomIndex == upperBound && Time.timeSinceLevelLoad - lastSpawnTimes[ageIndex] >= 60)
                        {
                            lastSpawnTimes[ageIndex] = Time.timeSinceLevelLoad;
                        }

                        // Récupérer la préfab d'unité correspondant au nombre aléatoire
                        GameObject unitPrefab = unitPrefabs[randomIndex];

                        // Appeler la méthode SpawnPlayer du PlayerSpawner en passant la préfab d'unité
                        spawner.SpawnPlayerBoosted(unitPrefab);
                        countOfUnits++;
                        //print("countOfUnits = " + countOfUnits);
                        if (countOfUnits == 3)
                        {
                            scenarioSpecific = true;
                            // print("retour a 0");
                            countOfUnits = 0;
                        }
                        
                    }   
                }

            else if (scenarioSpecific == true) {
                
                // Si le nombre de tanks est égal à 0 et le nombre d'archers est égal à 0
                if (tankCount == 0 && archerCount == 0)
                {
                    // Spawn d'un tank (unité 2) et incrémentation du compteur de tanks
                    SpawnTank();
                    tankCount++;
                    // print("tank");
                    print(tankCount);
                }
                // Si le nombre de tanks est égal à 1 et le nombre d'archers est égal à 0
                else if (tankCount == 1 && archerCount == 0)
                {
                    // Spawn d'un archer (unité 3) et incrémentation du compteur d'archers
                    SpawnArcher();
                    archerCount++;
                    // print("archer 1");
                }
                // Si le nombre de tanks est égal à 1 et le nombre d'archers est égal à 1
                else if (tankCount == 1 && archerCount == 1)
                {
                    // Spawn d'un autre archer (unité 3) et réinitialisation des compteurs à 0
                    SpawnArcher();
                    tankCount = 0;
                    archerCount = 0;
                    scenarioSpecific=false;
                    // print("archer 2");
                }
            }
        }
        else
        {
            Debug.LogError("PlayerSpawner script is not assigned!");
        }
    }
}



    // Fonction pour faire apparaître un tank
    void SpawnTank()
    {
        int ageIndex = ageValue.getAge();
        int lowerBound = ageIndex * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge
        // Utilisez les bornes pour déterminer l'index de l'unité de tank dans votre liste
        int tankIndex = lowerBound+1; // Utilisez le lowerBound pour l'index du tank
        GameObject tankPrefab = unitPrefabs[tankIndex];
        spawner.SpawnPlayerBoosted(tankPrefab); // Supposons que vous avez une fonction SpawnPlayer dans votre spawner
    }

    // Fonction pour faire apparaître un archer
    void SpawnArcher()
    {
        int ageIndex = ageValue.getAge();
        int lowerBound = ageIndex * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge
        // Utilisez les bornes pour déterminer l'index de l'unité de l'archer dans votre liste
        int archerIndex = lowerBound+2 ; // Utilisez le lowerBound pour l'index de l'archer
        GameObject archerPrefab = unitPrefabs[archerIndex];
        spawner.SpawnPlayerBoosted(archerPrefab); // Supposons que vous avez une fonction SpawnPlayer dans votre spawner
    }




// Fonction pour réinitialiser le timer lorsque l'âge change
    public void ResetSpawnTimer()
    {
        int ageIndex = ageValue.getAge(); // Obtenez l'âge actuel

        // Mettre à jour le temps écoulé pour cet âge
        lastSpawnTimes[ageIndex] = Time.timeSinceLevelLoad;
    }
}