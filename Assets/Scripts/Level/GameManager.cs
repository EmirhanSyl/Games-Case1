using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text gemText;
    [SerializeField] private TMP_Text goldText;

    [SerializeField] private GameObject finishPortal;
    [SerializeField] private Spawner spawner;

    private int gemAmount;
    private int goldAmount;

    private int totalMonsterCount;
    private int killedMonsterCount;

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

        totalMonsterCount = spawner.slimeMonsterCount + spawner.turtleMonsterCount;
    }

    public void IncreaseGemAmount(int amount)
    {
        gemAmount += amount;
        gemText.text = gemAmount.ToString();
        PlayerPrefs.SetInt("GemAmount", gemAmount);
    }
    
    public void IncreaseGoldAmount(int amount)
    {
        goldAmount += amount;
        goldText.text = goldAmount.ToString();
        PlayerPrefs.SetInt("GoldAmount", goldAmount);
    }

    public void MonsterKilled()
    {
        killedMonsterCount++;

        if (killedMonsterCount >= totalMonsterCount)
        {
            Passed();
        }
    }

    public void Failed()
    {
        StartCoroutine(GetBackToMenuScene());
    }

    public void Passed()
    {
        var level = PlayerPrefs.GetInt("Level");
        level++;
        PlayerPrefs.SetInt("Level", level);
        finishPortal.SetActive(true);
    }

    IEnumerator GetBackToMenuScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
    
}
