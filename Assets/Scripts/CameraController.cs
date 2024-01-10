using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager; 
    public float padding = 1.0f; 
    public float tiltAngle = 45.0f;
    public float distanceFromCenter = 10.0f;

    void Start()
    {
        PositionAndSizeCamera();
    }

    void PositionAndSizeCamera()
    {
        Bounds bounds = CalculateGridBounds();
        
        float cameraFOV = CalculateCameraFOV(bounds.size.x, bounds.size.z);
        
        transform.position = CalculateCameraPosition(bounds, cameraFOV);
        transform.rotation = Quaternion.Euler(tiltAngle, 0, 0);
        Camera.main.fieldOfView = cameraFOV;
    }

    Vector3 CalculateCameraPosition(Bounds bounds, float cameraFOV)
    {
        Vector3 cameraPosition = new Vector3(bounds.center.x, CalculateCameraHeight(bounds, cameraFOV), bounds.center.z - distanceFromCenter);

        return cameraPosition;
    }

    float CalculateCameraFOV(float width, float height)
    {
        float diagonalSize = Mathf.Sqrt(width * width + height * height);
        float fov = 2.0f * Mathf.Atan(diagonalSize / (2.0f * CalculateCameraZOffset())) * Mathf.Rad2Deg;
        fov = Mathf.Clamp(fov, 1.0f, 179.0f);

        return fov;
    }

    float CalculateCameraZOffset()
    {
        return _gridManager.width > _gridManager.height ? _gridManager.width : _gridManager.height;
    }

    float CalculateCameraHeight(Bounds bounds, float cameraFOV)
    {
        float cameraHeight = bounds.size.z / (2.0f * Mathf.Tan(cameraFOV * 0.5f * Mathf.Deg2Rad)) + padding;
        return cameraHeight;
    }

    Bounds CalculateGridBounds()
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        foreach (Node node in _gridManager.Nodes.Keys)
        {
            bounds.Encapsulate(node.GetComponent<Renderer>().bounds);
        }

        return bounds;
    }
}