using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    Player player;

    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();

        UpdateHealthText();
    }

    private void Update()
    {
        UpdateHealthText();
    }

    public void UpdateHealthText()
    {
        healthText.text = player.GetHealth().ToString();
    }
}
