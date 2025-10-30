using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemoteControl : MonoBehaviour
{
    [Header("TV Channels (assign panels / quads)")]
    [SerializeField] private List<GameObject> tvChannels = new List<GameObject>();
    private int currentChannel = 0;

    [Header("Speaker")]
    [SerializeField] private AudioSource speakerAudio;
    private bool speakerOn = false;

    [Header("Optional: UI text for channel/speaker feedback")]
    [SerializeField] private Text channelLabel;
    [SerializeField] private Text speakerLabel;

    [Header("UI root (so we can enable/disable when picked up)")]
    [SerializeField] private GameObject remoteUIRoot;

    [Header("Object to control when Element 2 is active")]
    [SerializeField] private GameObject objectToControl;
    public GameObject BookonShelf;

    private void Start()
    {
        if (remoteUIRoot != null)
            remoteUIRoot.SetActive(false);

        UpdateChannels();
        UpdateSpeakerUI();

        if (BookonShelf != null)
            BookonShelf.SetActive(false);
    }

    private void Update()
    {
        // --- KEYBOARD INPUT HANDLING ---
        if (Input.GetKeyDown(KeyCode.R))
            ChannelLeft();

        if (Input.GetKeyDown(KeyCode.T))
            ChannelRight();

        if (Input.GetKeyDown(KeyCode.F))
            SpeakerOn();

        if (Input.GetKeyDown(KeyCode.G))
            SpeakerOff();

        // --- BOOK VISIBILITY CONTROL ---
        if (BookonShelf != null)
        {
            bool shouldBeActive = (currentChannel == 1);
            if (BookonShelf.activeInHierarchy != shouldBeActive)
                BookonShelf.SetActive(shouldBeActive);
        }
    }

    public void SpeakerOn()
    {
        speakerOn = true;
        if (speakerAudio != null) speakerAudio.UnPause();

       // SoundManager.PlaySound(SoundType.Speaker);
        UpdateSpeakerUI();
    }

    public void SpeakerOff()
    {
        speakerOn = false;
        if (speakerAudio != null) speakerAudio.Pause();

        //SoundManager.PlaySound(SoundType.Button);
        UpdateSpeakerUI();
    }

    public void ChannelLeft()
    {
        Debug.Log("Previous Channel...");
        if (tvChannels == null || tvChannels.Count == 0) return;
        currentChannel = (currentChannel - 1 + tvChannels.Count) % tvChannels.Count;

       // SoundManager.PlaySound(SoundType.Button);
        UpdateChannels();
    }

    public void ChannelRight()
    {
        if (tvChannels == null || tvChannels.Count == 0) return;
        currentChannel = (currentChannel + 1) % tvChannels.Count;

        //SoundManager.PlaySound(SoundType.Button);
        UpdateChannels();
    }

    private void UpdateChannels()
    {
        bool wasElement2Active = tvChannels.Count > 2 && tvChannels[2] != null && tvChannels[2].activeInHierarchy;

        for (int i = 0; i < tvChannels.Count; i++)
            if (tvChannels[i] != null) tvChannels[i].SetActive(i == currentChannel);

        bool isElement2ActiveNow = tvChannels.Count > 2 && tvChannels[2] != null && tvChannels[2].activeInHierarchy;

        if (wasElement2Active != isElement2ActiveNow && objectToControl != null)
            objectToControl.SetActive(isElement2ActiveNow);

        if (channelLabel != null)
            channelLabel.text = tvChannels.Count > 0 ? $"CH {currentChannel + 1} / {tvChannels.Count}" : "No channels";
    }

    private void UpdateSpeakerUI()
    {
        if (speakerLabel != null)
            speakerLabel.text = speakerOn ? "Speaker: On" : "Speaker: Off";
    }

    public void SetUIActive(bool on)
    {
        if (remoteUIRoot != null)
            remoteUIRoot.SetActive(on);
    }
}


