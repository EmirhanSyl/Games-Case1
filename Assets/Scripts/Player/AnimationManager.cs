using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public enum animationStates { idle, walk, attack, hitted, dead};
    public animationStates animationStatesDropdown;

    private Animator animator;
    private Movement movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
    }

    
    void Update()
    {
        SetAnimationStates();
    }

    void SetAnimationStates()
    {
        switch (animationStatesDropdown)
        {
            case animationStates.idle:
                animator.SetBool("IsMoving", false);
                break;
            case animationStates.walk:
                animator.SetBool("IsMoving", true);
                break;
            case animationStates.attack:
                animator.SetTrigger("Attack");
                break;
            case animationStates.hitted:
                animator.SetTrigger("DamageTaken");
                break;
            case animationStates.dead:
                animator.SetBool("Dead", true);
                break;
            default:
                break;
        }
    }
}
