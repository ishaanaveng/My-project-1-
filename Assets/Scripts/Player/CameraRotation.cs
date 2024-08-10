using UnityEngine;
using Cinemachine;

public class CameraRotation : MonoBehaviour
{
    public CinemachineVirtualCamera cam; // Assign your Cinemachine camera component
    public float rotationSpeed = 50f; // Speed of rotation
    public float minYRotation = -45f; // Minimum upward rotation
    public float maxYRotation = 45f; // Maximum downward rotation
    public float minFocalLength = 20f; // Minimum focal length
    public float maxFocalLength = 100f; // Maximum focal length
    public float focalLengthChangeSpeed = 10f; // Speed of focal length change

    private float currentYRotation = 0f; // Current Y rotation of the camera

    void Start()
    {
        // Initialize camera rotation and focal length
        currentYRotation = cam.transform.eulerAngles.x; // Initialize with current camera rotation
        cam.m_Lens.FieldOfView = 60f; // Set initial field of view (or focal length)
    }

    void Update()
    {
        // Check for right mouse button input for rotation
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float mouseY = Input.GetAxis("Mouse Y");
            currentYRotation -= mouseY * rotationSpeed * Time.deltaTime;
            currentYRotation = Mathf.Clamp(currentYRotation, minYRotation, maxYRotation);
            cam.transform.rotation = Quaternion.Euler(currentYRotation, cam.transform.eulerAngles.y, 0);
        }

        // Change the focal length using mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            // Adjust the field of view based on scroll input
            float newFOV = cam.m_Lens.FieldOfView - scrollInput * focalLengthChangeSpeed;
            newFOV = Mathf.Clamp(newFOV, minFocalLength, maxFocalLength);
            cam.m_Lens.FieldOfView = newFOV;
        }
    }
}
