using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    public float padding = 1.0f;
    public float tiltAngle = 45.0f;
    public float distanceFromCenter = 10.0f;

    private void Start()
    {
        PositionAndSizeCamera();
    }

    private void PositionAndSizeCamera()
    {
        var bounds = CalculateGridBounds();

        var cameraFOV = CalculateCameraFOV(bounds.size.x, bounds.size.z);

        transform.position = CalculateCameraPosition(bounds, cameraFOV);
        transform.rotation = Quaternion.Euler(tiltAngle, 0, 0);
        Camera.main.fieldOfView = cameraFOV;
    }

    private Vector3 CalculateCameraPosition(Bounds bounds, float cameraFOV)
    {
        var cameraPosition = new Vector3(bounds.center.x, CalculateCameraHeight(bounds, cameraFOV), bounds.center.z - distanceFromCenter);

        return cameraPosition;
    }

    private float CalculateCameraFOV(float width, float height)
    {
        var diagonalSize = Mathf.Sqrt(width * width + height * height);
        var fov = 2.0f * Mathf.Atan(diagonalSize / (2.0f * CalculateCameraZOffset())) * Mathf.Rad2Deg;
        fov = Mathf.Clamp(fov, 1.0f, 179.0f);

        return fov;
    }

    private float CalculateCameraZOffset()
    {
        var settings = Settings.Instance;
        return settings.GetWidth() > settings.GetHeight() ? settings.GetWidth() : settings.GetHeight();
    }

    private float CalculateCameraHeight(Bounds bounds, float cameraFOV)
    {
        var cameraHeight = bounds.size.z / (2.0f * Mathf.Tan(cameraFOV * 0.5f * Mathf.Deg2Rad)) + padding;
        return cameraHeight;
    }

    private Bounds CalculateGridBounds()
    {
        var bounds = new Bounds(Vector3.zero, Vector3.zero);

        foreach (var node in gridManager.WalkableNodes.Keys) bounds.Encapsulate(node.GetComponent<Renderer>().bounds);

        return bounds;
    }
}