using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 Position;
    [HideInInspector]public Node parent;
    [HideInInspector] public float gCost;
    [HideInInspector] public float hCost;
    [HideInInspector] public float fCost;

    public Node(Vector3 position)
    {
        Position = position;
    }
}

 