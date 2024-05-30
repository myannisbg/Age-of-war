using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpawnButton : MonoBehaviour
{
    public List<GameObject> unitPrefabs; // Liste des préfabs d'unités
    public PlayerSpawner playerSpawner; // Référence au script PlayerSpawner
    public GlobalAge ageValue;
    public Money money;
    public List<int> priceOfUnit;
    
    
    void Start()
    {
        // Récupérer la référence au script PlayerSpawner s'il n'est pas attribué
        if (playerSpawner == null)
        {
            playerSpawner = FindObjectOfType<PlayerSpawner>();
        }

        // S'assurer que le bouton possède un composant Button
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // Ajouter un écouteur d'événement pour le clic sur le bouton
            button.onClick.AddListener(SpawnUnitByAge);
        }
    }



    void SpawnUnitByAge()
    {
        // Vérifier si le script PlayerSpawner est assigné
        if (playerSpawner != null)
        {
            // Vérifier si l'index d'âge est valide
            if (money.canBuy(priceOfUnit[ageValue.getAge()])){
                money.addGold(-priceOfUnit[ageValue.getAge()]);
            
                if (ageValue.getAge() >= 0 && ageValue.getAge() < unitPrefabs.Count)
                {
                    // Récupérer la préfab d'unité correspondant à l'âge actuel
                    GameObject unitPrefab = unitPrefabs[ageValue.getAge()];
                    
                    // Appeler la méthode SpawnPlayer du PlayerSpawner en passant la préfab d'unité
                    playerSpawner.SpawnPlayer(unitPrefab);
                }
                else
                {
                    Debug.LogError("Invalid age index or unit prefab list is empty!");
                }
            }
        }
        else
        {
            Debug.LogError("PlayerSpawner script is not assigned!");
        }
    }

    public int getPrice(){
        return priceOfUnit[ageValue.getAge()];
    }
}
