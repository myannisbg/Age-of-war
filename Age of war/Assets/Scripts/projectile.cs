using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 100;

    private Transform target;
    private Vector2 targetPosition;
    public Unit.UnitType unitType;

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
        string currentTag = transform.root.tag;
        if (currentTag == "BulletAlly"){
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
        else if (currentTag=="BulletEnnemy"){
            float closestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ally");

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
    }

 

    private void OnTriggerEnter2D(Collider2D other)
    {
        string currentTag = transform.root.tag;
         if (currentTag == "BulletAlly"){
        if (other.CompareTag("Ennemy"))
        {
            Unit unit = other.gameObject.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeDamage(damage,Unit.UnitType.None);
            }
            Destroy(gameObject);
        }
    }
    else if (currentTag == "BulletEnnemy"){
         if (other.CompareTag("Ally"))
        {
            Unit unit = other.gameObject.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeDamage(damage,Unit.UnitType.None);
            }
            Destroy(gameObject);
        }

    }
    }
}
