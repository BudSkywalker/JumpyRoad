using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _state;
    public GameState CurrentState
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            //TODO: Use state controller to toggle UI Panels @Joel
            switch(CurrentState)
            {
                case GameState.Menu:

                    break;
                case GameState.Playing:

                    break;
                case GameState.Paused:

                    break;
                case GameState.GameOver:

                    break;
                default: break;
            }
        }
    }
    public int score, highscore;
    public UIController uiController;

    //Singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            AddToScore(1);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
        */
    }

    //Game states
    public void YouWin()
    {

    }

    public void YouLose()
    {

    }

    public void ResetGame()
    {
        CheckHighscore();
        uiController.UpdateHighscore();

        score = 0;
        uiController.UpdateScore();
    }

    public void PauseGame()
    {
        Time.timeScale = 1.0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 0.0f;
    }

    //Score system
    private void CheckHighscore()
    {
        if(highscore <= score)
        {
            highscore = score;
        }
    }

    public void AddToScore(int addNum)
    {
        score = score + addNum;
        uiController.UpdateScore();
    }

    //Settings
    public void SetVolume(float volumeNum)
    {

    }

    public void Fullscreen(bool isFullscreen)
    {

    }
}

public enum GameState
{
    Menu,
    Playing,
    Paused,
    GameOver
}
