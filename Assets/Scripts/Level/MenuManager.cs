using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public int gemAmount;
    public int goldAmount;

    [SerializeField] private TMP_Text gemText;
    [SerializeField] private TMP_Text goldText;

    void Start()
    {
        if (PlayerPrefs.HasKey("GemAmount"))
        {
            gemAmount = PlayerPrefs.GetInt("GemAmount");
        }
        else
        {
            gemAmount = 0;
        }

        if (PlayerPrefs.HasKey("GoldAmount"))
        {
            goldAmount = PlayerPrefs.GetInt("GoldAmount");
        }
        else
        {
            goldAmount = 0;
        }

        goldText.text = goldAmount.ToString();
        gemText.text = gemAmount.ToString();
    }

    public void DecreaseGemAmount(int amount)
    {
        gemAmount -= amount;
        gemText.text = gemAmount.ToString();
        PlayerPrefs.SetInt("GemAmount", gemAmount);
    }
    public void DecreaseGoldAmount(int amount)
    {
        goldAmount -= amount;
        goldText.text = goldAmount.ToString();
        PlayerPrefs.SetInt("GoldAmount", goldAmount);
    }

}
