using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private GameObject optionsCanvas;

    // Start is called before the first frame update
    void Start()
    {

        if(SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Credits")
        {
            GameManager.Instance.CurrentState = GameState.Menu;
        }

        GameManager.Instance.uiController = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            optionsCanvas.SetActive(!optionsCanvas.activeSelf);
        }
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickOptions()
    {
        //GameManager.Instance.PauseGame();
    }

    public void OnClickCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
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
