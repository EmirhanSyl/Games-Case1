using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject collesionFX;

    private float deactivationTimer;
    private bool charge;

    void Update()
    {
        if (charge)
        {            
            transform.Translate(Time.deltaTime * movementSpeed * Vector3.forward);
            DeactivationTimer();
        }
    }

    public void ChargeToEnemy()
    {
        //transform.rotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        charge = true;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            other.gameObject.GetComponent<MonsterHealth>().DecreaseHealth();

            deactivationTimer = 0;
            ProjectilePooler.instance.projectilePool.Enqueue(gameObject);
            //ProjectilePooler.instance.AddPool(gameObject);
            gameObject.SetActive(false);            
        }
        else if (other.gameObject.layer == 7) //7 = Wall layer
        {
            deactivationTimer = 0;
            ProjectilePooler.instance.projectilePool.Enqueue(gameObject);
            //ProjectilePooler.instance.AddPool(gameObject);
            Instantiate(collesionFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void DeactivationTimer()
    {
        deactivationTimer += Time.deltaTime;
        if (deactivationTimer > 6)
        {
            ProjectilePooler.instance.projectilePool.Enqueue(gameObject);
            //ProjectilePooler.instance.AddPool(gameObject);
            deactivationTimer = 0;
            gameObject.SetActive(false);
        }
    }
}
