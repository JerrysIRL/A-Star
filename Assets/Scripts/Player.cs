using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Material finishMaterial;
    [SerializeField] GridManager gridManager;
    private Material _defaultNodeMaterial;
    private Camera _cam;
    private LineRenderer _lineRenderer;
    private PathFinding _pathFinding;
    private Node _current;
    private Node _finish;
    
    private void Start()
    {
        _cam = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
        _pathFinding = GetComponent<PathFinding>();
        _current = gridManager.GetNodeAtPosition(new Vector3(0, 0, 0));
        _defaultNodeMaterial = _current.GetComponent<Renderer>().material;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetFinishNode();
        }
        
    }

    private void GenerateAndDisplayPath()
    {
        if (_finish != null)
        {
            var path = _pathFinding.GetPath(_current, _finish);
            _lineRenderer.positionCount = path.Count;
            _lineRenderer.SetPositions(path.ToArray());
        }
    }
    
    private void SetFinishNode()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            ClearFinish();
            _finish = gridManager.GetNodeAtPosition(hit.transform.position);
            _finish.GetComponent<Renderer>().material = finishMaterial;
            GenerateAndDisplayPath();
        }
    }
    
    private void ClearFinish()
    {
        if (_finish != null)
        {
            _finish.GetComponent<Renderer>().material = _defaultNodeMaterial;
            _finish = null;
        }
    }
}
