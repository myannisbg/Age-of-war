using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class turetPlacement : MonoBehaviour
{
    public Tilemap gameMap;
    public GameObject turretSlotPrefab;
    public Vector3Int castleTilePosition; // Position de la tile du château sur la Tilemap
    public int maxTurrets = 3; // Nombre maximum de tourelles
    private int currentTurrets = 0; // Nombre actuel de tourelles placées
    private Vector3 lastPosition; // Dernière position où une tourelle a été placée
    public Money money; // Supposons que tu aies une gestion d'argent

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddTurretSlotOnCastle();
        }
    }

    public void AddTurretSlotOnCastle()
    {
        Debug.Log("Add Turret Slot button clicked.");
        if (turretSlotPrefab != null && gameMap != null && currentTurrets < maxTurrets)
        {
            if (currentTurrets == 0)
            {
                lastPosition = gameMap.CellToWorld(castleTilePosition) + new Vector3(-10.7246f, -0.2499f, 0);
            }
            else
            {
                lastPosition += new Vector3(0, 1, 0); // Décale chaque nouvelle tourelle d'une unité vers le haut
            }

            Instantiate(turretSlotPrefab, lastPosition, Quaternion.identity);
            currentTurrets++; // Incrémenter le nombre de tourelles placées
        }
        else if (currentTurrets >= maxTurrets)
        {
            Debug.LogError("Maximum turret slots reached!");
        }
        else
        {
            Debug.LogError("Turret slot prefab or gameMap not set!");
        }
    }
}
