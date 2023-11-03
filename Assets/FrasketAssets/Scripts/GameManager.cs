using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int TotalPoints { get { return TotalPoints; } }
    private int totalPoints;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            Debug.Log("It's more than one GameManager on Scene");
        }
    }
}
