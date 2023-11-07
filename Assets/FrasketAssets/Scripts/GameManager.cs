using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score = 0;

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

}