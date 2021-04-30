using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    GameSession gameSession;

    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();

        UpdateScoreText();
    }

    private void Update()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
