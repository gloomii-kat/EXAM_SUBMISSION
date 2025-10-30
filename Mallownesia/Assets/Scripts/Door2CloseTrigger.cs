using UnityEngine;

public class Door2CloseTrigger : MonoBehaviour
{
    [Header("Door Reference")]
    public Door2 doorToClose;

    [Header("Timer Reference")]
    public Timer2 bedroomTimer; // Reference to the bedroom timer

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Close the door
            if (doorToClose != null)
            {
                doorToClose.OnCloseTriggerEnter();
            }

            // Stop and hide the first level timer
            if (Timer.Instance != null)
            {
                Timer.Instance.StopTimer(); // This now also hides the text
                Debug.Log("First level timer stopped and hidden");
            }

            // Start the bedroom timer
            if (bedroomTimer != null && !bedroomTimer.IsTimerActive())
            {
                bedroomTimer.StartBedroomTimer();
                Debug.Log("Bedroom timer started");
            }
        }
    }
}
