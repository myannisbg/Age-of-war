using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addPlacement : MonoBehaviour
{
    public GameObject turretPrefab; // Glisse ici le prefab de la tourelle

    void OnMouseDown()
    {
        if (turretPrefab != null && transform.childCount == 0) // V�rifie s'il n'y a pas d�j� une tourelle
        {
            Instantiate(turretPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
