using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[AddComponentMenu("UI/Simple Health Bar/Simple Health Bar")]
public class CGM : APlayerBar
{
    public float targetMax;
    public float targetMin;

    // Start is called before the first frame update
    void Start()
    {
        targetMax = 225.0f;
        targetMin = 75.0f;
        currentValue = player.BloodSugar;
        SetCurrentFraction = (player.BloodSugar / maxValue);
    }

    protected new void UpdateBar()
    {
        base.UpdateBar();
        UpdateColor();
    }


    public void UpdateColor()
    {
        if (fill == null)
            return;

        if (currentValue > targetMax)
        {
            barColor = Color.red;
            fill.color = barColor;
        }
        else if (currentValue < targetMin)
        {
            barColor = Color.blue;
            fill.color = barColor;
        }
        else
        {
            barColor = Color.green;
            fill.color = barColor;
        }
    }

    protected override void Update()
    {
        currentValue = player.BloodSugar;
        UpdateBar();
    }
}