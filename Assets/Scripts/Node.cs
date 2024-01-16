using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] public TMP_Text text;
    [HideInInspector] public Node parent;
    [HideInInspector] public float gCost;
    [HideInInspector] public float hCost;
    [HideInInspector] public float fCost;
    public Vector3 position;

    public Node(Vector3 position)
    {
        this.position = position;
    }

    public int AdditionalCost { get; private set; }

    public void Reset()
    {
        gCost = 0;
        hCost = 0;
        fCost = 0;
        parent = null;
        text.text = null;
    }

    public void SetAdditionalCost(int value)
    {
        AdditionalCost = value;
    }
}