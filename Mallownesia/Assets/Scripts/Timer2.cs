using System.Collections;
using UnityEngine;
using TMPro;

public class Timer2 : MonoBehaviour
{
    [Header("Timer Settings")]
    public float bedroomTimeLimit = 60f;
    private float currentTime;
    private bool timerActive = false;
    private bool hasTriggeredGameOver = false;

    [Header("References")]
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private Door2CloseTrigger doorCloseTrigger;
    private SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = FindFirstObjectByType<SceneLoader>();
        currentTime = bedroomTimeLimit;

        // Ensure timer text is hidden at start
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!timerActive || hasTriggeredGameOver) return;

        currentTime -= Time.deltaTime;

        if (countdownText != null)
            countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            hasTriggeredGameOver = true;
            TriggerGameOver();
        }
    }

    public void StartBedroomTimer()
    {
        if (timerActive) return;

        timerActive = true;
        currentTime = bedroomTimeLimit;

        // Show timer UI
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        Debug.Log("Bedroom timer started!");
    }

    public void StopBedroomTimer()
    {
        timerActive = false;

        // Hide timer UI
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        Debug.Log("Bedroom timer stopped!");
    }

    private void TriggerGameOver()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadGameOverScene("GameOver");
        }
        else
        {
            Debug.LogWarning("SceneLoader not found in scene.");
        }
    }

    public bool IsTimerActive()
    {
        return timerActive;
    }
}