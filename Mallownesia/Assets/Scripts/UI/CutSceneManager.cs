using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

#region Class Description
/*
 *  Manages cutscene slides with optional speaker names and dialogue typing.
 */
#endregion

public class CutSceneManager : MonoBehaviour
{
    #region Fields

    [System.Serializable]
    public class Slide
    {
        public Sprite image;
        public string speakerName;
        [TextArea(2, 5)] public string dialogue;
        [Header("Options")]
        public bool hideSpeakerName; // New bool to control speaker name visibility
    }

    [Header("Slide Settings")]
    public Slide[] slides;

    [Header("UI References")]
    public GameObject speakerNameBG;
    public Image slideImage;
    public TMP_Text speakerNameText;
    public TMP_Text dialogueText;

    [Header("Options")]
    public string nextSceneName;
    public float typeSpeed = 0.03f;

    private int currentIndex = 0;
    private Coroutine typingCoroutine;

    #endregion

    #region Initializations

    private void Start()
    {
        ShowSlide(0);
    }

    #endregion

    #region Updates

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = slides[currentIndex].dialogue;
                typingCoroutine = null;
            }
            else
            {
                NextSlide();
            }
        }
    }

    private void ShowSlide(int index)
    {
        if (index < 0 || index >= slides.Length) return;

        slideImage.sprite = slides[index].image;

        // Hide or show the speaker name object based on the slide setting
        if (slides[index].hideSpeakerName)
        {
            speakerNameText.gameObject.SetActive(false);
            speakerNameBG.gameObject.SetActive(false);
        }
        else
        {
            speakerNameText.gameObject.SetActive(true);
            speakerNameBG.gameObject.SetActive(true);
            speakerNameText.text = slides[index].speakerName;
        }

        dialogueText.text = "";

        typingCoroutine = StartCoroutine(TypeDialogue(slides[index].dialogue));
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        foreach (char c in dialogue)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        typingCoroutine = null;
    }

    private void NextSlide()
    {
        currentIndex++;
        if (currentIndex >= slides.Length)
        {
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        ShowSlide(currentIndex);
    }

    #endregion
}
