using TMPro;
using UnityEngine;

public class PickUpObject5 : MonoBehaviour
{
    ///////////////////////// KEYPAD //////////////////////////


    [Header("References")]

    public TextMeshProUGUI interact; // "Press E"
    private KeypadController nearbyKeypad = null; // keypad currently in trigger

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           if (nearbyKeypad != null)
            {
                // Interact with keypad
                Debug.Log("Interacted with keypad!");
                //nearbyKeypad.OpenKeypadPanel();
                if (interact != null) interact.gameObject.SetActive(false);
                
                //SoundManager.PlaySound(SoundType.Door);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Keypad"))
        {
            nearbyKeypad = other.GetComponent<KeypadController>();
            if (interact != null)
            {
                interact.text = "INTERACT [NUMPAD] (BACKSPACE = CLEAR; ENTER )";
                interact.gameObject.SetActive(true);
                Debug.Log("KEYPAD IN USE");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Keypad"))
        {
            interact.gameObject.SetActive(false);
            nearbyKeypad = null;
        }
    }
}
