using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public Slider slider;
    private float sliderVolume;
    private float timer = 10f;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (scoreText == null)
        {
            scoreText = GetComponentInChildren<TextMeshProUGUI>();
            Debug.LogWarning("Score Text is not assigned.");
        }
        if (timeText == null)
        {
            timeText = GetComponentInChildren<TextMeshProUGUI>();
            Debug.LogWarning("Time Text is not assigned.");
        }
        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
            Debug.LogWarning("Slider is not assigned.");
        }

        slider.value = PlayerPrefs.GetFloat("volumenAudio", 1f);
        AudioListener.volume = slider.value;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (scoreText != null)
        {
            scoreText.text = GameManager.score.ToString();
        }

        if (timer <= 0)
        {
            timer = 0;
        }
        if (timeText != null)
        {
            timeText.text = timer.ToString("f0");
        }

        NextLevel();
    }

    public void ChangeSlider(float value)
    {
        sliderVolume = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderVolume);
        AudioListener.volume = slider.value;
    }

    public void NextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "MainMenu")
        {
            score = 0;
        }
        if (currentSceneName == "Level1" && timer <= 0 && score >= 300)
        {
            timer = 40f;
            SceneManager.LoadScene("Level2");
        }
        if (currentSceneName == "Level2" && timer <= 0 && score >= 500)
        {
            timer = 60f;
            SceneManager.LoadScene("GameOver");
        }
    }
}