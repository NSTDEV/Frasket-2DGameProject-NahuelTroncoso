using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject easterEgg;
    [SerializeField] private RawImage backgroundRawImage;
    private bool backgroundVisible = false;
    private bool pausedGame = false;

    void Start()
    {
        backgroundRawImage.enabled = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateGameState();
        }
    }

    public void optionsSetter(bool option1, bool option2)
    {
        pauseButton.SetActive(option1);
        pauseMenu.SetActive(option2);
        easterEgg.SetActive(option2);
    }

    public void StartMenu()
    {
        pausedGame = false;
        ToggleBackgroundVisibility();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleBackgroundVisibility()
    {
        backgroundVisible = !backgroundVisible;
        backgroundRawImage.enabled = backgroundVisible;
    }

    public void Close()
    {
        Debug.Log("Closing game...");
        Application.Quit();
    }

    public void UpdateGameState()
    {
        pausedGame = !pausedGame;
        ToggleBackgroundVisibility();

        if (pausedGame)
        {
            Time.timeScale = 0f;
            optionsSetter(false, true);
        }
        else
        {
            Time.timeScale = 1f;
            optionsSetter(true, false);
        }
    }

    public bool isPaused()
    {
        return pausedGame;
    }
}