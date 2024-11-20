using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // The point to rotate around
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float zoomSpeed = 2.0f;
    [SerializeField] private float minZoomDistance = 5.0f;
    [SerializeField] private float maxZoomDistance = 20.0f;
    [SerializeField] private float minYAngle = -45.0f;
    [SerializeField] private float maxYAngle = 45.0f;
    [SerializeField] private float minZAngle = -45.0f;
    [SerializeField] private float maxZAngle = 45.0f;
    [SerializeField] private float minXPosition = -10.0f;
    [SerializeField] private float maxXPosition = 10.0f;

    private float currentYAngle = 0.0f;
    private float currentZAngle = 0.0f;
    private float currentZoomDistance = 10.0f;

    void Update()
    {
        RotateCamera();
        MoveCamera();
        ZoomCamera();
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            //TODO move "Mouse X" to variable
            float yRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float zRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

            currentYAngle = Mathf.Clamp(currentYAngle + yRotation, minYAngle, maxYAngle);
            currentZAngle = Mathf.Clamp(currentZAngle - zRotation, minZAngle, maxZAngle);

            transform.position = target.position;
            transform.rotation = Quaternion.Euler(currentZAngle, currentYAngle, 0);
            transform.Translate(Vector3.back * currentZoomDistance); // Adjust distance from target
        }
    }

    void MoveCamera()
    {
        //TODO move "Horizontal" to variable
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(moveX, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, minXPosition, maxXPosition);
        transform.position = newPosition;
    }

    void ZoomCamera()
    {
        //TODO move "Mouse ScrollWheel" to variable
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoomDistance = Mathf.Clamp(currentZoomDistance - scrollInput * zoomSpeed, minZoomDistance, maxZoomDistance);
        transform.Translate(Vector3.forward * scrollInput * zoomSpeed);
    }
}