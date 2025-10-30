/*using TMPro;
using UnityEngine;

public class PickUpObject1 : MonoBehaviour
{
    [Header("References")]
    public GameObject PickObjct; // 3D remote
    public GameObject remote;    // remote UI panel
    public TextMeshProUGUI interact; // "Press E"
    public GameObject bookPanel;
    public GameObject InvitePanel;
    public Inventory playerInventory; // link player’s Inventory script

    
    private bool holdingRemote = false;
    private bool nearBook = false;
    private bool nearInvite = false;
    private KeyItem nearbyKey = null; // key currently in trigger
    private KeypadController nearbyKeypad = null; // keypad currently in trigger

    private void Awake()
    {
        if (remote != null) remote.SetActive(false);
        if (interact != null) interact.gameObject.SetActive(false);
        if (bookPanel != null) bookPanel.SetActive(false);
        if (InvitePanel != null) InvitePanel.SetActive(false);   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canPickUp && !holdingRemote)
            {
                // Pick up remote
                if (remote != null) remote.SetActive(true);
                if (PickObjct != null) PickObjct.SetActive(false);

                Debug.Log("Picked up remote");
                holdingRemote = true;
                canPickUp = false;

                if (interact != null) interact.gameObject.SetActive(false);
            }
            else if (holdingRemote)
            {
                // Drop remote
                if (remote != null) remote.SetActive(false);
                if (PickObjct != null) PickObjct.SetActive(true);

                holdingRemote = false;
            }
            else if (nearInvite)
            {
                Debug.Log("Interacted with book!");
                if (InvitePanel != null) InvitePanel.SetActive(true);
                if (interact != null) interact.gameObject.SetActive(false);
            }
            else if (nearBook)
            {
                Debug.Log("Interacted with book!");
                if (bookPanel != null) bookPanel.SetActive(true);
                if (interact != null) interact.gameObject.SetActive(false);
            }
           
            else if (nearbyKey != null)
            {
                // Pick up key
                playerInventory.AddKey(nearbyKey);
                Debug.Log("Picked up key: " + nearbyKey.keyID);
                Destroy(nearbyKey.gameObject);
                if (interact != null) interact.gameObject.SetActive(false);
                nearbyKey = null;
            }
            else if (nearbyKeypad != null)
            {
                // Interact with keypad
                Debug.Log("Interacted with keypad!");
                //nearbyKeypad.OpenKeypadPanel(); // <-- Make sure your KeypadItem has this method
                if (interact != null) interact.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            if (interact != null)
            {
                interact.text = "Press E to pick up Remote";
                interact.gameObject.SetActive(true);
                Debug.Log("PICKED UP REMOTE");
            }
            canPickUp = true;
        }

        /*if (other.CompareTag("InvitePoster"))
        {
            if (interact != null)
            {
                
                interact.gameObject.SetActive(true);
                Debug.Log("INVITE OPEN");
            }
            nearInvite = true;
        }

        if (other.CompareTag("Book"))
        {
            if (interact != null)
            {
                interact.text = "Press E to read Book";
                interact.gameObject.SetActive(true);
                Debug.Log("BOOK OPEN");
            }
            nearBook = true;
        }

        if (other.CompareTag("Key"))
        {
            nearbyKey = other.GetComponent<KeyItem>();
            if (interact != null)
            {
                interact.text = "Press E to pick up Key";
                interact.gameObject.SetActive(true);
                Debug.Log("KEY PICKED UP");
            }
        }

        if (other.CompareTag("Keypad"))
        {
            nearbyKeypad = other.GetComponent<KeypadController>();
            if (interact != null)
            {
                interact.text = "INTERACT USING numbers (BACKSPACE = Clear, ENTER =Enter)";
                interact.gameObject.SetActive(true);
                Debug.Log("KEYPAD IN USE");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            if (!holdingRemote && interact != null)
                interact.gameObject.SetActive(false);
            canPickUp = false;
        }

        if (other.CompareTag("Book"))
        {
            if (interact != null) interact.gameObject.SetActive(false);
            nearBook = false;
        }

        if (other.CompareTag("Key") && nearbyKey != null && other.GetComponent<KeyItem>() == nearbyKey)
        {
            if (interact != null) interact.gameObject.SetActive(false);
            nearbyKey = null;
        }

        if (other.CompareTag("Keypad") && nearbyKeypad != null && other.GetComponent<KeypadController>() == nearbyKeypad)
        {
            if (interact != null) interact.gameObject.SetActive(false);
            nearbyKeypad = null;
        }
    }

    public void CloseBookPanel()
    {
        if (bookPanel != null)
            bookPanel.SetActive(false);
    }
}*/
