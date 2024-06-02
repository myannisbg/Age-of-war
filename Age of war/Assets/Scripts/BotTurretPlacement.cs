using UnityEngine;
using System.Linq; 

public class BotTurretPlacement : MonoBehaviour
{
    public GameObject[] turretPrefabs;  // Liste des prefabs de tourelles
    private float placementInterval ; // Temps entre chaque placement de tourelle
    private float placementTimer = 0f;
    private int selectedTurretIndex = 0; // Index de la tourelle sélectionnée
    public GlobalAge ageValue;  // Référence à la classe GlobalAge pour obtenir l'âge actuel
    public Bot bot;
    private int currentAge = 1;


    void Start()
    {
        if (bot != null)
        {
            bot.StartIncrementing();  // Démarre l'incrémentation de l'âge dans le script Bot
        }
    }


    void Update()
    {
        placementTimer += Time.deltaTime;
        if (placementTimer >= placementInterval)
        {
            SelectTurretBasedOnAgeAndDifficulty();
            PlaceTurretInNextAvailableSlot();
            UpdateAgeAndRemoveOldTurrets();
            placementTimer = 0f;
        }
    }

    void SelectTurretBasedOnAgeAndDifficulty()
    {
        int currentAge = bot.ageValue;  // Obtient l'âge actuel
        print(currentAge);
        int difficulty = bot.returnDifficulty();

        int lowTurretIndex = currentAge *3;
        int mediumTurretIndex = currentAge *3 + 1;
        int highTurretIndex = currentAge *3 + 2;

        // Assurez-vous que les index ne dépassent pas la taille du tableau de prefabs
        lowTurretIndex = Mathf.Clamp(lowTurretIndex, 0, turretPrefabs.Length - 1);
        mediumTurretIndex = Mathf.Clamp(mediumTurretIndex, 0, turretPrefabs.Length - 1);
        highTurretIndex = Mathf.Clamp(highTurretIndex, 0, turretPrefabs.Length - 1);

       switch (difficulty)
        {
            case 0:  // Low difficulty
                selectedTurretIndex = lowTurretIndex;
                placementInterval=30f;
                break;
            case 1:  // Medium difficulty
                selectedTurretIndex = mediumTurretIndex;
                placementInterval=25f;
                break;
            case 2:  // High difficulty (case 2 and above)
            case 3: 
                selectedTurretIndex = highTurretIndex;
                placementInterval=15f;
                break;
            default:
                Debug.LogWarning("Unknown bot difficulty level. Defaulting to medium.");
                selectedTurretIndex = mediumTurretIndex;
                break;
        }


        Debug.Log("Selected turret index based on age and difficulty: " + selectedTurretIndex);
    }

    void PlaceTurretInNextAvailableSlot()
    {
        GameObject turretPrefab = GetSelectedTurret();
        if (turretPrefab == null)
        {
            Debug.LogWarning("No turret prefab selected.");
            return;
        }

        addPlacement[] slots = FindObjectsOfType<addPlacement>();
        foreach (addPlacement slot in slots)
        {
            if (slot.isEnemySlot && slot.transform.childCount == 0)
            {
                slot.PlaceTurretForBot(turretPrefab);
                break;
            }
        }
    }

    GameObject GetSelectedTurret()
    {
        if (selectedTurretIndex >= 0 && selectedTurretIndex < turretPrefabs.Length)
        {
            return turretPrefabs[selectedTurretIndex];
        }
        else
        {
            Debug.LogWarning("Selected turret index is out of range.");
            return null;
        }
    }

    // Méthode pour changer la tourelle sélectionnée
    public void SetSelectedTurret(int index)
    {
        if (index >= 0 && index < turretPrefabs.Length)
        {
            selectedTurretIndex = index;
            Debug.Log("Selected turret changed to index: " + index);
        }
        else
        {
            Debug.LogWarning("Invalid turret index: " + index);
        }
    }


public void RemoveTurretsWithSpecificTag(string tag)
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject turret in turrets)
        {
            Destroy(turret);
            Debug.Log("Removed turret with tag: " + tag + " at position: " + turret.transform.position);
        }
    }



    void UpdateAgeAndRemoveOldTurrets()
    {
        int newAge = bot.ageValue;

        if (newAge != currentAge)
        {
            RemoveTurretsWithSpecificTag("TurretEnnemy");  // Remplacez "TurretEnnemy" par le tag réel de vos tourelles
            currentAge = newAge;
        }
    }

}


