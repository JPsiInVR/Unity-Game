using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Image Background { get => _background; set => _background = value; }
    public TextMeshProUGUI Text { get => _text; set => _text = value; }

    private Image _background;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _background = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }
}