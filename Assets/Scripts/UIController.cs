using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            GameManager.Instance.state = GameState.Menu;
        }

        GameManager.Instance.uiController = this;
    }

    public void OnClickPlay()
    {

    }

    public void OnClickOptions()
    {

    }

    public void OnClickCredits()
    {

    }

    public void OnClickQuit()
    {
        Application.Quit();
        Debug.Log("Is quitting...");
    }

    public void UpdateScore()
    {
        scoreText.text = "Recent Score: " + GameManager.Instance.score;
    }

    public void UpdateHighscore()
    {
        highscoreText.text = "Highscore: " + GameManager.Instance.highscore;
    }

}
