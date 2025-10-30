using TMPro;
using UnityEngine;

public class PickUpObject4 : MonoBehaviour
{
    ///////////////////////// KEY //////////////////////////


    [Header("References")]
    public TextMeshProUGUI interact; // "Press E"
    private KeyItem nearbyKey = null; // key currently in trigger

    private void Awake()
    {
   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
             if (nearbyKey != null)
            {
                // Pick up key
                Debug.Log("Picked up key: " + nearbyKey.keyID);
                Destroy(nearbyKey.gameObject);
                if (interact != null) interact.gameObject.SetActive(false);
                nearbyKey = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            nearbyKey = other.GetComponent<KeyItem>();
            if (interact != null)
            {
                interact.text = "Press E to pick up Key";
                interact.gameObject.SetActive(true);
                Debug.Log("KEY PICKED UP");

                //SoundManager.PlaySound(SoundType.FoundKey);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Key") && nearbyKey != null && other.GetComponent<KeyItem>() == nearbyKey)
        {
            if (interact != null) interact.gameObject.SetActive(false);
            nearbyKey = null;
        }
    }
}
