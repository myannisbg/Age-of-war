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
    public int ageValue =0;
    public float initialInterval = 100.0f;
    private float currentInterval;
    private Coroutine ageCoroutine;

    private int tankCount =  0;
    private int archerCount =  0;
    private bool scenarioSpecific = true;
    private int countOfUnits=0;
    
    
    


private Dictionary<int, float> lastSpawnTimes = new Dictionary<int, float>();


    void Start()
    {
        
        UpdateBotFunction();
        SpawnUnits();
        currentInterval = initialInterval;
        string unitTag = gameObject.tag;
        

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
            if (returnDifficulty()==3)
            {
                if (Time.timeSinceLevelLoad - lastModeChangeTime >= modeDuration)
                {
                    ToggleMode();
                    lastModeChangeTime = Time.timeSinceLevelLoad;
                }

                // Spawner une unité à intervalles réguliers
                if (Time.timeSinceLevelLoad - lastSpawnTime >= spawnInterval)
                {
                    if (currentMode == Mode.Offense)
                    {
                        SpawnNextInOffenseSequence();
                    }
                    else
                    {
                        SpawnNextInDefenseSequence();
                    }
                    lastSpawnTime = Time.timeSinceLevelLoad;
                }
            }
    }

    public int returnDifficulty(){

        int difficulty = PlayerPrefs.GetInt("Difficulty",0);
          return difficulty;
    }


     public void StartIncrementing()
    {
        if (ageCoroutine != null)
        {
            StopCoroutine(ageCoroutine);
        }
        ageCoroutine = StartCoroutine(IncrementAge());
    }

        IEnumerator IncrementAge()
    {
        while (ageValue<6)
        {
            yield return new WaitForSeconds(currentInterval);
            ageValue++;
            // Debug.Log("Age value incremented to: " + ageValue);
            UpdateBotFunction();
        }
    }



    void UpdateBotFunction()
    {
         int difficulty = PlayerPrefs.GetInt("Difficulty",0);

        switch (difficulty)
        {
            case 0:
                currentFunction = BotFunction.BotIsDumb;
                currentInterval = initialInterval;
                break;
            case 1:
                currentFunction = BotFunction.BotIsTrying;
                currentInterval = initialInterval - 30.0f; 
                break;
            case 2:
                currentFunction = BotFunction.BotIsPlaying;
                currentInterval = initialInterval - 50.0f;
                break;
            case 3:
                currentFunction = BotFunction.BotIsTooStrong;
                currentInterval = initialInterval - 65.0f;
                break;
            default:
                Debug.LogWarning("Unknown difficulty setting: " + difficulty);
                break;
        }

        ApplyBotFunction();
    }

    void ApplyBotFunction()
    {
        switch (currentFunction)
        {
            case BotFunction.BotIsDumb:
                // Logic for dumb bot
                Debug.Log("Bot is set to Dumb");
                break;
            case BotFunction.BotIsTrying:
                // Logic for trying bot
                Debug.Log("Bot is set to Trying");
                break;
            case BotFunction.BotIsPlaying:
                // Logic for playing bot
                Debug.Log("Bot is set to Playing");
                break;
            case BotFunction.BotIsTooStrong:
                // Logic for too strong bot
                Debug.Log("Bot is set to Too Strong");
                break;
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
             int lowerBound = ageValue * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge
            // Vérifier si l'âge est valide
            if (ageValue >= 0 && upperBound < unitPrefabs.Count)
            {
                // Générer un nombre aléatoire dans la plage de l'âge actuel
                int randomIndex = Random.Range(lowerBound, upperBound);

                // Récupérer la préfab d'unité correspondant au nombre aléatoire
                GameObject unitPrefab = unitPrefabs[randomIndex];
               

                // Appeler la méthode SpawnPlayer du PlayerSpawner en passant la préfab d'unité
                spawner.SpawnPlayer(unitPrefab);
                // print(unitPrefab);
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
        int ageIndex = ageValue;
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
        int ageIndex = ageValue;
        int lowerBound = ageIndex * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge
        
        


        // Vérifier si l'âge est valide
        if (ageIndex >= 0 && upperBound < unitPrefabs.Count)
        {
            // Générer un nombre aléatoire dans la plage de l'âge actuel
            int randomIndex = Random.Range(lowerBound, upperBound + 1);

            if (scenarioSpecific==false){
            // Si le temps requis est écoulé pour l'unité du upperBound, ou si l'index correspond à une unité précédente, alors procéder au spawn
                    if (Time.timeSinceLevelLoad - lastSpawnTimes[ageIndex] >= 20 || randomIndex != upperBound)
                    {
                        // Si le temps requis est écoulé pour l'unité du upperBound, réinitialiser le temps écoulé pour cet âge
                        if (randomIndex == upperBound && Time.timeSinceLevelLoad - lastSpawnTimes[ageIndex] >= 20)
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
public enum Mode
{
    Offense,
    Defense
}

private Mode currentMode = Mode.Offense; // Mode initial
private float lastModeChangeTime;
private float modeDuration = 60f; // Durée de chaque mode en secondes
private float lastSpawnTime;
private float spawnInterval = 5f; // Intervalle de temps entre chaque spawn

private int offenseIndex = 0;
private int defenseIndex = 0;



private string[] offenseSequence = { "Infanterie", "Archer", "Tank", "Archer", "AntiTank", "Archer" };
private string[] defenseSequence = { "AntiTank", "Tank", "Archer" };
private int maxAllies = 10; // Nombre maximum d'unités alliées

private bool scenarioSpecific2 = false;


  void BotIsTooStrong()
{
    // Vérifier si le script PlayerSpawner est assigné
    if (spawner != null)
    {
        // Définir les bornes des tranches d'âge
        int ageIndex = ageValue;
        int lowerBound = ageIndex * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge

        // Vérifier si l'âge est valide
        if (ageIndex >= 0 && upperBound < unitPrefabs.Count)
        {
            // Vérifier si le temps de changement de scénario est écoulé
            if (Time.timeSinceLevelLoad - lastModeChangeTime >= modeDuration)
            {
                scenarioSpecific2 = !scenarioSpecific2; // Alterner le scénario
                lastModeChangeTime = Time.timeSinceLevelLoad;
            }

            if (!scenarioSpecific2)
            {
                // Vérifier si le temps requis est écoulé pour l'unité du upperBound
                if (Time.timeSinceLevelLoad - lastSpawnTimes.GetValueOrDefault(ageIndex, 0f) >= 20)
                {
                    lastSpawnTimes[ageIndex] = Time.timeSinceLevelLoad;
                    GameObject unitPrefab = unitPrefabs[upperBound];
                    spawner.SpawnPlayerBoosted(unitPrefab);
                }
            }
            
        }
        else
        {
            Debug.LogError("Age index is out of range!");
        }

        // Changer de mode après une certaine durée
        if (Time.timeSinceLevelLoad - lastModeChangeTime >= modeDuration)
        {
            ToggleMode();
            lastModeChangeTime = Time.timeSinceLevelLoad;
        }

        // Gérer les séquences en fonction du mode actuel
        if (currentMode == Mode.Offense)
        {
            if (Time.timeSinceLevelLoad - lastSpawnTime >= spawnInterval)
            {
                SpawnNextInOffenseSequence();
                lastSpawnTime = Time.timeSinceLevelLoad;
            }
        }
        else if (currentMode == Mode.Defense)
        {
            if (Time.timeSinceLevelLoad - lastSpawnTime >= spawnInterval)
            {
                SpawnNextInDefenseSequence();
                lastSpawnTime = Time.timeSinceLevelLoad;
            }
        }
    }
    else
    {
        Debug.LogError("PlayerSpawner script is not assigned!");
    }
}


private void SpawnNextInOffenseSequence()
{
    if (CountUnitsWithTag("Ally") >= maxAllies) return;

    string unitType = offenseSequence[offenseIndex];
    offenseIndex = (offenseIndex + 1) % offenseSequence.Length;

    SpawnUnitByType(unitType);
}

private void SpawnNextInDefenseSequence()
{
    if (CountUnitsWithTag("Ally") >= maxAllies) return;

    string unitType = defenseSequence[defenseIndex];
    defenseIndex = (defenseIndex + 1) % defenseSequence.Length;

    SpawnUnitByType(unitType);
}

private void SpawnUnitByType(string unitType)
{
    int ageIndex = ageValue;
    int lowerBound = ageIndex * 4;

    GameObject unitPrefab = null;
    switch (unitType)
    {
        case "Infanterie":
            unitPrefab = unitPrefabs[lowerBound];
            break;
        case "Tank":
            unitPrefab = unitPrefabs[lowerBound + 2];
            break;
        case "Archer":
            unitPrefab = unitPrefabs[lowerBound + 1];
            break;
        case "AntiTank":
            unitPrefab = unitPrefabs[lowerBound + 3];
            break;
    }

    if (unitPrefab != null && spawner != null)
    {
        spawner.SpawnPlayerBoosted(unitPrefab);
    }
}

private int CountUnitsWithTag(string tag)
{
    return GameObject.FindGameObjectsWithTag(tag).Length;
}

private void ToggleMode()
{
    currentMode = currentMode == Mode.Offense ? Mode.Defense : Mode.Offense;
}



    // Fonction pour faire apparaître un tank
    void SpawnTank()
    {
        int ageIndex = ageValue;
        int lowerBound = ageIndex * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge
        // Utilisez les bornes pour déterminer l'index de l'unité de tank dans votre liste
        int tankIndex = lowerBound+1; // Utilisez le lowerBound pour l'index du tank
        GameObject tankPrefab = unitPrefabs[tankIndex];
        spawner.SpawnPlayerBoosted(tankPrefab); 
    }

    // Fonction pour faire apparaître un archer
    void SpawnArcher()
    {
        int ageIndex = ageValue;
        int lowerBound = ageIndex * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge
        // Utilisez les bornes pour déterminer l'index de l'unité de l'archer dans votre liste
        int archerIndex = lowerBound+2 ; // Utilisez le lowerBound pour l'index de l'archer
        GameObject archerPrefab = unitPrefabs[archerIndex];
        spawner.SpawnPlayerBoosted(archerPrefab);
    }

    void SpawnInfanterie()
    {
        int ageIndex = ageValue;
        int lowerBound = ageIndex * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge
        // Utilisez les bornes pour déterminer l'index de l'unité de tank dans votre liste
        int infanterieIndex = lowerBound; // Utilisez le lowerBound pour l'index de l'infanterie
        GameObject infanteriePrefab = unitPrefabs[infanterieIndex];
        spawner.SpawnPlayerBoosted(infanteriePrefab); 
    }
    void SpawnAntiTank()
    {
        int ageIndex = ageValue;
        int lowerBound = ageIndex * 4; // Born inférieure de la tranche d'âge
        int upperBound = lowerBound + 3; // Born supérieure de la tranche d'âge
        // Utilisez les bornes pour déterminer l'index de l'unité de tank dans votre liste
        int antiArmorIndex = lowerBound+4; // Utilisez le lowerBound pour l'index de l'anti-armure
        GameObject antiArmorPrefab = unitPrefabs[antiArmorIndex];
        spawner.SpawnPlayerBoosted(antiArmorPrefab); 
    }




// Fonction pour réinitialiser le timer lorsque l'âge change
    public void ResetSpawnTimer()
    {
        int ageIndex = ageValue; // Obtenez l'âge actuel

        // Mettre à jour le temps écoulé pour cet âge
        lastSpawnTimes[ageIndex] = Time.timeSinceLevelLoad;
    }


}
