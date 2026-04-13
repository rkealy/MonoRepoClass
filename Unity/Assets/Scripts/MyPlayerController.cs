using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform respawnPoint;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float turningSpeed = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f;
    private float verticalVelocity;

    [Header("Coyote Time Settings")]
    [SerializeField] private float coyoteTime = 0.1f;
    private float lastGroundedTime;

    private float moveInput;
    private float turnInput;
    private bool hasJumped;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -25f)
            Respawn();

        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if (controller.isGrounded)
            lastGroundedTime = Time.time;

        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0; right.y = 0;
        forward.Normalize(); right.Normalize();
        Vector3 horizontalDir = forward * moveInput + right * turnInput;

        // Normalize only if non-zero to prevent jump issues
        if (horizontalDir.sqrMagnitude > 1f)
            horizontalDir.Normalize();

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        bool canUseCoyote = Time.time - lastGroundedTime <= coyoteTime;

        if (controller.isGrounded && verticalVelocity <= 0f)
        {
            verticalVelocity = -0.1f;
            hasJumped = false;

            if (Input.GetButtonDown("Jump") && canUseCoyote)
            {
                verticalVelocity = Mathf.Sqrt(2f * jumpHeight * gravity);
                hasJumped = true;
            }
        }
        else
        {
            verticalVelocity -= gravity * 2f * Time.deltaTime;
        }

        // Apply vertical and horizontal movement separately
        Vector3 moveVector = horizontalDir * currentSpeed;
        moveVector.y = verticalVelocity;
        controller.Move(moveVector * Time.deltaTime);

        if (horizontalDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(horizontalDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turningSpeed);
        }
    }


    private void Respawn()
    {
        if (respawnPoint != null)
        {

            controller.enabled = false;
            transform.position = respawnPoint.position;
            controller.enabled = true;
        }
        else
        {
            Debug.LogWarning("RespawnPoint not assigned!");
        }
    }

    public void SetRespawnPoint(Transform newPoint)
    {
        respawnPoint = newPoint;
    }

}

