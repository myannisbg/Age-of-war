using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTuret : MonoBehaviour
{
    private bool isDeletionMode = false;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; // Assurez-vous que mainCamera est correctement référencée
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Make sure your camera has the 'MainCamera' tag.");
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

        // Lance un rayon depuis la position de la souris
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition2D, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject) // Vérifie que le rayon touche cet objet
        {
            Debug.Log("Objet détruit.");
            Destroy(gameObject);

            // Désactive le mode suppression
            isDeletionMode = false;
            Debug.Log("Mode suppression désactivé après la suppression de la tourelle.");
        }
    }

    // Update is called une fois par frame
    void Update()
    {
        if (isDeletionMode && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tentative de suppression en mode suppression.");
            CheckForObjectDeletion();
        }
    }
}
