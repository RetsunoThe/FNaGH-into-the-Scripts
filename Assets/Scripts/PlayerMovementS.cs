using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementS : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;

    [Header("Movement")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float crouchSpeed = 3f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    [Header("Look")]
    public float lookSpeed = 0.1f;   // Mouse delta is in pixels, so this is much smaller than before
    public float lookXLimit = 45f;

    [Header("Crouch")]
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;
    public bool canMove = true;
    public bool canWalk = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;
        if (keyboard == null || mouse == null) return;

        // --- Input ---
        Vector2 moveInput = Vector2.zero;
        if (canWalk)
        {
            if (keyboard.wKey.isPressed) moveInput.y += 1f;
            if (keyboard.sKey.isPressed) moveInput.y -= 1f;
            if (keyboard.dKey.isPressed) moveInput.x += 1f;
            if (keyboard.aKey.isPressed) moveInput.x -= 1f;
        }
        
        moveInput = Vector2.ClampMagnitude(moveInput, 1f);

        bool isRunning = keyboard.leftShiftKey.isPressed;
        bool isCrouching = keyboard.rKey.isPressed;

        // --- Speed ---
        float speed = isCrouching ? crouchSpeed : (isRunning ? runSpeed : walkSpeed);

        // --- Horizontal movement ---
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        float verticalVelocity = moveDirection.y;

        if (canMove)
            moveDirection = (forward * moveInput.y + right * moveInput.x) * speed;
        else
            moveDirection = Vector3.zero;

        // --- Jump & gravity ---
        if (characterController.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f; // keeps the controller snapped to the ground

        if (keyboard.spaceKey.wasPressedThisFrame && canMove && characterController.isGrounded)
            verticalVelocity = jumpPower;

        verticalVelocity -= gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;

        // --- Crouch ---
        characterController.height = isCrouching ? crouchHeight : defaultHeight;

        characterController.Move(moveDirection * Time.deltaTime);

        // --- Look ---
        if (canMove)
        {
            Vector2 mouseDelta = mouse.delta.ReadValue();

            rotationX -= mouseDelta.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

            transform.Rotate(0f, mouseDelta.x * lookSpeed, 0f);
        }
    }
}