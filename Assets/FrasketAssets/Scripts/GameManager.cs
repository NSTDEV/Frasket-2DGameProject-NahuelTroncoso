using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int score = 0;
    public static int scoreToCatch = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreToCatchText;
    public TextMeshProUGUI timeText;
    public static float gameTimer = 0f;

    public Animator transitionAnimator;

    void Start()
    {
        gameTimer = 40f;

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            transitionAnimator.SetBool("EnterNextRoom", false);
            gameTimer = 60f;
        }

        if (scoreText == null || timeText == null)
        {
            Debug.LogWarning("Score or Time Text not assigned.");
        }
    }

    void Update()
    {
        gameTimer -= Time.deltaTime;
        gameTimer = Mathf.Max(0, gameTimer);


        if (scoreText != null)
        {
            scoreText.text = GameManager.score.ToString();
            scoreToCatchText.text = GameManager.scoreToCatch.ToString();
        }

        if (gameTimer <= 0)
        {
            transitionAnimator.SetBool("EnterNextRoom", true);
        }

        if (timeText != null)
        {
            timeText.text = gameTimer.ToString("f0");
        }

        SceneTransition();
    }

    private void SceneTransition()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Level1")
        {
            scoreToCatch = 300;

            if (gameTimer <= 0 && score >= scoreToCatch)
            {
                StartCoroutine(ChangeScene("Level2"));
            }
        }
        else if (currentSceneName == "Level2")
        {
            scoreToCatch = 550;

            if (gameTimer <= 0 && score >= scoreToCatch)
            {
                StartCoroutine(ChangeScene("GameOver"));
            }
        }
    }

    IEnumerator ChangeScene(string sceneName)
    {
        float waitTime = 1.2f;
        yield return new WaitForSeconds(waitTime);

        float transitionDuration = 1.2f;
        yield return new WaitForSecondsRealtime(transitionDuration);

        SceneManager.LoadScene(sceneName);
    }
}