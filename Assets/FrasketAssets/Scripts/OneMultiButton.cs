using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class MultiButtonController : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject menuOptions;
    public GameObject easterEgg;
    public AudioSource touch, menu;
    public GameObject transition;
    private Animator transitionAnimator;
    public Slider slider;
    private float sliderVolume;

    private bool pausedGame = false;
    private bool retryStarted = false;
    private bool tutorialOn = false;

    private void Start()
    {
        if (pauseButton == null || menuOptions == null || easterEgg == null || slider == null)
        {
            Debug.LogError("One or more game objects are not assigned.");
        }
        if (slider != null)
        {
            slider.value = PlayerPrefs.GetFloat("volumenAudio", 1f);
            AudioListener.volume = slider.value;
        }

        transitionAnimator = transition.GetComponent<Animator>();
        transition.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !tutorialOn)
        {
            PauseResumeGame();
        }

        if (GameManager.gameTimer <= 0 && GameManager.score < GameManager.scoreToCatch && !retryStarted)
        {
            retryStarted = true;
            StartCoroutine(StartRetry());
        }
    }

    public void PauseResumeGame()
    {
        pausedGame = !pausedGame;
        touch.Play();

        if (tutorialOn)
        {
            transitionAnimator.SetBool("Enter", false);
            Time.timeScale = 1f;
            tutorialOn = false;
        }
        else if (pausedGame)
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

    private void optionsSetter(bool option1, bool option2)
    {
        pauseButton.SetActive(option1);
        menuOptions.SetActive(option2);
        easterEgg.SetActive(option2);
        easterEgg.transform.GetChild(0).gameObject.SetActive(false);
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

        StartCoroutine(ChangeScene("MainMenu"));
    }

    public void StartTutorial()
    {
        touch.Play();
        transition.SetActive(true);
        Time.timeScale = 0f;
        tutorialOn = true;
        transitionAnimator.SetBool("Enter", true);
    }

    IEnumerator StartRetry()
    {
        yield return new WaitForSeconds(1.5f);
        StartTutorial();
    }

    public void StartGame()
    {
        touch.Play();
        transitionAnimator.SetBool("Enter", false);
        Time.timeScale = 1f;

        StartCoroutine(ChangeScene("Level1"));
    }

    IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
        GameManager.score = 0;
    }

    public void ActivateEasterEgg()
    {
        touch.Play();
        StartCoroutine(ActivateAndDeactivateEasterEgg());
    }

    private IEnumerator ActivateAndDeactivateEasterEgg()
    {
        easterEgg.transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1.5f);
        easterEgg.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ChangeSlider(float value)
    {
        sliderVolume = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderVolume);
        AudioListener.volume = slider.value;
    }
}