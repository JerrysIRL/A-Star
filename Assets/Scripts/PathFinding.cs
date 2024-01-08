using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] GridManager gridManager;

    List<Node> _openList = new List<Node>();
    HashSet<Node> _closedList = new HashSet<Node>();

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

                n.parent = currentNode;
                n.gCost = currentNode.gCost + CalculateDistance(n.Position, currentNode.Position);
                n.hCost = CalculateDistance(n.Position, finish.Position);
                n.fCost = n.gCost + n.hCost;
                n.text.text = $"{n.gCost:F1} , {n.hCost:F1}  \n {n.fCost:F1}";

                if (_openList.Contains(n))
                {
                    continue;
                }

                _openList.Add(n);
            }
        }

        return null;
    }

    private List<Vector3> ConstructPath(Node input)
    {
        List<Vector3> path = new List<Vector3>();
        var current = input;
        do
        {
            path.Add(current.Position + Vector3.up / 10);
            current = current.parent;

        } while (current != null);

        return path;
    }

    private List<Node> GetNeighbours(Node input)
    {
        List<Node> temp = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                var n = gridManager.GetNodeAtPosition(new Vector3(input.Position.x + x, 0, input.Position.z + y));
                if (n != null)
                    temp.Add(n);
            }
        }

        return temp;
    }

    float CalculateDistance(Vector3 start, Vector3 end) => Vector3.Distance(start, end);
}