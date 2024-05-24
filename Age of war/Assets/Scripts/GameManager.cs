using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();

    private void Start()
    {
        // Obtenez toutes les instances de la classe Unit dans la scène et ajoutez-les à la liste
        Unit[] allUnits = FindObjectsOfType<Unit>();

        // Parcourir toutes les unités trouvées
        foreach (Unit unit in allUnits)
        {
            // Vérifiez si l'unité appartient à la liste de suivi ou si elle a un tag spécifique
            
                // Ajoutez l'unité à la liste de suivi
                units.Add(unit);
            
        }
    }

    public void call()
    {
        
    }

    private void OnApplicationQuit()
    {
        // Vérifiez si au moins une unité a été initialisée
        if (Unit.UnitsSpawned)
        {
            // Convertissez la liste de Unit en une liste de GameObject
            List<GameObject> unitPrefabs = units.ConvertAll(unit => unit.gameObject);
            
            // Appelez la méthode de réinitialisation des statistiques uniquement si des unités ont été initialisées
            StatReset.ResetStats(unitPrefabs);
            // Debug.Log("Resetting initial values on quit");
        }
        // else
        // {
        //     Debug.Log("No Ally units spawned, skipping stat reset");
        // }
    }
}




