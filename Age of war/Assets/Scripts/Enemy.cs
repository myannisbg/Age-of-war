using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float timeBetweenShots = 2f;  // Temps entre les tirs
    public float maxShootingDistance = 10f;  // Distance maximale de tir

    [Header("References")]
    public GameObject bullet;  // Préfab de la balle

    private float shotTimer;  // Compteur pour le prochain tir
    private bool isDeletionMode = false;

    private Camera mainCamera;
    private Renderer objectRenderer;

    void Start()
    {
        shotTimer = timeBetweenShots;
        mainCamera = Camera.main;
        objectRenderer = GetComponent<Renderer>(); // Utilise le Renderer pour déterminer les limites de l'objet
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isDeletionMode = !isDeletionMode;  // Active ou désactive le mode suppression
            Debug.Log("Mode suppression: " + (isDeletionMode ? "activé" : "désactivé"));
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

        // Vérifie les clics de souris pour la suppression
        if (isDeletionMode && Input.GetMouseButtonDown(0))
        {
            CheckForObjectDeletion();
        }
    }

    private void CheckForObjectDeletion()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Vérifie si la position de la souris est à l'intérieur des limites de l'objet
        if (IsMouseOverGameObject(mousePosition))
        {
            Debug.Log("Objet détruit.");
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
            if (distanceToEnnemy <= maxShootingDistance)  // Vérifie si l'ennemi est à portée
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
