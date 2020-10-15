using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[AddComponentMenu("UI/Simple Health Bar/Simple Health Bar")]
public class CGM : MonoBehaviour
{
    public CGM healthBar;
    public Image barImage;
    public Color barColor = Color.white;
    public float targetMax;
    public float targetMin;
    public Text barText;
    public float maxValue;
    [Range(0f, 360f)]
    public float currentValue = 0f;
    public GameObject player;

    float currentFraction;

    public float GetCurrentFraction
    {
        get
        {
            return currentFraction;
        }
    }

    public float targetFill = 0.0f;

    void DisplayText()
    {
        // If the user does not want text to be displayed, or the text component is null, then return.
        if (barText == null)
            return;

        barText.text = (GetCurrentFraction * maxValue).ToString("F0");
    }

    #region PUBLIC FUNCTIONS
    public void UpdateBar()
    {
        if (barImage == null)
            return;

        currentFraction = currentValue / maxValue;

        if (currentFraction < 0 || currentFraction > 1)
            currentFraction = currentFraction < 0 ? 0 : 1;

        targetFill = currentFraction;
        barImage.fillAmount = targetFill;
        DisplayText();
        UpdateColor();
    }

    public void UpdateColor()
    {
        if (barImage == null)
            return;

        if (currentValue > targetMax)
        {
            barColor = Color.red;
            barImage.color = barColor;
        }
        else if (currentValue < targetMin)
        {
            barColor = Color.blue;
            barImage.color = barColor;
        }
        else
        {
            barColor = Color.green;
            barImage.color = barColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxValue = 360.0f;
        targetMax = 225.0f;
        targetMin = 75.0f;
        currentValue = player.GetComponent<BloodSugarCalc>().BloodSugar;
        currentFraction = (player.GetComponent<BloodSugarCalc>().BloodSugar / maxValue);
    }

    void Update()
    {
        currentValue = player.GetComponent<BloodSugarCalc>().BloodSugar;
        UpdateBar();
    }
}
#endregion