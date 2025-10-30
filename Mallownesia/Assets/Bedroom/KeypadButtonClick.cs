using UnityEngine;
using UnityEngine.EventSystems;

public class KeypadButtonClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private KeypadButton button;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button != null)
            button.PressButton();
        else
            Debug.LogWarning("Button reference not assigned on " + gameObject.name);
    }
}
