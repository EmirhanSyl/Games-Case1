using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public static ProjectilePooler instance;

    [SerializeField] private GameObject projectilePrefab;

    public Queue<GameObject> projectilePool = new Queue<GameObject>();

    void Start()
    {
        instance = this;

        for (int i = 0; i < 32; i++)
        {
            var projectiles = Instantiate(projectilePrefab);
            projectilePool.Enqueue(projectiles);
            projectiles.SetActive(false);
        }
    }

    public void AddPool(GameObject projectile)
    {
        projectilePool.Enqueue(projectile);
    }

    public GameObject CreateProjectile(Vector3 position, Quaternion rotation)
    {
        var projectile = projectilePool.Dequeue();
        projectile.transform.position = position;
        projectile.transform.rotation = rotation;
        projectile.SetActive(true);
        return projectile;
    }
}
