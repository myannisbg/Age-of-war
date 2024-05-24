using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turet : MonoBehaviour
{
    [Header("Stats")]
    public float timeBetweenShots = 2f;  // Temps entre les tirs
    public float maxShootingDistance = 10f;  // Distance maximale de tir

    [Header("References")]
    public GameObject bullet;  // Préfab de la balle

    private float shotTimer;  // Compteur pour le prochain tir

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
        if (shotTimer <= 0)
        {
            AttackClosestEnnemy();
            shotTimer = timeBetweenShots;
        }
        else
        {
            shotTimer -= Time.deltaTime;
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
        if (closestEnnemy != null && Vector2.Distance(transform.position, closestEnnemy.position) <= maxShootingDistance)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
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
