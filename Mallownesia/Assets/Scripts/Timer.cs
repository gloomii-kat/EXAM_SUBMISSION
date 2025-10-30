using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // <-- import TextMeshPro namespace

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    float currentTime;
    public float startingTime = 10f;

    [SerializeField] TMP_Text countdownText;
    private bool hasTriggeredGameOver = false;
    private bool timerActive = true;

    private SceneLoader sceneLoader;
    public float CurrentTime => currentTime;

    void Awake()
    {
        // Singleton pattern - only one Timer can exist
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        currentTime = startingTime;
        sceneLoader = FindFirstObjectByType<SceneLoader>();
    }

    void Update()
    {
        // Only update if timer is active and not game over
        if (!timerActive || hasTriggeredGameOver) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            hasTriggeredGameOver = true;

            if (sceneLoader != null)
            {
                sceneLoader.LoadGameOverScene("GameOver");
            }
            else
            {
                Debug.LogWarning("SceneLoader not found in scene.");
            }
        }

        countdownText.text = currentTime.ToString("0");
    }

    // Public methods to control the timer from other scripts
    public void StopTimer()
    {
        timerActive = false;

        // Hide the timer text
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        Debug.Log("Timer stopped and hidden");
    }

    public void StartTimer()
    {
        timerActive = true;

        // Show the timer text
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        Debug.Log("Timer started and shown");
    }

    public void ResetTimer()
    {
        currentTime = startingTime;
        hasTriggeredGameOver = false;
        timerActive = true;

        // Show the timer text
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        Debug.Log("Timer reset");
    }

    public bool IsTimerActive()
    {
        return timerActive;
    }

    // Method to completely hide the timer without stopping it
    public void HideTimer()
    {
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }
    }

    // Method to show the timer without starting it
    public void ShowTimer()
    {
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }
    }
}
