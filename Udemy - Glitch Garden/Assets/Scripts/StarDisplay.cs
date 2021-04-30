using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StarDisplay : MonoBehaviour
{
    [SerializeField] int startingStars = 100;
    
    private int currentStars;
    Text starText;

    void Start()
    {
        starText = GetComponent<Text>();
        currentStars = startingStars;

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        starText.text = currentStars.ToString();
    }

    public void AddStars(int stars)
    {
        currentStars += stars;
        if (currentStars > 999)
        {
            currentStars = 999;
        }
        UpdateDisplay();
    } 

    public bool HaveEnoughStars(int starCost)
    {
        if (currentStars >= starCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
