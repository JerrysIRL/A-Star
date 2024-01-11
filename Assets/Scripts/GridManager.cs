using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public float width, height;
    [SerializeField] private Node nodePrefab;
    [SerializeField] [Range(0, 1)] private float walkable;
    [SerializeField] private GameObject[] fillerObjects;


    private Camera _cam;
    public Dictionary<Node, Vector3> Nodes { get; } = new();

    private void Awake()
    {
        InitializeGrid();
        _cam = Camera.main;
    }

    private void InitializeGrid()
    {
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var rand = Random.Range(0f, 1f);
                var pos = new Vector3(x, 0, y);
                var node = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
                node.Position = pos;
                if (rand >= walkable)
                {
                    Instantiate(fillerObjects[Random.Range(0, fillerObjects.Length)], node.Position, Quaternion.identity);
                    continue;
                }

                node.name = $"({x},{y})";
                Nodes.Add(node, pos);
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
        foreach (var node in Nodes)
            node.Key.Reset();
    }

    public Node GetNodeAtPosition(Vector3 pos)
    {
        var tolerance = 0.001f;

        return Nodes.SingleOrDefault(n => Vector3.Distance(n.Value, pos) < tolerance).Key;
    }
}