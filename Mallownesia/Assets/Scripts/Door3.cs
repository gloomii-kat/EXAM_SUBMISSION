using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door3 : MonoBehaviour
{
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

        // Disable the blocking collider when door opens
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        isOpen = true;
        SceneManager.LoadScene(3);
        Debug.Log("Door opened!");
    }

    public void CloseDoor()
    {
        if (!isOpen) return; // Don't close if already closed

        // Re-enable the blocking collider when door closes
        if (doorCollider != null)
        {
            doorCollider.enabled = true;
        }

        isOpen = false;
        Debug.Log("Door closed!");
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
}
