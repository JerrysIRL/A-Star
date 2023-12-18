using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
            //Debug.Log(currentNode);

            openList = openList.OrderBy(node => node.fCost).ToList();
            var currentNode = openList[0];
            // for (int i = 0; i < openList.Count; i++)
            // {
            //     if (openList[i].fCost > currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost > currentNode.hCost)
            //         currentNode = openList[i];
            // }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == finish)
            {
                Debug.Log("found path");
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
            pointsList.Add(new Vector3(node.Position.x, node.Position.y, -0.1f));
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

                var n = gridManager.GetNodeAtPosition(input.Position.x + x, input.Position.y + y);
                if (n != null)
                    temp.Add(n);
            }
        }

        return temp;
    }

    private float CalculateHeuristic((int x, int y) point1, (int x, int y) point2)
    {
        int dx = point2.x - point1.x;
        int dy = point2.y - point1.y;

        // Use Mathf.RoundToInt to ensure integer result
        return Mathf.Sqrt(dx * dx + dy * dy);
    }
    
    float CalculateDistance((int x,int y) start , (int x, int y) end ) => Vector2Int.Distance(new Vector2Int(start.x, start.y), new Vector2Int(end.x, end.y));
}