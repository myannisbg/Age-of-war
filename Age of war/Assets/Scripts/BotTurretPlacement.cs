using UnityEngine;

public class BotTurretPlacement : MonoBehaviour
{
    public GameObject[] turretPrefabs;  // Liste des prefabs de tourelles
    public float placementInterval = 5f; // Temps entre chaque placement de tourelle
    private float placementTimer = 0f;
    private int selectedTurretIndex = 0; // Index de la tourelle sélectionnée
    public GlobalAge ageValue;  // Référence à la classe GlobalAge pour obtenir l'âge actuel
    public string botDifficulty = "medium";  // Niveau de difficulté du bot ("low", "medium", "high")

    void Update()
    {
        placementTimer += Time.deltaTime;
        if (placementTimer >= placementInterval)
        {
            SelectTurretBasedOnAgeAndDifficulty();
            PlaceTurretInNextAvailableSlot();
            placementTimer = 0f;
        }
    }

    void SelectTurretBasedOnAgeAndDifficulty()
    {
        int currentAge = ageValue.getAge();  // Obtient l'âge actuel

        int lowTurretIndex = currentAge;
        int mediumTurretIndex = currentAge + 1;
        int highTurretIndex = currentAge + 2;

        // Assurez-vous que les index ne dépassent pas la taille du tableau de prefabs
        lowTurretIndex = Mathf.Clamp(lowTurretIndex, 0, turretPrefabs.Length - 1);
        mediumTurretIndex = Mathf.Clamp(mediumTurretIndex, 0, turretPrefabs.Length - 1);
        highTurretIndex = Mathf.Clamp(highTurretIndex, 0, turretPrefabs.Length - 1);

        switch (botDifficulty.ToLower())
        {
            case "low":
                selectedTurretIndex = lowTurretIndex;
                break;
            case "medium":
                selectedTurretIndex = mediumTurretIndex;
                break;
            case "high":
                selectedTurretIndex = highTurretIndex;
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
}
