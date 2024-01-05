using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float width, height;
    [SerializeField] private Node nodePrefab;

    public Dictionary<Node, Vector3> Nodes { get; } = new Dictionary<Node, Vector3>();


    private void Awake()
    {
        InitializeGrid();
    }


    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var pos = new Vector3(x, 0, y);
                var node = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
                node.Position = pos;
                node.name = $"({x},{y})";
                Nodes.Add(node, pos);
            }
        }
    }

    public Node GetNodeAtPosition(Vector3 pos)
    {
        float tolerance = 0.001f;

        return Nodes.SingleOrDefault(n => Vector3.Distance(n.Value, pos) < tolerance).Key;
    }
}