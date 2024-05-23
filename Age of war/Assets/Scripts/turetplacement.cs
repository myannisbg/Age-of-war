using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TurretPlacement : MonoBehaviour
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
        if (turretSlotPrefab != null && gameMap != null)
        {
            if (currentTurrets < maxTurrets)
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
                Debug.Log("Turret slot added at position: " + lastPosition);
            }
            else
            {
                Debug.LogWarning("Maximum turret slots reached!");
            }
        }
        else
        {
            if (turretSlotPrefab == null)
            {
                Debug.LogError("Turret slot prefab not set!");
            }
            if (gameMap == null)
            {
                Debug.LogError("Game map not set!");
            }
        }
    }
}
