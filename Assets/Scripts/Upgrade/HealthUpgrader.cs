using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpgrader : MonoBehaviour
{
    [SerializeField] private UpgradeCosts costs;
    [SerializeField] private Image radialWhell;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text costText;

    public int currentLevel;
    private float progress;

    private MenuManager menuManager;

    void Start()
    {
        menuManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuManager>();
        
        

        if (PlayerPrefs.HasKey("HealthLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("HealthLevel");
        }
        else
        {
            currentLevel = 0;
            PlayerPrefs.SetInt("HealthLevel", currentLevel);
        }

        switch (currentLevel)
        {
            case 0:
                costText.text = "COST: " + costs.cost_Health_Lvl1;
                levelText.text = "LEVEL - " + currentLevel;
                break;
            case 1:
                costText.text = "COST: " + costs.cost_Health_Lvl2;
                levelText.text = "LEVEL - " + currentLevel;
                break;
            case 2:
                costText.text = "COST: " + costs.cost_Health_Lvl3;
                levelText.text = "LEVEL - " + currentLevel;
                break;
            case 3:
                costText.text = "MAX";
                levelText.text = "MAX LEVEL REACHED";
                break;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CheckCosts())
            {
                progress += Time.deltaTime;
                radialWhell.fillAmount = progress / 2;

                if (progress >= 2)
                {
                    currentLevel++;
                    PlayerPrefs.SetInt("HealthLevel", currentLevel);
                    DecreaseMoney();
                    progress = 0;

                    levelText.text = "LEVEL - " + currentLevel;
                    switch (currentLevel)
                    {
                        case 1:
                            costText.text = "COST: " + costs.cost_Health_Lvl2;
                            break;
                        case 2:
                            costText.text = "COST: " + costs.cost_Health_Lvl3;
                            break;
                        case 3:
                            costText.text = "MAX";
                            break;
                    }

                    if (currentLevel == 3)
                    {
                        levelText.text = "MAX LEVEL REACHED";
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            progress = 0;
            radialWhell.fillAmount = progress;
        }
    }

    bool CheckCosts()
    {
        int nextLevel = currentLevel + 1;
        bool beAbleToBuy = false;

        switch (nextLevel)
        {
            case 1:
                if (menuManager.goldAmount >= costs.cost_Health_Lvl1)
                {
                    beAbleToBuy = true;
                }
                break;
            case 2:
                if (menuManager.goldAmount >= costs.cost_Health_Lvl2)
                {
                    beAbleToBuy = true;
                }
                break;
            case 3:
                if (menuManager.goldAmount >= costs.cost_Health_Lvl3)
                {
                    beAbleToBuy = true;
                }
                break;
            case 4:
                beAbleToBuy = false;
                break;
        }

        return beAbleToBuy;
    }

    void DecreaseMoney()
    {
        switch (currentLevel)
        {
            case 1:
                menuManager.DecreaseGoldAmount(costs.cost_Health_Lvl1);
                break;
            case 2:
                menuManager.DecreaseGoldAmount(costs.cost_Health_Lvl2);
                break;
            case 3:
                menuManager.DecreaseGoldAmount(costs.cost_Health_Lvl3);
                break;
        }
    }
}
