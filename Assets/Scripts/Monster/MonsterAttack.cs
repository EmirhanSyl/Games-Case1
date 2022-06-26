using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] private float minDamageAmount;
    [SerializeField] private float maxDamageAmount;

    [SerializeField] private float attackDuration;

    private float attackTimer;
    private bool insideThePlayer;

    private Animator animator;
    private MonsterMovement monsterMovement;
    private MonsterHealth monsterHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        monsterMovement = GetComponent<MonsterMovement>();
        monsterHealth = GetComponent<MonsterHealth>();

        attackTimer = attackDuration;
    }

    
    void Update()
    {
        if (monsterHealth.IsDead()) return;
        

        if (monsterMovement.OnTarget())
        {
            Attacking();
        }
    }

    void Attacking()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDuration)
        {
            animator.SetTrigger("Attack1");
            DecreaseHealth();
            attackTimer = 0;
        }
    }

    void DecreaseHealth()
    {
        if (!insideThePlayer)
        {
            return;
        }

        float damage = Random.Range(minDamageAmount, maxDamageAmount);
        Health.instance.DamageTaken(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            insideThePlayer = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            insideThePlayer = false;
        }
    }
}
