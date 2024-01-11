using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetters : MonoBehaviour
{
    [SerializeField] TMP_InputField widthBox, heightBox;
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener(SetWalkable);
    }

    private void SetWalkable(float value) => Settings.Instance.walkableAmount = value;
    public void SetWidth() => Settings.Instance.SetWidth(int.Parse(widthBox.text));
    public void SetHeight() => Settings.Instance.SetHeight(int.Parse(heightBox.text));
}