using System.Collections;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    [Header("Drawer/Chest Settings")]
    [SerializeField] private bool isChest = false;
    [SerializeField] private Transform lid;
    [SerializeField] private Transform hinge;
    [SerializeField] private Vector3 openEuler = new Vector3(-90f, 0f, 0f);
    [SerializeField] private Vector3 openPositionOffset = new Vector3(0.0f, 0.0f, 0.5f);
    [SerializeField] private float animationSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition; // <-- added this
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;

    private void Start()
    {
        closedPosition = transform.localPosition;

        if (isChest && hinge != null)
        {
            closedRotation = hinge.localRotation;
            openRotation = closedRotation * Quaternion.Euler(openEuler);
        }
        else
        {
            openPosition = closedPosition + openPositionOffset;
        }
    }

    public void ToggleDrawerOrChest()
    {
        StopAllCoroutines();

        if (isChest && hinge != null)
        {
            StartCoroutine(RotateHinge(isOpen ? closedRotation : openRotation));
        }
        else
        {
            StartCoroutine(MoveDrawer(isOpen ? closedPosition : openPosition));
        }

        isOpen = !isOpen;
    }

    private IEnumerator MoveDrawer(Vector3 target)
    {
        while (Vector3.Distance(transform.localPosition, target) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * animationSpeed);
            yield return null;
        }
        transform.localPosition = target;
    }

    private IEnumerator RotateHinge(Quaternion target)
    {
        while (Quaternion.Angle(hinge.localRotation, target) > 0.01f)
        {
            hinge.localRotation = Quaternion.RotateTowards(hinge.localRotation, target, animationSpeed * 100f * Time.deltaTime);
            yield return null;
        }
        hinge.localRotation = target;
    }
}
