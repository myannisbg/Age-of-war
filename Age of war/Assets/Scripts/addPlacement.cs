using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addPlacement : MonoBehaviour
{
    public GameObject turretPrefab; // Glisse ici le prefab de la tourelle

    void OnMouseDown()
    {
        if (turretPrefab != null && transform.childCount == 0) // Vérifie s'il n'y a pas déjà une tourelle
        {
            Instantiate(turretPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
