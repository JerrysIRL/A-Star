using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField] private Material startMaterial;
    [SerializeField] private Material finishMaterial;
    private GridManager _gridManager;
    private LineRenderer _lineRenderer;
    private PathFinding _pathFinding;

    private Camera cam;
    private Node finish;
    private Node start;

    private void Start()
    {
        cam = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
        _pathFinding = GetComponent<PathFinding>();
        _gridManager = GetComponent<GridManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) SetStartNode();
        if (Input.GetKeyDown(KeyCode.Mouse1)) SetFinishNode();
    }

    private void SetStartNode()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            ClearStartFinish();
            start = _gridManager.GetNodeAtPosition(hit.transform.position);
            start.GetComponent<Renderer>().material = startMaterial;
        }
    }

    private void GenerateAndDisplayPath()
    {
        if (start != null && finish != null)
        {
            var path = _pathFinding.GetPath(start, finish);
            _lineRenderer.positionCount = path.Count;
            _lineRenderer.SetPositions(path.ToArray());
        }
    }

    private void SetFinishNode()
    {
        if (start == null) return;
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            ClearFinish();
            finish = _gridManager.GetNodeAtPosition(hit.transform.position);
            finish.GetComponent<Renderer>().material = finishMaterial;
            GenerateAndDisplayPath();
        }
    }

    private void ClearStartFinish()
    {
        if (start != null)
        {
            start.GetComponent<Renderer>().material.color = Color.white;
            start = null;
        }

        ClearFinish();
        _lineRenderer.positionCount = 0;
    }

    private void ClearFinish()
    {
        if (finish != null)
        {
            finish.GetComponent<Renderer>().material.color = Color.white;
            finish = null;
        }
    }
}