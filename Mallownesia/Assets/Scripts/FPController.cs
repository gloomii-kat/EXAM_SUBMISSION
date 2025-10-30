using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    //public float jumpHeight = 1.5f;
    [Header("Animation Settings")]
    public Animator animator;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 0.2f;
    public float verticalLookLimit = 90f;

    [Header("PickUpSettings")]
    public float pickupRange = 3f;
    public LayerMask pickupLayerMask = ~0; // set in inspector to only include pickupable layers
    public QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Collide;
    public Transform holdPoint;
    private PickUpObject heldObject;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
       // Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();

        /*if (heldObject != null)
        {
            heldObject.MoveToHoldPoint(holdPoint.position);
        }*/
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    /*public void OnPickUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (heldObject == null)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.green);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                Debug.Log("Hit: " + hit.collider.name);
                PickUpObject pickUp = hit.collider.GetComponent<PickUpObject>();

                if (pickUp != null)
                {
                    Debug.Log("Found PickUpObject component on " + hit.collider.name);
                    pickUp.PickUp(holdPoint);
                    heldObject = pickUp;
                }
                else
                {
                    Debug.Log("No PickUpObject script found on " + hit.collider.name);
                }
            }
            else
            {
                Debug.Log("Raycast hit nothing.");
            }
        }
        else
        {
            Debug.Log("Dropping object: " + heldObject.name);
            heldObject.Drop();
            heldObject = null;
        }
    }*/





    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -1.5f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ✅ Animation update
        float speed = move.magnitude;
        animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
    }

    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit,
        verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

}