using UnityEngine;
using UnityEngine.Tilemaps;

public class TurretPlacementOnTilemap : MonoBehaviour
{
    public Tilemap gameMap;
    public GameObject turretSlotPrefab;
    public Vector3Int castleTilePosition; // Position de la tile du château sur la Tilemap
    public Money money;

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
            Vector3 worldPosition = gameMap.CellToWorld(castleTilePosition) + new Vector3(-10.7246f, -0.2499f, 0);
            Instantiate(turretSlotPrefab, worldPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Turret slot prefab or gameMap not set!");
        }
    }

}
