using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameSettings gameSettings;
    
    public static Settings Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWidth(int value) => gameSettings.width = value;
    public void SetHeight(int value) => gameSettings.height = value;
    public int GetWidth() => gameSettings.width;
    public int GetHeight() => gameSettings.height;

}