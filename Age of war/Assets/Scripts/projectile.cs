using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 100;

    private Transform target;
    private Vector2 targetPosition;

    void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            FindClosestEnemy();
        }

        if (target != null)
        {
            targetPosition = new Vector2(target.position.x, target.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f) // Pr�cision accrue pour �viter les probl�mes de non collision
        {
            Destroy(gameObject);
        }
    }

    private void FindClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
        {
            Unit unit = other.gameObject.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
