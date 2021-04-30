using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] Defender defenderPrefab;
    [SerializeField] Color32 defaultColor = new Color32(41, 41, 41, 255);

    DefenderSpawner defenderSpawner;
    DefenderButton[] buttons;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defenderSpawner = FindObjectOfType<DefenderSpawner>();
        buttons = FindObjectsOfType<DefenderButton>();

        LabelButtonWithCost();
    }

    private void OnMouseDown()
    {
        defenderSpawner.SetSelectedDefender(defenderPrefab);

        foreach (var button in buttons)
        {
            button.spriteRenderer.color = defaultColor;
        }
        spriteRenderer.color = Color.white;
    }

    private void LabelButtonWithCost()
    {
        Text costText = GetComponentInChildren<Text>();
        if (!costText) { return; }

        costText.text = defenderPrefab.GetStarCost().ToString();
    }
}