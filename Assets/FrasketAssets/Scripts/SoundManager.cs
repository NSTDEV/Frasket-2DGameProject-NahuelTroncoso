using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public Slider slider;
    private float sliderVolume;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         slider.enable = true;
    //     }
    // }

    void Start()
    {
        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
            Debug.LogWarning("Slider is not assigned.");
        }
        else if (slider != null)
        {
            slider.value = PlayerPrefs.GetFloat("volumenAudio", 1f);
            AudioListener.volume = slider.value;
        }
    }

    public void ChangeSlider(float value)
    {
        sliderVolume = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderVolume);
        AudioListener.volume = slider.value;
    }
}