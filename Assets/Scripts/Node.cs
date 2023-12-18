using UnityEngine;

public class Node : MonoBehaviour
{
    public (int x, int y) Position;
    [HideInInspector]public Node parent;
    [HideInInspector] public float gCost;
    [HideInInspector] public float hCost;
    [HideInInspector] public float fCost;
}

 