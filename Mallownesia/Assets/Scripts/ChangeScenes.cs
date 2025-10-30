using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScenes : MonoBehaviour
{
    [Header("Assign your panels here in order")]
    public GameObject[] panels; // drag all panels in order (0 = first panel)

    [Header("Optional Buttons (assign in Inspector)")]
    public Button nextButton;
    public Button backButton;

    private int currentPanel = 0;

    private void Start()
    {
        // Ensure all panels are off except the first
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }

        // Assign button listeners if provided
        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextClick);

        if (backButton != null)
            backButton.onClick.AddListener(OnBackClick);
    }

    public void OnNextClick()
    {
        Debug.Log("Next clicked, current panel: " + currentPanel);

        // Hide the current panel
        panels[currentPanel].SetActive(false);

        currentPanel++;

        // If we're past the last panel, load next scene
        if (currentPanel >= panels.Length)
        {
            SceneManager.LoadScene("kat's Scene");
            return;
        }

        // Show the next panel
        panels[currentPanel].SetActive(true);
    }

    public void OnBackClick()
    {
        Debug.Log("Back clicked, current panel: " + currentPanel);

        // Prevent going below 0
        if (currentPanel <= 0)
            return;

        // Hide current and show previous
        panels[currentPanel].SetActive(false);
        currentPanel--;
        panels[currentPanel].SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
