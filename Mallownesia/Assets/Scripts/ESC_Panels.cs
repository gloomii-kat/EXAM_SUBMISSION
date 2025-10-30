using UnityEngine;

public class ESC_Panels : MonoBehaviour
{
    private GameObject panelToClose; 
    
    private void Start()
    {
        // If no panel is specified, default to this GameObject
        if (panelToClose == null)
            panelToClose = gameObject;
    }

    private void Update()
    {
        // When ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle off (close) the panel
            if (panelToClose.activeSelf)
            {
                panelToClose.SetActive(false);
                Debug.Log($"{panelToClose.name} closed with ESC");
            }
        }
    }
}
