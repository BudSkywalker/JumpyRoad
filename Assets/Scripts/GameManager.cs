using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public int RecentScore { get; private set; }
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
                    Time.timeScale = 1f;
                    break;
                case GameState.Playing:
                    StartCoroutine(FindGameOverPanel());
                    Time.timeScale = 1f;
                    break;
                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;
                case GameState.GameOver:
                    Time.timeScale = 0f;
                    RecentScore = FindObjectOfType<PlayerController>().HighestScore;
                    gameOverPanel.SetActive(true);
                    if (RecentScore > PlayerPrefs.GetInt("Highscore"))
                    {
                        PlayerPrefs.SetInt("Highscore", RecentScore);
                    }
                    break;
                default: 
                    break;
            }
        }
    }

    public float musicVolume = 0.55f;
    public float sfxVolume = 0.55f;
    public bool hasFullscreen = true;

    private GameObject gameOverPanel;

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

    IEnumerator FindGameOverPanel()
    {
        while(!gameOverPanel)
        {
            gameOverPanel = GameObject.Find("Game Over Panel");
            yield return new WaitForEndOfFrame();
        }
        
        gameOverPanel.SetActive(false);
    }
}

public enum GameState
{
    Menu,
    Playing,
    Paused,
    GameOver
}
