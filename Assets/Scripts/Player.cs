using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private bool _isMoving;
    private int _index = 1;
    private List<Vector3> _path = new List<Vector3>();


    private void Start()
    {
        _cam = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
        _pathFinding = GetComponent<PathFinding>();
        _current = gridManager.GetNodeAtPosition(new Vector3(0, 0, 0));
        _defaultNodeMaterial = _current.GetComponent<Renderer>().material;
    }

    // private void Update()
    // {
    //     // if (_isMoving)
    //     //     return;
    //     //
    //     // if (Input.GetKeyDown(KeyCode.Mouse0))
    //     //     SetFinishNode();
    // }

    private void Update()
    {
        if (_isMoving)
            return;


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetFinishNode();
            _path = _pathFinding.GetPath(_current, _finish);
            DisplayPath(_path);
        }
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Move(_path);
        }
    }

    private void Move(List<Vector3> path)
    {
        var goal = path[^_index];
        transform.position = Vector3.MoveTowards(transform.position, goal, 5 * Time.fixedDeltaTime);
        if (transform.position == goal)
        {
            if (path.Count == _index)
            {
                _isMoving = false;
                _current = _finish; //gridManager.GetNodeAtPosition(transform.position);
                _index = 1;
            }
            else
            {
                _index++;
            }
        }
    }

    private void DisplayPath(List<Vector3> path)
    {
        _lineRenderer.positionCount = path.Count;
        _lineRenderer.SetPositions(path.ToArray());
    }

    private void SetFinishNode()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            ClearFinish();
            _finish = gridManager.GetNodeAtPosition(hit.transform.position);
            _finish.GetComponent<Renderer>().material = finishMaterial;

            _isMoving = true;
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