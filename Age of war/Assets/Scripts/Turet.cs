using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turet : MonoBehaviour
{
    [Header("Stats")]
    public float timeBetweenShots = 2f;  // Temps entre les tirs
    public float maxShootingDistance = 10f;  // Distance maximale de tir

    [Header("References")]
    public GameObject bullet;  // Pr�fab de la balle

    private float shotTimer;  // Compteur pour le prochain tir
    private bool isDeletionMode = false;

    private Camera mainCamera;
    private Renderer objectRenderer;

    void Start()
    {
        shotTimer = timeBetweenShots;
        mainCamera = Camera.main;
        objectRenderer = GetComponent<Renderer>(); // Utilise le Renderer pour d�terminer les limites de l'objet
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isDeletionMode = !isDeletionMode;  // Active ou d�sactive le mode suppression
            Debug.Log("Mode suppression: " + (isDeletionMode ? "activ�" : "d�sactiv�"));
        }

        if (shotTimer <= 0)
        {
            AttackClosestEnnemy();
            shotTimer = timeBetweenShots;
        }
        else
        {
            shotTimer -= Time.deltaTime;
        }

        // V�rifie les clics de souris pour la suppression
        if (isDeletionMode && Input.GetMouseButtonDown(0))
        {
            CheckForObjectDeletion();
        }
    }

    private void CheckForObjectDeletion()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // V�rifie si la position de la souris est � l'int�rieur des limites de l'objet
        if (IsMouseOverGameObject(mousePosition))
        {
            Debug.Log("Objet d�truit.");
            Destroy(gameObject);
        }
    }

    private bool IsMouseOverGameObject(Vector2 mousePosition)
    {
        if (objectRenderer != null)
        {
            Bounds bounds = objectRenderer.bounds;
            return bounds.Contains(mousePosition);
        }
        return false;
    }

    private void AttackClosestEnnemy()
    {
        Transform closestEnnemy = FindClosestEnnemy();
        if (closestEnnemy != null)
        {
            float distanceToEnnemy = Vector2.Distance(transform.position, closestEnnemy.position);
            if (distanceToEnnemy <= maxShootingDistance)  // V�rifie si l'ennemi est � port�e
            {
                Instantiate(bullet, transform.position, Quaternion.identity);  // Tir
            }
        }
    }

    private Transform FindClosestEnnemy()
    {
        Transform closestEnnemy = null;
        float closestDistance = Mathf.Infinity;
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach (GameObject ennemy in ennemies)
        {
            float distance = Vector2.Distance(transform.position, ennemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnnemy = ennemy.transform;
                closestDistance = distance;
            }
        }
        return closestEnnemy;
    }
}
