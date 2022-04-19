using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float scrollSpeed = 150f;
    public float minCameraHeight = 10;
    public float maxCameraHeight = 180;
    public float panBoardThickness = 10f;
    private bool _isCameraMovementActive = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) _isCameraMovementActive = !_isCameraMovementActive;
        if (!_isCameraMovementActive) return;
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoardThickness)
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("a") || Input.mousePosition.x <= panBoardThickness)
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("s") || Input.mousePosition.y <= panBoardThickness)
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoardThickness)
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        var scrollDelta = Input.mouseScrollDelta.y * scrollSpeed * Time.deltaTime;
        var updatedCameraPosition = transform.position;
        updatedCameraPosition.y = Mathf.Clamp(updatedCameraPosition.y - scrollDelta, minCameraHeight, maxCameraHeight);
        transform.position = updatedCameraPosition;
    }
}