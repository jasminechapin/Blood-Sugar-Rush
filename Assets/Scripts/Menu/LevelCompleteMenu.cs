using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteMenu : IMenu
{
    public Button continueButton;
    public TextMeshProUGUI score;
    public A1cCalc a1Ccalc;

    // Start is called before the first frame update
    void Start()
    {
        continueButton.onClick.AddListener(ContinueTaskOnClick);
        score.text = "A1c Score: " + a1Ccalc.A1c;
    }

    void ContinueTaskOnClick()
    {
        //Load new level
    }
}
