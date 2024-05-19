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

    void Start()
    {
        shotTimer = timeBetweenShots;
    }

    void Update()
    {
        if (shotTimer <= 0)
        {
            AttackClosestEnemy();
            shotTimer = timeBetweenShots;
        }
        else
        {
            shotTimer -= Time.deltaTime;
        }
    }

    private void AttackClosestEnemy()
    {
        Transform closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, closestEnemy.position);
            if (distanceToEnemy <= maxShootingDistance)  // Vérifie si l'ennemi est à portée
            {
                Instantiate(bullet, transform.position, Quaternion.identity);  // Tir
            }
        }
    }

    private Transform FindClosestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy.transform;
                closestDistance = distance;
            }
        }
        return closestEnemy;
    }
}
