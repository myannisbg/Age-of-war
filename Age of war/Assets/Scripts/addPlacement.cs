using UnityEngine;

public class addPlacement : MonoBehaviour
{
    public bool isEnemySlot = false; // Indique si ce slot appartient à l'ennemi
    public Money money;  // Assurez-vous que ce champ est assigné dans l'Inspecteur Unity
    private TurretManager turretManager;

    private void Start()
    {
        turretManager = FindObjectOfType<TurretManager>();
        if (turretManager == null)
        {
            Debug.LogError("TurretManager not found in the scene.");
        }
    }

    void OnMouseDown()
    {
        if (isEnemySlot)
        {
            Debug.LogWarning("Cannot interact with enemy turret slots.");
            return;
        }

        if (turretManager == null)
        {
            Debug.LogError("TurretManager reference is not set.");
            return;
        }

        GameObject turretPrefab = turretManager.selectedTurretPrefab;

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

        int turretCost = turretManager.GetTurretCostByAge();

        if (money.canBuy(turretCost))
        {
            Instantiate(turretPrefab, transform.position, Quaternion.identity, transform);
            money.SpendGold(turretCost);
            turretManager.selectedTurretPrefab = null;
            Debug.Log("Turret placed, gold spent: " + turretCost);
        }
        else
        {
            Debug.LogWarning("Not enough gold! Needed: " + turretCost);
        }
    }

    public void PlaceTurretForBot(GameObject turretPrefab)
    {
        if (isEnemySlot)
        {
            if (turretPrefab != null && transform.childCount == 0)
            {
                Instantiate(turretPrefab, transform.position, Quaternion.identity, transform);
                Debug.Log("Bot placed a turret.");
            }
            else
            {
                Debug.LogWarning("Slot is already occupied or turretPrefab is not set.");
            }
        }
    }
}
