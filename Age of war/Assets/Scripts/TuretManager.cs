using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public List<GameObject> turretPrefabs;  // List of turret prefabs
    public GameObject selectedTurretPrefab;  // Currently selected turret prefab
    public GlobalAge ageValue;
    public List<int> priceOfTurret; // List of turret prices

    public void SelectTurretByAge()
    {
        int ageIndex = ageValue.getAge();
        if (ageIndex >= 0 && ageIndex < turretPrefabs.Count)
        {
            selectedTurretPrefab = turretPrefabs[ageIndex];
            Debug.Log("Turret selected: " + selectedTurretPrefab.name);
        }
        else
        {
            Debug.LogError("Invalid age index: Failed to select turret.");
        }
    }

    public int GetTurretCostByAge()
    {
        int ageIndex = ageValue.getAge();
        if (ageIndex >= 0 && ageIndex < priceOfTurret.Count)
        {
            return priceOfTurret[ageIndex];
        }
        else
        {
            Debug.LogError("Invalid age index: Failed to get turret cost.");
            return -1;  // Indicate an error in cost retrieval
        }
    }
}
