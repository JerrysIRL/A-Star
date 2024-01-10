using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public float width, height;
    [SerializeField] private Node nodePrefab;
    [SerializeField][Range(0, 1)] private float walkable;
    [SerializeField] GameObject[] fillerObjects;


    private Camera _cam;
    public Dictionary<Node, Vector3> Nodes { get; } = new Dictionary<Node, Vector3>();

    private void Awake()
    {
        InitializeGrid();
        _cam = Camera.main;
    }
    
    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
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
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            return GetNodeAtPosition(hit.transform.position);

        return null;
    }
    
    public void ResetGrid()
    {
        foreach (var node in Nodes)
        {
            node.Key.Reset();
        }
    }
    
    public Node GetNodeAtPosition(Vector3 pos)
    {
        float tolerance = 0.001f;

        return Nodes.SingleOrDefault(n => Vector3.Distance(n.Value, pos) < tolerance).Key;
    }
}