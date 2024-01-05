using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] GridManager gridManager;

    public List<Vector3> GetPath(Node start, Node finish)
    {
        var openList = new List<Node>();
        var closedList = new HashSet<Node>();
        openList.Add(start);

        while (openList.Count > 0)
        {
            openList = openList.OrderBy(node => node.fCost).ToList();
            var currentNode = openList[0];

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == finish)
            {
                return ConstructPath(currentNode);
            }

            var neighbours = GetNeighbours(currentNode);
            foreach (var n in neighbours)
            {
                if (closedList.Contains(n))
                    continue;

                n.parent = currentNode;
                n.gCost = currentNode.gCost + CalculateDistance(n.Position, currentNode.Position);
                n.hCost = CalculateDistance(n.Position, finish.Position);
                n.fCost = n.gCost + n.hCost;

                if (openList.Contains(n))
                {
                    continue;
                }

                openList.Add(n);
            }
        }

        return null;
    }

    private List<Vector3> ConstructPath(Node input)
    {
        List<Node> path = new List<Node>();
        path.Clear();
        var current = input;
        while (current != null)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        List<Vector3> pointsList = new List<Vector3>();
        foreach (var node in path)
        {
            pointsList.Add(node.Position + Vector3.up / 10);
        }

        return pointsList;
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