using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

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

    public UIController uiController;

    public float musicVolume = 0.55f;
    public float sfxVolume = 0.55f;
    public bool hasFullscreen = true;

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
            DontDestroyOnLoad(this);
        }
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

    }

    public void PauseGame()
    {
        Time.timeScale = 1.0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 0.0f;
    }
  
}

public enum GameState
{
    Menu,
    Playing,
    Paused,
    GameOver
}
