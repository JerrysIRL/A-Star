using System;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField] private Material startMaterial;
    [SerializeField] private Material finishMaterial;
    
    private Camera cam;
    private LineRenderer _lineRenderer;
    private PathFinding _pathFinding;
    private GridManager _gridManager;
    private Node start;
    private Node finish;

    private void Start()
    {
        cam = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
        _pathFinding = GetComponent<PathFinding>();
        _gridManager = GetComponent<GridManager>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (start == null)
                {
                    start = _gridManager.GetNodeAtPosition((int)MathF.Round(hit.transform.position.x), (int)MathF.Round(hit.transform.position.y));
                    start.GetComponent<Renderer>().material = startMaterial;
                }
                else  if (start != null && finish == null)
                {
                    finish = _gridManager.GetNodeAtPosition((int)MathF.Round(hit.transform.position.x), (int)MathF.Round(hit.transform.position.y));
                    finish.GetComponent<Renderer>().material = finishMaterial;
                     Debug.Log("start: " + start + "finish : " + finish);
                     var path = _pathFinding.GetPath(start, finish);
                     {
                         _lineRenderer.positionCount = path.Count;
                         _lineRenderer.SetPositions(path.ToArray()); 
                     }
                    
                    
                }
            }
        }
    }
} 