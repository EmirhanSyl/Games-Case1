using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    private int level;
    
    
    void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
        else
        {
            level = 1;
            PlayerPrefs.SetInt("Level", level);
        }

        if (level >= 3)
        {
            level = 1;
            PlayerPrefs.SetInt("Level", level);
        }

        levelText.text = "LEVEL - " + level.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (level)
            {
                case 1:
                    SceneManager.LoadScene(1);
                    break;
                case 2:
                    SceneManager.LoadScene(2);
                    break;
            }
        }
    }
}
