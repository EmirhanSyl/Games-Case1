using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private MonsterHealth monsterHealth;

    private bool onTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        monsterHealth = GetComponent<MonsterHealth>();
    }

    
    void Update()
    {
        if (monsterHealth.IsDead()) return;

        SetTarget();
    }

    void SetTarget()
    {
        float distanceToTarget = (player.transform.position - transform.position).magnitude;

        if (distanceToTarget < 0.5f)
        {
            agent.SetDestination(transform.position);
            animator.SetBool("Idle", true);
            onTarget = true;
        }
        else
        {
            agent.SetDestination(player.transform.position);
            //transform.Translate(Time.deltaTime * movementSpeed * (player.transform.position - transform.position));
            animator.SetBool("Idle", false);
            onTarget = false;
        }
    }

    public bool OnTarget()
    {
        return onTarget;
    }
}
