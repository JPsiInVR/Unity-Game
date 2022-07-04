using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LobbyScreenController : MonoBehaviour
{
    [SerializeField]
    private List<Texture> _sectionPreviews;
    [SerializeField]
    private float _fadeInTransitionDuration = 1;
    [SerializeField]
    private float _fadeOutTransitionDuration = 6;

    private TweenerCore<float, float, FloatOptions> _tweener;
    private MeshRenderer _screenRenderer;

    private void Start()
    {
        _screenRenderer = GetComponent<MeshRenderer>();
    }

    public void DisplayTexture(int section)
    {
        if (_tweener != null && !_tweener.IsComplete()) 
        {
            _tweener.Complete();
        }

        _screenRenderer.material.SetTexture("_ScreenTexture", _sectionPreviews[section]);
        _tweener = _screenRenderer.material.DOFloat(3f, "_EmissionMultiplier", _fadeInTransitionDuration);
    }

    public void ClearScreen()
    {
        if(_tweener != null && !_tweener.IsComplete())
        {
            _tweener.Complete();
        }

        _tweener = _screenRenderer.material.DOFloat(-1, "_EmissionMultiplier", _fadeOutTransitionDuration);
    }
}
