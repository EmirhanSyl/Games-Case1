using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health instance;

    public bool isDead;

    [SerializeField] private float maxHealth_Lvl0;
    [SerializeField] private float maxHealth_Lvl1;
    [SerializeField] private float maxHealth_Lvl2;
    [SerializeField] private float maxHealth_Lvl3;

    [SerializeField] private float godModeDuration;
    [SerializeField] private HealthBar healthBar;

    private float currentHealth;
    private float godModeTimer;

    private int healthLevel;

    private bool godMode;

    private AnimationManager animationManager;
    private GameManager gameManager;

    void Start()
    {
        instance = this;

        animationManager = GetComponent<AnimationManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        if (PlayerPrefs.HasKey("HealthLevel"))
        {
            healthLevel = PlayerPrefs.GetInt("HealthLevel");
        }
        else
        {
            healthLevel = 0;
        }

        switch (healthLevel)
        {
            case 0:
                currentHealth = maxHealth_Lvl0;
                break;
            case 1:
                currentHealth = maxHealth_Lvl1;
                break;
            case 2:
                currentHealth = maxHealth_Lvl2;
                break;
            case 3:
                currentHealth = maxHealth_Lvl3;
                break;
        }

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(currentHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    
    void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Dead();
            return;
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

    public void DamageTaken(float damageAmount)
    {
        if (godMode)
        {
            return;
        }
        currentHealth -= damageAmount;
        animationManager.animationStatesDropdown = AnimationManager.animationStates.hitted;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        godMode = true;
    }

    void Dead()
    {
        isDead = true;
        healthBar.SetHealth(currentHealth);
        animationManager.animationStatesDropdown = AnimationManager.animationStates.dead;
        gameManager.Failed();
    }
}
