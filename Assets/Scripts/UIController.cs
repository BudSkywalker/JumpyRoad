using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private GameObject optionsCanvas;

    [SerializeField] AudioMixer sfxAudioMixer;
    [SerializeField] AudioMixer musicAudioMixer;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle fullscreenToggle;

    // Start is called before the first frame update
    void Start()
    {

        if(SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Credits")
        {
            GameManager.Instance.CurrentState = GameState.Menu;
        }

        GameManager.Instance.uiController = this;


        sfxSlider.value = GameManager.Instance.sfxVolume;
        musicSlider.value = GameManager.Instance.musicVolume;
        fullscreenToggle.isOn = GameManager.Instance.hasFullscreen;

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

    }

    public void UpdateHighscore()
    {

    }

    //Settings
    public void SetMusicVolume(float volumeNum)
    {
        musicAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volumeNum) * 20);
        GameManager.Instance.musicVolume = volumeNum;
    }

    public void SetSFXVolume(float volumeNum)
    {
        sfxAudioMixer.SetFloat("SFXVolume", Mathf.Log10(volumeNum) * 20);
        GameManager.Instance.sfxVolume = volumeNum;
    }

    public void SetFullscreen(bool isFullscreen)
    {

        Screen.fullScreen = isFullscreen;
        GameManager.Instance.hasFullscreen = isFullscreen;
    }

}
