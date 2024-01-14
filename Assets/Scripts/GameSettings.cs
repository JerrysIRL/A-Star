using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [Range(0.1f, 1f)] public float walkableAmount;
    public int width = 15;
    public int height = 15;
}
