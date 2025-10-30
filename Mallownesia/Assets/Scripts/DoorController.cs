using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Rotation Parameters")]
    [SerializeField] public Vector3 openRotation = new Vector3(-90, 0, 0);
    public float animationSpeed = 2f;

    [Header("Key Requirement")]
    public KeyItem requiredKey;

    [Header("Door Type Settings")]
    [SerializeField] private bool isKeypadDoor = false;

    private Quaternion closedRotation;
    private Quaternion openRotationQuaternion;
    private bool isOpen = false;

    // --- New fields ---
    private bool playerNear = false;
    private Inventory playerInventory;

    private void Start()
    {
        closedRotation = transform.localRotation;
        openRotationQuaternion = closedRotation * Quaternion.Euler(openRotation);
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory != null)
            {
                ToggleDoor(playerInventory);
            }
            else
            {
                Debug.LogWarning("No player inventory found!");
            }
        }
    }

    public void ToggleDoor(Inventory playerInventory)
    {
        if (isKeypadDoor)
        {
            Debug.Log("This door requires a keypad to open.");
            return;
        }

        if (requiredKey != null && !playerInventory.HasKey(requiredKey))
        {
            Debug.Log("You need the correct key to open this door!");
            return;
        }

        if (requiredKey == null && !isKeypadDoor)
        {
            Debug.Log("The door is unlocked and can be opened.");
        }

        ToggleDoorState();
    }

    public void OpenDoorFromKeypad()
    {
        if (isKeypadDoor && !isOpen)
        {
            ToggleDoorState();
        }
    }

    private void ToggleDoorState()
    {
        StopAllCoroutines();
        Quaternion targetRotation = isOpen ? closedRotation : openRotationQuaternion;
        StartCoroutine(AnimateDoorRotation(targetRotation));
        isOpen = !isOpen;
    }

    private IEnumerator AnimateDoorRotation(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.01f)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                targetRotation,
                Time.deltaTime * animationSpeed * 100f
            );
            yield return null;
        }

        transform.localRotation = targetRotation;
    }

    // --- Trigger detection for player ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            playerInventory = other.GetComponent<Inventory>(); // grab player's inventory
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            playerInventory = null;
        }
    }
}
