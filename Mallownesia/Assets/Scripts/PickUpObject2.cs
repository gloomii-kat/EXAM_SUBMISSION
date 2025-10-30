using TMPro;
using UnityEngine;

public class PickUpObject2 : MonoBehaviour
{
    ///////////////////////// BOOKS //////////////////////////

    [Header("References")]
    public TextMeshProUGUI interact; // "Press E"
    public GameObject bookPanel;
    public GameObject key1;

    private bool nearBook = false;

    private void Awake()
    {
        if (interact != null) interact.gameObject.SetActive(false);
        if (bookPanel != null) bookPanel.SetActive(false);
        if (key1 != null) key1.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           if (nearBook)
            {
                Debug.Log("Interacted with book!");
                if (bookPanel != null) bookPanel.SetActive(true);
                if (interact != null) interact.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Book"))
        {
            if (interact != null)
            {
                interact.text = "Press E to read Book";
                interact.gameObject.SetActive(true);
                key1.SetActive(true);
                Debug.Log("BOOK OPEN");
            }
            nearBook = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            if (interact != null) interact.gameObject.SetActive(false);
            nearBook = false;
        }
    }

    public void CloseBookPanel()
    {
        if (bookPanel != null)
        {
            bookPanel.SetActive(false);
            
        }
            
    }
}
