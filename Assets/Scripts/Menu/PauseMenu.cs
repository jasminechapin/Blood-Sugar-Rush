using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : IMenu
{
    public Button resumeButton;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(ResumeTaskOnClick);
    }

    void ResumeTaskOnClick()
    {
        paused = false;
    }
}
