using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    static GameSession gameSession;

    // ScoreDisplay scoreDisplay;
    
    int score = 0;

    private void Awake()
    {
        if (gameSession != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameSession = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }

    
    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int valueToAdd)
    {
        score += valueToAdd;
        // scoreDisplay.UpdateScoreText();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
