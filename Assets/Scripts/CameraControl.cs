using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 2f;
    public float maxHeight = 80f;
    public float minHeight = -80f;
    public float distanceFromPlayer = 3f;
    public float transitionSpeed = 5f; // Speed to return behind player

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isMoving = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Detect movement input
        bool movementInput = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;

        if (movementInput)
        {
            isMoving = true; // Start moving, so the camera should reset behind the player
        }
        else if (Input.GetMouseButton(1)) // Right mouse button enables free orbit mode
        {
            isMoving = false;
        }

        float moveMouseX = Input.GetAxis("Mouse X") * sensitivity;
        float moveMouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // If the player is NOT moving, allow free orbit
        if (!isMoving)
        {
            rotationY += moveMouseX;
            rotationX -= moveMouseY;
            rotationX = Mathf.Clamp(rotationX, minHeight, maxHeight);
        }
        else
        {
            // When moving, smoothly reset the camera behind the player
            rotationY = Mathf.Lerp(rotationY, player.eulerAngles.y, Time.deltaTime * transitionSpeed);
            rotationX = Mathf.Lerp(rotationX, 15f, Time.deltaTime * transitionSpeed); // Slight downward angle
        }

        // Calculate camera position
        Vector3 direction = new Vector3(0, 0, -distanceFromPlayer);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.position = player.position + rotation * direction;

        // Always look at the player
        transform.LookAt(player.position);
    }
}
