using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OneMultiButton : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject menuOptions;
    public GameObject slider;
    public GameObject easterEgg;
    private bool pausedGame = false;
    public AudioSource touch, menu;

    private void Start()
    {
        if (pauseButton == null)
        {
            pauseButton = GameObject.Find("PauseButton");
            Debug.LogWarning("Pause Button is not found.");
        }

        if (menuOptions == null)
        {
            menuOptions = GameObject.Find("MenuOptions");
            Debug.LogWarning("Menu Options is not found.");
        }

        if (easterEgg == null)
        {
            easterEgg = GameObject.Find("EasterEgg");
            Debug.LogWarning("Easter Egg is not found.");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }
    }

    public void PauseResumeGame()
    {
        pausedGame = !pausedGame;
        touch.Play();

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

    public void optionsSetter(bool option1, bool option2)
    {
        pauseButton.SetActive(option1);
        menuOptions.SetActive(option2);
        easterEgg.SetActive(option2);
        slider.SetActive(option2);
    }

    public void ExitGame()
    {
        Debug.Log("Thanks you berry berry much =)");
        Debug.Log("Closing game...");
        touch.Play();
        Application.Quit();
    }

    public void GoToMenu()
    {
        if (pausedGame) PauseResumeGame();
        menu.Play();
        SceneManager.LoadScene("MainMenu");
        GameManager.score = 0;
    }

    public void StartGame()
    {
        touch.Play();
        SceneManager.LoadScene("Level1");
    }

    public void ActivateEasterEgg()
    {
        touch.Play();
        // Implementa aquí la lógica para activar tu "Easter Egg"
    }
}