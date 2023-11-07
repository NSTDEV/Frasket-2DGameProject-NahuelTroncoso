using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public bool canGoToNextLevel;
    public int levelIndex;

    public static void ResetScore()
    {
        score = 0;
    }

    void Awake()
    {
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeLevel(levelIndex);
        }

        if (canGoToNextLevel)
        {
            ChangeLevel(levelIndex);
        }
    }

    public void ChangeLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

}