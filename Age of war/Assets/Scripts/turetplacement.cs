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
    private int currentTurretsAlly = 0; // Nombre actuel de tourelles placées
    private int currentTurretsEnnemy = 0;
    private Vector3 lastPosition; // Dernière position où une tourelle a été placée
    public Money money; // Supposons que tu aies une gestion d'argent
    public GameObject turretSlotPrefabEnnemy;


    public void spawnArea()
    {
        //tout ce qui est ici est déstiné aux bots / autre joueurs

        for (int i = 0; i < maxTurrets; i++)
            {
                if (currentTurretsEnnemy == 0)
                {
                    lastPosition = gameMap.CellToWorld(castleTilePosition) + new Vector3(27f, -0.2499f, -1);
                }
                else
                {
                    lastPosition += new Vector3(0, 2, -1); // Décale chaque nouvelle tourelle d'une unité vers le haut
                }

                GameObject slot = Instantiate(turretSlotPrefabEnnemy, lastPosition, Quaternion.identity);
                addPlacement slotComponent = slot.GetComponent<addPlacement>();
                if (slotComponent != null)
                {
                    slotComponent.isEnemySlot = true; // Définit le slot comme ennemi
                }
                currentTurretsEnnemy++;
            }
    }


    public void AddTurretSlotOnCastle()
    {
        Debug.Log("Add Turret Slot button clicked.");
        if (turretSlotPrefab != null && gameMap != null)
        {
            if (currentTurretsAlly < maxTurrets)
            {
                int cost = GetTurretCost(currentTurretsAlly);
                if (money.canBuy(cost))
                {
                    if (currentTurretsAlly == 0)
                    {
                        lastPosition = gameMap.CellToWorld(castleTilePosition) + new Vector3(-10.7246f, -0.2499f, -1);
                    }
                    else
                    {
                        lastPosition += new Vector3(0, 2, -1); // Décale chaque nouvelle tourelle d'une unité vers le haut
                    }


                    
                    
                    GameObject slot = Instantiate(turretSlotPrefab, lastPosition, Quaternion.identity);
                    money.SpendGold(cost); // Déduire le coût de la tourelle du total de l'or
                    addPlacement slotComponent = slot.GetComponent<addPlacement>();
                    Debug.Log("Turret slot added at position: " + lastPosition + " for " + cost + " gold.");
                    if (slotComponent != null)
                    {
                        slotComponent.isEnemySlot = false; // Définit le slot comme allié
                        currentTurretsAlly++; // Incrémenter le nombre de tourelles placées
                    }
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

    public int GetTurretCost(int turretIndex)
    {
        // Définir le coût des emplacements de tourelle
        switch (turretIndex)
        {
            case 0: return 250;
            case 1: return 500;
            case 2: return 1000;
            default: return 1000; // Retourne le coût le plus élevé si au-delà de l'index attendu
        }
    }

    public void DecrementTurretCount()
    {
        if (currentTurretsAlly > 0)
        {
            currentTurretsAlly--;
        }
    }

    public void DecrementTurretCountEnnemy()
    {
        if (currentTurretsEnnemy > 0)
        {
            currentTurretsEnnemy--;
        }
    }

    // Nouvelle méthode pour obtenir le coût d'une tourelle à une position donnée
    public int GetTurretCost(GameObject turret)
    {
        // Vous pouvez ajouter une logique pour déterminer le coût de la tourelle basée sur sa position ou d'autres critères
        return GetTurretCost(currentTurretsAlly - 1);
    }
}
