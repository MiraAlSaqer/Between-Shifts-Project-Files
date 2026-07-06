using UnityEngine;

public class SeatedView : MonoBehaviour
{
    [Header("Looking Settings")]
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    [Header("Look Constraints")]
    public float xLimit = 60f;
    public float yLimit = 90f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xLimit, xLimit);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -yLimit, yLimit);

        playerCamera.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}