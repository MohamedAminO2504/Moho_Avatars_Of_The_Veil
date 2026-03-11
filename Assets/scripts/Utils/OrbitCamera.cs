using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;

    [Header("Rotation")]
    public float rotationSpeed = 0.2f;
    public float minYAngle = -20f;
    public float maxYAngle = 60f;

    [Header("Zoom")]
    public float zoomSpeed = 5f;
    public float minDistance = 2f;
    public float maxDistance = 15f;

    [Header("Pan")]
    public float panSpeed = 0.01f;

    private float distance = 5f;
    private float currentX = 0f;
    private float currentY = 20f;

    void Start()
    {
        if (target == null)
        {
                Debug.LogError("OrbitCamera : Target manquant !");
            enabled = false;
            return;
        }

        distance = Vector3.Distance(transform.position, target.position);
    }

    void LateUpdate()
    {
        HandleRotation();
        HandleZoom();
        HandlePan();
        UpdatePosition();
    }

    void HandleRotation()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();

            currentX += delta.x * rotationSpeed;
            currentY -= delta.y * rotationSpeed;
            currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
        }
    }

    void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll != 0)
        {
            distance -= scroll * zoomSpeed * Time.deltaTime;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }
    }

    void HandlePan()
    {
        if (Mouse.current.middleButton.isPressed)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();

            Vector3 right = transform.right;
            Vector3 up = transform.up;

            Vector3 move = (-right * delta.x + -up * delta.y) * panSpeed;

            target.position += move;
        }
    }

    void UpdatePosition()
    {
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = rotation * new Vector3(0, 0, -distance);

        transform.position = target.position + direction;
        transform.LookAt(target.position);
    }
}
