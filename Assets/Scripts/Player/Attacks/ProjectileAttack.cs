using System.Collections;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] private float instantiateOffset;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileInitLoc;

    private ProjectilePooler pooler;

    private void Start()
    {
        pooler = ProjectilePooler.instance;    
    }

    public void ThrowProjectile(int level)
    {
        StartCoroutine(WaitTime(level));
    }

    IEnumerator WaitTime(int level)
    {
        yield return new WaitForSeconds(instantiateOffset);

        switch (level)
        {
            case 0:
                Lvl0Proj();
                break;
            case 1:
                Lvl1Proj();
                break;
            case 2:
                Lvl2Proj();
                break;
            case 3:
                Lvl3Proj();
                break;
        }
        
    }

    void Lvl0Proj()
    {
        //var project = Instantiate(projectile, projectileInitLoc.position, transform.rotation);
        var project = ProjectilePooler.instance.CreateProjectile(projectileInitLoc.position, transform.rotation);
        project.GetComponent<ProjectileMovement>().ChargeToEnemy();
    }
    void Lvl1Proj()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                //var project = Instantiate(projectile, projectileInitLoc.position, transform.rotation);
                var project = ProjectilePooler.instance.CreateProjectile(projectileInitLoc.position, transform.rotation);
                project.GetComponent<ProjectileMovement>().ChargeToEnemy();
            }
            else if (i == 1)
            {
                //var project = Instantiate(projectile, projectileInitLoc.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 45, transform.eulerAngles.z)));
                var project = ProjectilePooler.instance.CreateProjectile(projectileInitLoc.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 45, transform.eulerAngles.z)));
                project.GetComponent<ProjectileMovement>().ChargeToEnemy();
            }
            else
            {
                //var project = Instantiate(projectile, projectileInitLoc.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 45, transform.eulerAngles.z)));
                var project = ProjectilePooler.instance.CreateProjectile(projectileInitLoc.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 45, transform.eulerAngles.z)));
                project.GetComponent<ProjectileMovement>().ChargeToEnemy();
            }
        }
    }
    void Lvl2Proj()
    {
        float angle = 0;
        for (int i = 0; i < 4; i++)
        {
            //var project = Instantiate(projectile, projectileInitLoc.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + angle, transform.eulerAngles.z)));
            var project = ProjectilePooler.instance.CreateProjectile(projectileInitLoc.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + angle, transform.eulerAngles.z)));
            project.GetComponent<ProjectileMovement>().ChargeToEnemy();
            angle += 90;
        }
    }
    void Lvl3Proj()
    {
        float angle = 0;
        for (int i = 0; i < 8; i++)
        {
            //var project = Instantiate(projectile, projectileInitLoc.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + angle, transform.eulerAngles.z)));
            var project = ProjectilePooler.instance.CreateProjectile(projectileInitLoc.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + angle, transform.eulerAngles.z)));
            project.GetComponent<ProjectileMovement>().ChargeToEnemy();
            angle += 45;
        }
    }
}
