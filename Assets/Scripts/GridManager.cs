using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float width, height;
    [SerializeField] private Node nodePrefab;

    public Dictionary<Node, (int x, int y)> Nodes { get; } = new Dictionary<Node, (int, int)>();


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
                var node = Instantiate(nodePrefab, new Vector2(x, y), Quaternion.identity, transform);
                node.Position = (x, y);
                node.name = $"({x},{y})";
                Nodes.Add(node, (x, y));
            }
        }
    }

    public Node GetNodeAtPosition(int x, int y)
    {
        return Nodes.SingleOrDefault(n => n.Value.x == x && n.Value.y == y).Key;
    }
}