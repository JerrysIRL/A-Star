using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro.Examples;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] Material finishMaterial;
    [SerializeField] Material mudNodeMaterial;
    [SerializeField] GridManager gridManager;
    [SerializeField] float speed = 3;
    private Material _defaultNodeMaterial;
    private LineRenderer _lineRenderer;
    private PathFinding _pathFinding;
    private Node _current;
    private Node _finish;
    private bool _isMoving;
    private int _index = 1;
    private List<Vector3> _path = new List<Vector3>();


    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
        _pathFinding = GetComponent<PathFinding>();
        var rand = gridManager.Nodes.Keys.ToArray()[Random.Range(0, gridManager.Nodes.Count)];
        transform.position = rand.Position;
        _current = gridManager.GetNodeAtPosition(gridManager.Nodes[rand]);
        _defaultNodeMaterial = _current.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (_isMoving && _path != null)
        {
            Move(_path);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (SetFinishNode())
            {
                _path = _pathFinding.GetPath(_current, _finish);
                if (_path != null)
                {
                    DisplayPath(_path);
                }
                else
                {
                    ClearFinish();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            var node = gridManager.GetNodeAtScreenPosition();
            node.GetComponent<Renderer>().material = mudNodeMaterial;
            node.SetAdditionalCost(5);
        }
    }

    private void Move(List<Vector3> path)
    {
        var goal = path[^_index];
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
        var goalRotation = Quaternion.LookRotation(goal - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, 5 * Time.deltaTime);

        if (transform.position == goal)
        {
            if (path.Count == _index)
            {
                _isMoving = false;
                _current = _finish;
                _current.Reset();
                _index = 1;
            }
            else 
                _index++;
            
        }
    }

    private void DisplayPath(List<Vector3> path)
    {
        _lineRenderer.positionCount = path.Count;
        _lineRenderer.SetPositions(path.ToArray());
    }
    
    private bool SetFinishNode()
    { 
        ClearFinish();
        _finish = gridManager.GetNodeAtScreenPosition();
        if(_finish)
        {
            _finish.GetComponent<Renderer>().material = finishMaterial;
            _isMoving = true;
            return true;
        }
        
        return false;
    }

    private void ClearFinish()
    {
        if (_finish != null)
        {
            _finish.GetComponent<Renderer>().material = _defaultNodeMaterial;
            _finish = null;
            _isMoving = false;
        }
    }
}