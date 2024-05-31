using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTurret : MonoBehaviour
{
    private bool isDeletionMode = false;
    private Camera mainCamera;
    public Money money; // Référence à la classe Money
    public float sellPercentage = 0.5f; // Pourcentage de remboursement lors de la vente
    public float debugCircleRadius = 0.5f; // Rayon du cercle de débogage
    public Color debugCircleColor = Color.red; // Couleur du cercle de débogage

    private TurretPlacement turretPlacement; // Référence au script TurretPlacement

void Start()
    {
        mainCamera = Camera.main; // Assurez-vous que mainCamera est correctement référencée
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Make sure your camera has the 'MainCamera' tag.");
        }

        turretPlacement = FindObjectOfType<TurretPlacement>(); // Obtenez la référence à TurretPlacement
        if (turretPlacement == null)
        {
            Debug.LogError("TurretPlacement script not found in the scene.");
        }
    }

    public void ToggleDeletionMode()
    {
        isDeletionMode = !isDeletionMode;
        Debug.Log("Mode suppression: " + (isDeletionMode ? "activé" : "désactivé"));
    }

    private void CheckForObjectDeletion()
    {
        // Obtenez la position de la souris en coordonnées du monde
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseWorldPosition2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

        // Affichez un cercle à la position de la souris pour le débogage
        DrawDebugCircle(mouseWorldPosition2D, debugCircleRadius, debugCircleColor);

        // Lance un rayon depuis la position de la souris
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition2D, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Object hit: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Turret")) // Vérifie que le rayon touche une tourelle
            {
                GameObject turret = hit.collider.gameObject;

                int turretCost = turretPlacement.GetTurretCost(turret);
                int sellValue = Mathf.RoundToInt(turretCost * sellPercentage);
                money.addGold(sellValue);
                Destroy(turret);

                Debug.Log("Turret sold for " + sellValue + " gold.");

                // Désactive le mode suppression
                isDeletionMode = false;
                Debug.Log("Mode suppression désactivé après la suppression de la tourelle.");

                // Informer TurretPlacement que la tourelle a été supprimée
                turretPlacement.DecrementTurretCount();
            }
            else
            {
                Debug.Log("Hit object is not a turret.");
            }
        }
        else
        {
            Debug.Log("No object hit at position: " + mouseWorldPosition2D);
        }
    }

    void Update()
    {
        if (isDeletionMode && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tentative de suppression en mode suppression.");
            CheckForObjectDeletion();
        }
    }

    // Fonction pour dessiner un cercle de débogage
    private void DrawDebugCircle(Vector2 position, float radius, Color color)
    {
        int segments = 360;
        LineRenderer lineRenderer = new GameObject("DebugCircle").AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = segments + 1;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        float angle = 20f;

        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += (360f / segments);
        }

        lineRenderer.transform.position = position;
        Destroy(lineRenderer.gameObject, 0.5f); // Détruire le cercle après 0.5 secondes pour ne pas encombrer la scène
    }
}
