using UnityEngine;

public class addPlacement : MonoBehaviour
{
    public Money money;  // Ensure this field is assigned in the Unity Inspector

    void OnMouseDown()
    {
        GameObject turretPrefab = TurretManager.Instance.selectedTurretPrefab;  // Access the selected prefab from TurretManager

        GameObject eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
        if (eventSystem != null)
        {
            Money moneyComponent = eventSystem.GetComponent<Money>();

            if (turretPrefab != null && transform.childCount == 0)  // Check if there is already a turret
            {
                int turretCost = TurretManager.Instance.GetTurretCostByAge();  // Updated method call
                if (moneyComponent.canBuy(turretCost))  // Check if the player has enough gold
                {
                    Instantiate(turretPrefab, transform.position, Quaternion.identity, transform);
                    moneyComponent.SpendGold(turretCost);  // Deduct the cost of the turret from the total gold
                    TurretManager.Instance.selectedTurretPrefab = null;  // Optionally reset after placing
                    Debug.Log("Turret placed, gold spent: " + turretCost);
                }
                else
                {
                    Debug.LogWarning("Not enough gold! Needed: " + turretCost);
                }
            }
        }
    }
}
