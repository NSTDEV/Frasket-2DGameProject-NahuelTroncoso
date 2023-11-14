using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    private float timer = 10f;

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

    public void NextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
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