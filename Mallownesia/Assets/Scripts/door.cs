using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    [Header("Door Animation")]
    public Animator animator;

    // Animation parameter names
    private string isOpenParam = "IsOpen";

    [Header("Key Requirement")]
    public KeyItem requiredKey;

    [Header("Door Type Settings")]
    public bool isKeypadDoor = false;

    [Header("References")]
    public Collider doorCollider; // the main blocking collider 
    public Collider triggerCollider; // child trigger collider 

    private bool isOpen = false;

    // --- Player detection ---
    private bool playerNear = false;
    private Inventory playerInventory;

    // Optional UI prompt
    public GameObject pressEText;

    private void Start()
    {
        if (pressEText != null)
            pressEText.SetActive(false);

        if (doorCollider != null)
        {
            doorCollider.isTrigger = false; // This blocks movement
        }

        if (triggerCollider != null)
        {
            triggerCollider.isTrigger = true; // This detects player
        }
        else
        {
            Debug.LogError("No trigger collider assigned to door!");
        }

        // Auto-find animator if not assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Ensure door starts closed
        if (animator != null)
        {
            animator.SetBool(isOpenParam, false);
        }
    }

    private void Update()
    {
        if (playerNear && playerInventory != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed while near door. Checking inventory...");
            TryOpenDoor(playerInventory);
        }
    }

    private void TryOpenDoor(Inventory inventory)
    {
        Debug.Log("Checking door requirements...");

        if (isKeypadDoor)
        {
            Debug.Log("Door requires a keypad. Cannot open with E.");
            return;
        }

        if (requiredKey != null)
        {
            Debug.Log("Door requires key: " + requiredKey.keyID);
            if (!inventory.HasKey(requiredKey))
            {
                Debug.Log("Player does NOT have the required key: " + requiredKey.keyID);
                return;
            }
            else
            {
                Debug.Log("Player has the required key: " + requiredKey.keyID);
            }
        }
        else
        {
            Debug.Log("Door does not require a key.");
        }

        // Actually open the door when all checks pass
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (isOpen) return; // Don't open if already open

        if (animator != null)
        {
            // Use bool parameter to trigger the open animation
            animator.SetBool(isOpenParam, true);
            Debug.Log("Setting door to open state");
        }
        else
        {
            Debug.LogWarning("No animator found on door!");
        }

        // Disable the blocking collider when door opens
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        isOpen = true;
        Debug.Log("Door opened!");
    }

    public void CloseDoor()
    {
        if (!isOpen) return; // Don't close if already closed
        StartCoroutine(DoorClose_Delay());
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    // --- Trigger detection ---
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has an Inventory component
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            playerNear = true;
            playerInventory = inventory;

            Debug.Log("Player detected near door: " + other.name);

            if (pressEText != null)
                pressEText.SetActive(true);
        }
        else
        {
            Debug.Log("Trigger entered by object without Inventory: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory != null && inventory == playerInventory)
        {
            playerNear = false;
            playerInventory = null;

            Debug.Log("Player exited door trigger zone: " + other.name);

            if (pressEText != null)
                pressEText.SetActive(false);
        }
    }

    IEnumerator DoorClose_Delay()
    {
        yield return new WaitForSeconds(5f);
        if (animator != null)
        {
            // Use bool parameter to trigger the close animation
            animator.SetBool(isOpenParam, false);
            Debug.Log("Setting door to closed state");
        }
        else
        {
            Debug.LogWarning("No animator found on door!");
        }

        // Re-enable the blocking collider when door closes
        if (doorCollider != null)
        {
            doorCollider.enabled = true;
        }

        isOpen = false;
        Debug.Log("Door closed!");
    }
}
