using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Node : MonoBehaviour
{
    public Vector3 Position;
    [SerializeField] public TMP_Text text;
    [HideInInspector]public Node parent;
    [HideInInspector] public float gCost;
    [HideInInspector] public float hCost;
    [HideInInspector] public float fCost;

    public void Reset()
    {
        gCost = 0;
        hCost = 0;
        fCost = 0;
        parent = null;
        text.text = null;
    }

    public Node(Vector3 position)
    {
        Position = position;
    }
    
}

 