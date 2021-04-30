using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthDisplay : MonoBehaviour
{
    //[SerializeField] int startingHealth = 10;
    int currentHealth;
    Text healthText;

    LevelController levelController;

    void Start()
    {
        healthText = GetComponent<Text>();
        levelController = FindObjectOfType<LevelController>();

        SetStartingHealth();
        UpdateText();
    }

    private void UpdateText()
    {
        healthText.text = currentHealth.ToString();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            levelController.HandleLoseCondition();
        }

        UpdateText();
    }

    private void SetStartingHealth()
    {
        int difficulty = PlayerPrefsController.GetDifficulty();

        if (difficulty == 0)
        {
            currentHealth = 10;
        }
        else if (difficulty == 1)
        {
            currentHealth = 5;
        }
        else if (difficulty == 2)
        {
            currentHealth = 1;
        }
    }
}
