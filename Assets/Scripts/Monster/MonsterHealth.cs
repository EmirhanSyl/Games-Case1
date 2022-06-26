using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float godModeDuration;
    [SerializeField] private ParticleSystem damageParticles;

    private float currentHealth;

    private float minDamageAmount;
    private float maxDamageAmount;
    private float godModeTimer;

    private bool isDead;
    private bool godMode;

    private GameObject player;
    private Animator animator;
    private HealthBar healthBar;
    private GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        healthBar = GetComponentInChildren<HealthBar>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        minDamageAmount = player.GetComponent<Attack>().minDamageAmount;
        maxDamageAmount = player.GetComponent<Attack>().maxDamageAmount;

        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        damageParticles.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (currentHealth <= 0)
        {
            Dead();
        }

        if (godMode)
        {
            godModeTimer += Time.deltaTime;
            if (godModeTimer > godModeDuration)
            {
                godModeTimer = 0;
                godMode = false;
            }
        }
    }

    public void DecreaseHealth()
    {
        if (godMode)
        {
            return;
        }

        float damage = Random.Range(minDamageAmount, maxDamageAmount);
        currentHealth -= damage;
        if (!player.GetComponent<Attack>().slashParticles.gameObject.activeSelf)
        {
            player.GetComponent<Attack>().manaAmount += damage;
        }

        if (healthBar != null)
        {
           healthBar.SetHealth(currentHealth);
        }
        animator.SetTrigger("DamageTaken");

        damageParticles.gameObject.SetActive(true);
        damageParticles.Play();
        godMode = true;
    }

    void Dead()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        StartCoroutine(Destroy());

        //Destroying animations...
        IEnumerator Destroy()
        {
            yield return new WaitForSeconds(1.5f);
            gameManager.IncreaseGoldAmount(Random.Range(5,11));
            gameManager.MonsterKilled();

            float chance = Random.value;
            if (chance >= 0.5f)
            {
                gameManager.IncreaseGemAmount(Random.Range(1, 3));
            }

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerMagic"))
        {
            DecreaseHealth();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
