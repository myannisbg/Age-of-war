using UnityEngine;
using UnityEngine.UI;

public class ButtonTurret : MonoBehaviour
{
    public int turretIndex;  // Index of the turret this button should select
    public GlobalAge ageValue; 
    private TurretManager turretManager;

    private void Start()
    {
        turretManager = FindObjectOfType<TurretManager>();
        if (turretManager == null)
        {
            Debug.LogError("TurretManager not found in the scene.");
            return;
        }

        Button button = GetComponent<Button>();
        button.onClick.AddListener(SelectTurret);
    }

       private void SelectTurret()
    {
        if (turretManager != null && turretIndex >= 0 && turretIndex < turretManager.turretPrefabs.Count)
        {
            int age = ageValue.getAge();
            int prefabIndex = age * 3 + turretIndex; // Assuming 2 turrets per age group

            if (prefabIndex >= 0 && prefabIndex < turretManager.turretPrefabs.Count)
            {
                turretManager.selectedTurretPrefab = turretManager.turretPrefabs[prefabIndex];
                Debug.Log("Selected turret prefab: " + turretManager.selectedTurretPrefab.name);
            }
            else
            {
                Debug.LogError("Invalid prefab index: " + prefabIndex);
            }
        }
        else
        {
            Debug.LogError("Invalid turret index or TurretManager is null.");
        }
    }
}
