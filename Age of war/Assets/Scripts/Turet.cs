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
    public Transform bulletSpawnPoint;

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
        string currentTag = transform.root.tag;
        
        if (currentTag == "TurretSlotAlly"){
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
        else if (currentTag == "TurretSlotEnnemy"){
                if (shotTimer <= 0)
                {
                    AttackClosestAlly();
                    shotTimer = timeBetweenShots;
                }
                else
                {
                    shotTimer -= Time.deltaTime;
                }
            
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
        Transform closestEnemy = FindClosestEnnemy();
        if (closestEnemy != null && Vector2.Distance(transform.position, closestEnemy.position) <= maxShootingDistance)
        {
            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);  // Utilisez bulletSpawnPoint.position ici
        }
    }


     private void AttackClosestAlly()
    {
        Transform closestAlly = FindClosestAlly();
        if (closestAlly != null && Vector2.Distance(transform.position, closestAlly.position) <= maxShootingDistance)
        {
            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);  // Utilisez bulletSpawnPoint.position ici
        }
    }

    private Transform FindClosestEnnemy()
    {
        Transform closestEnnemy = null;
        float closestDistanceEnnemy = Mathf.Infinity;
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach (GameObject ennemy in ennemies)
        {
            float distance = Vector2.Distance(transform.position, ennemy.transform.position);
            if (distance < closestDistanceEnnemy)
            {
                closestEnnemy = ennemy.transform;
                closestDistanceEnnemy = distance;
            }
        }
        return closestEnnemy;
    }
    
    private Transform FindClosestAlly()
    {
        Transform closestAlly = null;
        float closestDistanceAlly = Mathf.Infinity;
        GameObject[] allys = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject ally in allys)
        {
            float distance = Vector2.Distance(transform.position, ally.transform.position);
            if (distance < closestDistanceAlly) 
            {
                closestAlly = ally.transform;
                closestDistanceAlly = distance;
            }
        }
        return closestAlly;
    }
}
