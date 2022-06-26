using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    public GameObject targetEnemy;
    private Collider[] enemyColls;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Health.instance.isDead) return;

        DetectEnemy();
        if (!targetEnemy.activeSelf)
        {
            targetEnemy = null;
        }
    }

    void DetectEnemy()
    {
        enemyColls = Physics.OverlapSphere(transform.position, 15f, layerMask);

        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (var currentEnemyColl in enemyColls)
        {
            float distanceToCurrentEnemy = (currentEnemyColl.gameObject.transform.position - transform.position).sqrMagnitude;
            if (distanceToCurrentEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToCurrentEnemy;
                closestEnemy = currentEnemyColl.gameObject;
                targetEnemy = closestEnemy;
            }
        }
    }
}
