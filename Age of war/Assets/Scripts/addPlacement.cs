using UnityEngine;

public class addPlacement : MonoBehaviour
{
    public bool isEnemySlot = false; // Indique si ce slot appartient à l'ennemi
    public Money money;  // Assurez-vous que ce champ est assigné dans l'Inspecteur Unity

    void OnMouseDown()
    {
        // Empêche les interactions du joueur avec les slots ennemis
        if (isEnemySlot)
        {
            Debug.LogWarning("Cannot interact with enemy turret slots.");
            return;
        }

        if (TurretManager.Instance == null)
        {
            Debug.LogError("TurretManager.Instance is null.");
            return;
        }

        GameObject turretPrefab = TurretManager.Instance.selectedTurretPrefab;  // Accède au prefab sélectionné dans TurretManager

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

        // Trouve le composant Money s'il n'est pas déjà assigné
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

        int turretCost = TurretManager.Instance.GetTurretCostByAge();

        if (money.canBuy(turretCost))  // Vérifie si le joueur a suffisamment d'or
        {
            Instantiate(turretPrefab, transform.position, Quaternion.identity, transform);
            money.SpendGold(turretCost);  // Déduit le coût de la tourelle du total d'or
            TurretManager.Instance.selectedTurretPrefab = null;  // Réinitialise éventuellement après le placement
            Debug.Log("Turret placed, gold spent: " + turretCost);
        }
        else
        {
            Debug.LogWarning("Not enough gold! Needed: " + turretCost);
        }
    }

    // Méthode pour que le bot puisse poser une tourelle
     public void PlaceTurretForBot(GameObject turretPrefab)
    {
        if (isEnemySlot)
        {
            if (turretPrefab != null && transform.childCount == 0) // Vérifie s'il n'y a pas déjà une tourelle
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

