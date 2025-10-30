using UnityEngine;

public class DrawerTrigger : MonoBehaviour
{
    private DrawerController drawer;

    private void Start()
    {
        // Get the DrawerController on the same object
        drawer = GetComponent<DrawerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the object in the trigger is the player
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            drawer.ToggleDrawerOrChest();
        }
    }
}
