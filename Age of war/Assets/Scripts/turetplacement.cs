using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TurretPlacement : MonoBehaviour
{
    public Tilemap gameMap;
    public GameObject turretSlotPrefab;
    public Vector3Int castleTilePosition; // Position de la tile du ch�teau sur la Tilemap
    public int maxTurrets = 3; // Nombre maximum de tourelles
    private int currentTurrets = 0; // Nombre actuel de tourelles plac�es
    private Vector3 lastPosition; // Derni�re position o� une tourelle a �t� plac�e
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
                int cost = GetTurretCost(currentTurrets);
                if (money.canBuy(cost))
                {
                    if (currentTurrets == 0)
                    {
                        lastPosition = gameMap.CellToWorld(castleTilePosition) + new Vector3(-10.7246f, -0.2499f, 0);
                    }
                    else
                    {
                        lastPosition += new Vector3(0, 1, 0); // D�cale chaque nouvelle tourelle d'une unit� vers le haut
                    }

                    Instantiate(turretSlotPrefab, lastPosition, Quaternion.identity);
                    currentTurrets++; // Incr�menter le nombre de tourelles plac�es
                    money.SpendGold(cost); // D�duire le co�t de la tourelle du total de l'or
                    Debug.Log("Turret slot added at position: " + lastPosition + " for " + cost + " gold.");
                }
                else
                {
                    Debug.LogWarning("Not enough gold! Needed: " + cost);
                }
            }
            else
            {
                Debug.LogWarning("Maximum turret slots reached or prefab/gameMap not set!");
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


    private int GetTurretCost(int turretIndex)
    {
        // D�finir le co�t des emplacements de tourelle
        switch (turretIndex)
        {
            case 0: return 250;
            case 1: return 500;
            case 2: return 1000;
            default: return 1000; // Retourne le co�t le plus �lev� si au-del� de l'index attendu
        }
    }

     public void DecrementTurretCount()
    {
        if (currentTurrets > 0)
        {
            currentTurrets--;
        }
    }
}
