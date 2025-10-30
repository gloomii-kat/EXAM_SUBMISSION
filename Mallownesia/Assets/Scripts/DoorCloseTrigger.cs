using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    [Header("Door Reference")]
    public Door doorToClose;  // Assign the door that should close

    [Header("Trigger Settings")]
    public bool closeOnce = true; // Close only the first time player enters

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorToClose != null)
            {
                Debug.Log("Player entered close trigger, closing door: " + doorToClose.name);
                doorToClose.CloseDoor();

                // Disable trigger if we only want it to work once
                if (closeOnce)
                {
                    GetComponent<Collider>().enabled = false;
                }
            }
            else
            {
                Debug.LogWarning("No door assigned to DoorCloseTrigger!");
            }
        }
    }
}
