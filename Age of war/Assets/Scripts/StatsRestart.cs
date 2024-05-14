using UnityEngine;
using System.Collections.Generic;


public static class StatReset 
{
    public static void ResetStats(List<GameObject> unitPrefabs)
    {
        foreach (GameObject prefab in unitPrefabs)
        {
            Unit.ResetInitialValues(prefab);
            
        }
    }
}
