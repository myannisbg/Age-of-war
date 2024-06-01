using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 100;
    public float inactiveTimeBeforeDestroy = 1f; // Temps avant destruction en secondes si le projectile est inactif
    private Transform target;
    private Vector2 targetPosition;
    private float inactiveTime = 0f; // Temps d'inactivité

    void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            FindClosestEnemy();
        }

        if (target != null)
        {
            targetPosition = target.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f) 
            {
                HitTarget();
            }

            inactiveTime = 0f; // Réinitialiser le compteur d'inactivité quand le projectile bouge
        }
        else
        {
            inactiveTime += Time.deltaTime; // Augmenter le compteur d'inactivité quand il n'y a pas de cible
            if (inactiveTime >= inactiveTimeBeforeDestroy)
            {
                Destroy(gameObject); // Détruire le projectile après 2 secondes d'inactivité
            }
        }
    }

    private void FindClosestEnemy()
    {
        string targetTag = transform.root.CompareTag("BulletAlly") ? "Ennemy" : "Ally";
        float closestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf) 
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

    private void HitTarget()
    {
        if (target != null)
        {
            Unit unit = target.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeDamage(damage, Unit.UnitType.None);
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((transform.root.CompareTag("BulletAlly") && other.CompareTag("Ennemy")) ||
            (transform.root.CompareTag("BulletEnnemy") && other.CompareTag("Ally")))
        {
            Unit unit = other.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeDamage(damage, Unit.UnitType.None);
            }
            Destroy(gameObject);
        }
    }
}