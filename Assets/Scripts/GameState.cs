using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public BloodSugarCalc sugarCalc;
    public A1cCalc a1CCalc;
    public GameOverMenu gameOverPanel;
    public PauseMenu pauseMenu;
    public LevelCompleteMenu levelCompleteMenu;
    public bool paused;

    public enum GameStates
    {
        Play,
        Pause,
        GameOver,
        LevelComplete
    };

    public GameStates state;

    // Start is called before the first frame update
    void Start()
    {
        state = GameStates.Play;
        paused = false;
    }

    private void TogglePause(bool paused)
    {
        pauseMenu.SetActive(paused);
        state = GameStates.Pause;
        state = GameStates.Play;
    }

    // Update is called once per frame
    void Update()
    {
        if (sugarCalc.BloodSugar == 0)
        {
            pauseMenu.SetActive(false);
            gameOverPanel.SetActive(true);
            state = GameStates.GameOver;
        }
        else if (Input.GetKey("q"))
        {
            pauseMenu.paused = true;
            TogglePause(pauseMenu.paused);
        }
        else if (a1CCalc.A1c > 0)
        {
            levelCompleteMenu.SetActive(true);
        }
        else if (!pauseMenu.paused)
        {
            TogglePause(paused);
        }
    }
}
