using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 2, -4); // Default over-the-shoulder offset
    public float sensitivity = 3f; // Mouse sensitivity
    public float rotationSmoothTime = 0.1f; // Smooth rotation speed

    private Vector3 currentRotation;
    private Vector3 rotationSmoothVelocity;
    private float yaw;
    private float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the game window
        Cursor.visible = false; // Hide the cursor
    }

    void LateUpdate()
    {
        // Get mouse input
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -30f, 40f); // Restrict vertical movement

        // Smooth rotation
        Vector3 targetRotation = new Vector3(pitch, yaw);
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);

        // Apply rotation
        transform.eulerAngles = currentRotation;

        // Keep camera at offset from player
        transform.position = player.position + transform.rotation * offset;
    }
}
