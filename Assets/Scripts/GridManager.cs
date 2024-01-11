using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Node nodePrefab;
    [SerializeField] private GameObject[] fillerObjects;

    private float _width, _height;
    private Camera _cam;
    public Dictionary<Node, Vector3> WalkableNodes { get; } = new();

    private void Awake()
    {
        InitializeGrid();
        _cam = Camera.main;
    }

    private void InitializeGrid()
    {
        _width = Settings.Instance.GetWidth();
        _height = Settings.Instance.GetHeight();
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                var rand = Random.Range(0f, 1f);
                var pos = new Vector3(x, 0, y);
                var node = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
                node.Position = pos;
                if (rand >= Settings.Instance.walkableAmount)
                {
                    Instantiate(fillerObjects[Random.Range(0, fillerObjects.Length)], node.Position, Quaternion.identity, transform);
                    continue;
                }

                node.name = $"({x},{y})";
                WalkableNodes.Add(node, pos);
            }
        }
    }

    public Node GetNodeAtScreenPosition()
    {
        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            return GetNodeAtPosition(hit.transform.position);

        return null;
    }

    public void ResetGrid()
    {
        foreach (var node in WalkableNodes)
            node.Key.Reset();
    }

    public Node GetNodeAtPosition(Vector3 pos)
    {
        var tolerance = 0.001f;

        return WalkableNodes.SingleOrDefault(n => Vector3.Distance(n.Value, pos) < tolerance).Key;
    }
}