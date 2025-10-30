using UnityEngine;

public class Door2 : MonoBehaviour
{
    [Header("Door Animation")]
    public Animator animator;
    private string isOpenParam = "IsOpen";

    [Header("References")]
    public Collider stopPlayer_collider; //collider to stop player from going through door when closed
    public Collider closeTrigger; // Trigger to close the door

    [Header("UI Prompt")]
    public GameObject pressEText; // "Press E to open" text

    private bool isOpen = false;
    private bool playerNear = false;

    private void Start()
    {
        // Initialize UI
        if (pressEText != null)
            pressEText.SetActive(false);

        // Auto-find animator
        if (animator == null)
            animator = GetComponent<Animator>();

        // Ensure door starts closed
        if (animator != null)
            animator.SetBool(isOpenParam, false);
    }

    private void Update()
    {
        // Open door when E is pressed and player is near open trigger
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (isOpen) return;
        gameObject.GetComponent<Collider>().enabled = false;//disable 'press E text' collider
        stopPlayer_collider.gameObject.SetActive(false);//disable collider that stops player from going through door

        if (animator != null)
        {
            animator.SetBool(isOpenParam, true);
            Debug.Log("SimpleDoor: Playing open animation");
        }

        // Hide press E text
        if (pressEText != null)
        {
            pressEText.SetActive(false);
        }

        isOpen = true;
        SoundManager.PlaySound(SoundType.Door);
        Debug.Log("SimpleDoor: Door opened!");
    }

    public void CloseDoor()
    {
        if (!isOpen) return;

        if (animator != null)
        {
            animator.SetBool(isOpenParam, false);
            Debug.Log("SimpleDoor: Playing close animation");
        }

        isOpen = false;
        SoundManager.PlaySound(SoundType.Door);
        Debug.Log("SimpleDoor: Door closed!");
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    // Open trigger - for opening the door
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            playerNear = true;

            if (pressEText != null)
            {
                pressEText.SetActive(true);
                print("SimpleDoor: Player near open trigger");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            if (pressEText != null)
            {
                pressEText.SetActive(false);
            }

            Debug.Log("SimpleDoor: Player left open trigger");
        }
    }

    // Close trigger - for closing the door (separate method)
    public void OnCloseTriggerEnter()
    {
        CloseDoor();
        Debug.Log("SimpleDoor: Close trigger activated");
    }
}
