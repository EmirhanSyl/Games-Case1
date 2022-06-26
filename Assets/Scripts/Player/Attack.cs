using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    public float minDamageAmount;
    public float maxDamageAmount;
    public float manaAmount;

    [SerializeField] private float maxMana;
    [SerializeField] private float attackDuration;
    [SerializeField] private float particleDuration = 0.6f;

    public ParticleSystem slashParticles;
    [SerializeField] private HealthBar manaBar;

    private float attackTimer;
    private float particleTimer;

    private int attackLevel;

    private bool beAbleToAttack = true;
    private bool particlePlayed;

    private AnimationManager animationManager;
    private ProjectileAttack projectileAttack;

    void Start()
    {
        animationManager = GetComponent<AnimationManager>();
        projectileAttack = GetComponent<ProjectileAttack>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            beAbleToAttack = false;
        }
        else
        {
            manaBar.SetMaxHealth(maxMana);
        }

        if (PlayerPrefs.HasKey("AttackLevel"))
        {
            attackLevel = PlayerPrefs.GetInt("AttackLevel");
        }
        else
        {
            attackLevel = 0;
        }

        attackTimer = attackDuration;        
    }

    
    void Update()
    {
        if (Health.instance.isDead) return;

        if (manaAmount > maxMana)
        {
            manaAmount = maxMana;
        }

        if (manaBar != null)
        {            
            manaBar.SetHealth(manaAmount);
        }

        if (beAbleToAttack)
        {
            Attacking();
        }
    }

    void Attacking()
    {

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            animationManager.animationStatesDropdown = AnimationManager.animationStates.attack;
            projectileAttack.ThrowProjectile(attackLevel);

            if (manaAmount >= maxMana)
            {                
                slashParticles.gameObject.SetActive(true);
                particlePlayed = true;
                manaAmount = 0;
            }

            attackTimer = attackDuration;
        }

        if (particlePlayed)
        {
            PlayParticles();
        }
    }

    void PlayParticles()
    {
        particleTimer += Time.deltaTime;

        if (particleTimer > particleDuration)
        {           
            var main = slashParticles.main;
            main.startRotationY = (transform.rotation.eulerAngles.y - 147f) * Mathf.Deg2Rad;

            slashParticles.Play();
            particlePlayed = false;
            particleTimer = 0;
        }
    }
}
