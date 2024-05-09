using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();

    private void Start()
    {
        // Obtenez toutes les instances de la classe Unit dans la scène et ajoutez-les à la liste
        Unit[] unitArray = FindObjectsOfType<Unit>();
        units.AddRange(unitArray);
    }

    private void OnApplicationQuit()
    {
        // Convertissez la liste de Unit en une liste de GameObject
        List<GameObject> unitPrefabs = units.ConvertAll(unit => unit.gameObject);

        // Appelez la méthode de réinitialisation lors de la fermeture de l'application
        StatReset.ResetStats(unitPrefabs);
         Debug.Log("Resetting initial values on quit");
    }

    // private void OnApplicationPause(bool pauseStatus)
    // {
    //     // Convertissez la liste de Unit en une liste de GameObject
    //     List<GameObject> unitPrefabs = units.ConvertAll(unit => unit.gameObject);

    //     // Appelez la méthode de réinitialisation lors de la mise en pause de l'application
    //     StatReset.ResetInitialValues(unitPrefabs);
    //      Debug.Log("Resetting initial values on pause");
    // }
}
