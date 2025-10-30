using TMPro;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    ///////////////////////// REMOTE //////////////////////////
    [Header("References")]
    public GameObject PickObjct; // 3D remote
    public GameObject remote;    // remote UI panel
    public TextMeshProUGUI interact; // "Press E"

    private bool holdingRemote = false;
    private bool canPickUp = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (holdingRemote)
            {
                // Drop remote
                DropRemote();
            }
            else if (canPickUp)
            {
                // Pick up remote
                PickUpRemote();
            }
        }
    }

    private void PickUpRemote()
    {
        // Hide the 3D remote in the world
        if (PickObjct != null)
        {
            PickObjct.SetActive(false);
            // Optional: You might want to parent it to the player or move it
            // PickObjct.transform.SetParent(transform); // Attach to player
            // PickObjct.transform.localPosition = new Vector3(0, 0, 1); // Position in front
        }

        // Show the remote UI
        if (remote != null)
        {
            remote.SetActive(true);

            // If you're using the RemoteControl script's SetUIActive method
            RemoteControl remoteControl = remote.GetComponent<RemoteControl>();
            if (remoteControl != null)
            {
                remoteControl.SetUIActive(true);
            }
        }

        // Update UI text
        if (interact != null)
        {
            interact.text = "Press E to drop Remote";
        }

        holdingRemote = true;

        // Hide the "Press E" prompt if you want, or keep it showing drop instructions
        // interact.gameObject.SetActive(true); // Keep it visible for drop instructions
    }

    private void DropRemote()
    {
        // Show the 3D remote in the world again
        if (PickObjct != null)
        {
            PickObjct.SetActive(true);
            // Optional: Reset parent and position if you moved it
            // PickObjct.transform.SetParent(null); // Detach from player
            // Place it in front of the player when dropping
            PickObjct.transform.position = transform.position + transform.forward * 1.5f;
        }

        // Hide the remote UI
        if (remote != null)
        {
            remote.SetActive(false);

            // If you're using the RemoteControl script's SetUIActive method
            RemoteControl remoteControl = remote.GetComponent<RemoteControl>();
            if (remoteControl != null)
            {
                remoteControl.SetUIActive(false);
            }
        }

        // Update UI text or hide it
        if (interact != null)
        {
            interact.gameObject.SetActive(false);
        }

        holdingRemote = false;
        canPickUp = false; // Reset pickup flag
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            if (interact != null)
            {
                interact.text = "Press E to pick up Remote";
                interact.gameObject.SetActive(true);
                Debug.Log("CAN PICK UP REMOTE");
            }
            canPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            if (!holdingRemote && interact != null)
            {
                interact.gameObject.SetActive(false);
            }
            canPickUp = false;
        }
    }
}
