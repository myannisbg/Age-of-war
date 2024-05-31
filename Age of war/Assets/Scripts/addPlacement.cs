using UnityEngine;

public class addPlacement : MonoBehaviour
{
    private Money money;

    void OnMouseDown()
    {
        if (TurretManager.Instance == null)
        {
            Debug.LogError("TurretManager.Instance is null.");
            return;
        }

        GameObject turretPrefab = TurretManager.Instance.selectedTurretPrefab;  // Access the selected prefab from TurretManager

        if (turretPrefab == null)
        {
            Debug.LogWarning("No turret selected.");
            return;
        }

        if (transform.childCount != 0)
        {
            Debug.LogWarning("There is already a turret in this slot.");
            return;
        }

        if (money == null)
        {
            // Try to find the Money component in the scene
            GameObject eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
            if (eventSystem != null)
            {
                money = eventSystem.GetComponent<Money>();
                if (money == null)
                {
                    Debug.LogError("Money component not found on the EventSystem.");
                    return;
                }
            }
            else
            {
                Debug.LogError("EventSystem with tag 'EventSystem' not found.");
                return;
            }
        }

        int turretCost = TurretManager.Instance.GetTurretCostByAge();

        if (money.canBuy(turretCost))  // Check if the player has enough gold
        {
            Instantiate(turretPrefab, transform.position, Quaternion.identity, transform);
            money.SpendGold(turretCost);  // Deduct the cost of the turret from the total gold
            TurretManager.Instance.selectedTurretPrefab = null;  // Optionally reset after placing
            Debug.Log("Turret placed, gold spent: " + turretCost);
        }
        else
        {
            Debug.LogWarning("Not enough gold! Needed: " + turretCost);
        }
    }


    
}
