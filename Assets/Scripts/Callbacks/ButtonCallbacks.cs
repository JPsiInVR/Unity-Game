using UnityEngine;
using TMPro;

/// <summary>
/// This class contains all button callbacks.
/// </summary>
public class ButtonCallbacks : MonoBehaviour
{
    [SerializeField]
    private bool _rememberStateWhenDisabled = true;

    private TextMeshProUGUI _text;
    private Animator _animator;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();

        _animator = GetComponent<Animator>();
        if(_animator != null)
        {
            _animator.keepAnimatorControllerStateOnDisable = _rememberStateWhenDisabled;
        }
    }

    public void ForceTextUpdate()
    {
        _text.UpdateFontAsset();
    }
}
