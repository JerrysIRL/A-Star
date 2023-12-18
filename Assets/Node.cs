using UnityEngine;

public class Node : MonoBehaviour
{
    public (int x, int y) Position;
    [HideInInspector]public Node parent;
    [HideInInspector] public int gCost;
    [HideInInspector] public int hCost; 
    public int fCost => gCost + hCost;
}

 