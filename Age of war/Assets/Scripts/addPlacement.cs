using UnityEngine;

public class addPlacement : MonoBehaviour
{
    public bool isEnemySlot = false; // Indique si ce slot appartient à l'ennemi
    public Money money;  // Assurez-vous que ce champ est assigné dans l'Inspecteur Unity
    public TurretManager turretManager; // Référence au TurretManager

    void OnMouseDown()
    {
        // Empêche les interactions du joueur avec les slots ennemis
        if (isEnemySlot)
        {
            Debug.LogWarning("Cannot interact with enemy turret slots.");
            return;
        }

        if (turretManager == null)
        {
            GameObject turretManagerObj = GameObject.FindGameObjectWithTag("TurretManager");
            if (turretManagerObj != null)
            {
                turretManager = turretManagerObj.GetComponent<TurretManager>();
            }
            if (turretManager == null)
            {
                Debug.LogError("TurretManager reference is not set.");
                return;
            }
        }

        GameObject turretPrefab = turretManager.selectedTurretPrefab;  // Accède au prefab sélectionné dans TurretManager

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

        int turretCost = turretManager.GetTurretCostByAge();

        if (money.canBuy(turretCost))  // Vérifie si le joueur a suffisamment d'or
        {
            Vector3 positionWithOffset = transform.position + new Vector3(0, 0, -1);
            Instantiate(turretPrefab, positionWithOffset, Quaternion.identity, transform);
            money.SpendGold(turretCost);  // Déduit le coût de la tourelle du total d'or
            turretManager.selectedTurretPrefab = null;  // Réinitialise éventuellement après le placement
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
                Vector3 positionWithOffset = transform.position + new Vector3(0, 0, -1);
                Instantiate(turretPrefab, positionWithOffset, Quaternion.identity, transform);
                Debug.Log("Bot placed a turret.");
            }
            else
            {
                Debug.LogWarning("Slot is already occupied or turretPrefab is not set.");
            }
        }
    }
}
