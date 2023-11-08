using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public bool canGoToNextLevel;
    public int levelIndex;

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject menuOptions;
    [SerializeField] private GameObject easterEgg;
    [SerializeField] private BackgroundMove backgroundController;
    private bool backgroundVisible = false;
    private bool pausedGame = false;

    public static void ResetScore()
    {
        score = 0;
    }

    void Awake()
    {
        backgroundController.rawIMG.enabled = false;
        backgroundController = FindObjectOfType<BackgroundMove>();
        if (backgroundController == null)
        {
            Debug.LogError("Background controller not found.");
        }

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        scoreText.text = GameManager.score.ToString();

        if (canGoToNextLevel)
        {
            ChangeLevel(levelIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateGameState();
        }

        if (backgroundController != null)
        {
            NextLevel();
        }
    }

    public void ChangeLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void optionsSetter(bool option1, bool option2)
    {
        pauseButton.SetActive(option1);
        menuOptions.SetActive(option2);
        easterEgg.SetActive(option2);
    }

    public void StartMenu()
    {
        pausedGame = false;
        ToggleBackgroundVisibility();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SoundUI()
    {
        SceneManager.LoadScene(1);
        ToggleBackgroundVisibility();
    }

    public void ToggleBackgroundVisibility()
    {
        backgroundVisible = !backgroundVisible;
        backgroundController.rawIMG.enabled = backgroundVisible;
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

    public void Close()
    {
        Debug.Log("Closing game...");
        Application.Quit();
    }

    public void NextLevel()
    {
        if (levelIndex == 1 && score >= 300)
        {
            ChangeLevel(2);
        }
        else if (levelIndex == 2 && score >= 550)
        {
            ChangeLevel(3);
        }
    }
}