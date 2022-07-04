using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class ExpandableMenuItem : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Image _image;
    private TextMeshProUGUI _text;
    private ParticleSystem _particleSystem;

    public RectTransform RectTransform => _rectTransform;
    public Image Image => _image;
    public TextMeshProUGUI Text => _text;
    public ParticleSystem ParticleSystem => _particleSystem;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
}
