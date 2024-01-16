using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    private readonly HashSet<Node> _closedList = new();

    private List<Node> _openList = new();

    public List<Vector3> GetPath(Node start, Node finish)
    {
        gridManager.ResetGrid();
        _openList.Clear();
        _closedList.Clear();
        _openList.Add(start);

        while (_openList.Count > 0)
        {
            _openList = _openList.OrderBy(node => node.fCost).ToList();
            var currentNode = _openList[0];

            _openList.Remove(currentNode);
            _closedList.Add(currentNode);

            if (currentNode == finish)
            {
                return ConstructPath(currentNode);
            }

            var neighbours = GetNeighbours(currentNode);
            foreach (var n in neighbours)
            {
                if (_closedList.Contains(n))
                    continue;

                var tentativeGCost = currentNode.gCost + Vector3.Distance(n.position, currentNode.position);

                if (!_openList.Contains(n) || tentativeGCost < n.gCost)
                {
                    n.parent = currentNode;
                    n.gCost = tentativeGCost;
                    n.hCost = Vector3.Distance(n.position, finish.position);
                    n.fCost = n.gCost + n.hCost + n.AdditionalCost;
                    n.text.text = $"<color=red>{n.gCost:F1}</color>, <color=green>{n.hCost:F1}</color> \n <color=blue>{n.fCost:F1}</color>";

                    _openList.Add(n);
                }
            }
        }

        return null;
    }

    private List<Vector3> ConstructPath(Node input)
    {
        var path = new List<Vector3>();
        var current = input;
        do
        {
            path.Add(current.position + Vector3.up / 10);
            current = current.parent;
        } while (current != null);

        return path;
    }

    private List<Node> GetNeighbours(Node input)
    {
        var temp = new List<Node>();
        for (var x = -1; x <= 1; x++)
        for (var y = -1; y <= 1; y++)
        {
            if (x == 0 && y == 0)
                continue;

            var n = gridManager.GetNodeAtPosition(new Vector3(input.position.x + x, 0, input.position.z + y));
            if (n != null)
                temp.Add(n);
        }

        return temp;
    }
}