using UnityEngine;

public class Settings : MonoBehaviour
{
    private int _width = 15, _height = 15;
    public float walkableAmount;
    public static Settings Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetWidth(int value) => _width = value;
    public void SetHeight(int value) => _height = value;
    public int GetWidth() => _width;
    public int GetHeight() => _height;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}