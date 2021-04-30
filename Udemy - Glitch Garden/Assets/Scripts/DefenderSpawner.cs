using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender;

    StarDisplay starDisplay;
    GameObject defenderParent;

    private void Start()
    {
        starDisplay = FindObjectOfType<StarDisplay>();

        CreateDefenderParent();
    }

    private void OnMouseDown()
    {
        if (defender != null)
        {
            TrySpawnDefender(GetSquareClicked());
        }
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find("Defenders");
        if (!defenderParent)
        {
            defenderParent = new GameObject("Defenders");
        }
    }

    private void TrySpawnDefender(Vector2 gridPos)
    {
        if (starDisplay.HaveEnoughStars(defender.GetStarCost()))
        {
            SpawnDefender(gridPos);
            starDisplay.AddStars(-defender.GetStarCost());
        }
    }

    private void SpawnDefender(Vector2 worldPos)
    {
        Defender newDefender = Instantiate(defender, worldPos, Quaternion.identity);
        newDefender.transform.parent = defenderParent.transform;
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);
    }

    public void SetSelectedDefender(Defender defender)
    {
        this.defender = defender;
    }
}