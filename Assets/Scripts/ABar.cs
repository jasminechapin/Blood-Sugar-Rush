using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
[AddComponentMenu("UI/Simple Health Bar/Simple Health Bar")]
public abstract class APlayerBar : MonoBehaviour
{
    protected Image fill;
    public BloodSugarCalc player;

    public Color barColor = Color.white;


    public Text barText;

    public float maxValue;
    public float currentValue;
    private float currentFraction;

    public float GetCurrentFraction
    {
        get
        {
            return currentFraction;
        }
    }
    public float SetCurrentFraction
    {
        set
        {
            currentFraction = value;
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
        if (fill == null)
            return;

        currentFraction = currentValue / maxValue;

        if (currentFraction < 0 || currentFraction > 1)
            currentFraction = currentFraction < 0 ? 0 : 1;

        targetFill = currentFraction;
        fill.fillAmount = targetFill;
        DisplayText();
    }

    protected abstract void Update();
    protected void Awake()
    {
        fill = this.GetComponent<Image>();
    }
}
#endregion
