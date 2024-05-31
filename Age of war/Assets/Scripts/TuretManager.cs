using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public static TurretManager Instance { get; private set; }
    public GameObject[] turretPrefabs;  // Array of turret prefabs
    public GameObject selectedTurretPrefab;  // Currently selected turret prefab, public for access
    public int cost;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public void SetCost(int turretCost)
    {
        cost = turretCost;
    }

    public void SelectTurret(int index)
    {
        if (index >= 0 && index < turretPrefabs.Length)
        {
            selectedTurretPrefab = turretPrefabs[index];
            Debug.Log("Turret selected: " + selectedTurretPrefab.name);
        }
        else
        {
            Debug.LogError("Index out of range: Failed to select turret.");
        }
    }

    // New method to get cost based on turret index
    public int GetTurretCost()
    {
        int index = System.Array.IndexOf(turretPrefabs, selectedTurretPrefab);
        switch (index)
        {
            case 0:
                return 500;  // Cost for the first turret
            case 1:
                return 750;  // Cost for the second turret
            default:
                return 1000;  // Default cost for any other turrets
        }
    }
}

