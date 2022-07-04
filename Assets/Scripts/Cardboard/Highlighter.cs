using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(CardboardInteractable))]
public class Highlighter : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float _minOutlineThickness;

    [SerializeField]
    [Range(0, 1)]
    private float _maxOutlineThickness;

    [SerializeField]
    private Color _outlineColor;

    [SerializeField]
    private float _animationDelay;

    [SerializeField]
    private Animator _animator;

    private CardboardInteractable _cardboardInteractable;
    private WaitForSeconds _waitTime;
    private Renderer _renderer;
    private GameObject _outline;
    private bool _showOutline;

    private void Start()
    {
        _waitTime = new WaitForSeconds(_animationDelay);
        _outline = gameObject.transform.GetChild(0).gameObject;
        _renderer = _outline.GetComponent<Renderer>();
        _animator = GetComponent<Animator>();
        _cardboardInteractable = GetComponent<CardboardInteractable>();
        _showOutline = false;
        _cardboardInteractable.onSelectEnter.AddListener(HideOutline);
        _cardboardInteractable.onSelectExit.AddListener(ShowOutline);
        _cardboardInteractable.onHoverEnter.AddListener(ShowOutline);
        _cardboardInteractable.onHoverExit.AddListener(HideOutline);
    }

    private void OnDestroy()
    {
        _cardboardInteractable.onSelectEnter.RemoveListener(HideOutline);
        _cardboardInteractable.onSelectExit.RemoveListener(ShowOutline);
        _cardboardInteractable.onHoverEnter.RemoveListener(ShowOutline);
        _cardboardInteractable.onHoverExit.RemoveListener(HideOutline);
    }

    private void ShowOutline()
    {
        _showOutline = true;
        _outline.SetActive(_showOutline);
        _animator.SetBool(Constants.HighlighterAnimatorParameterName, _showOutline);

        if (_showOutline)
        {
            StartCoroutine(AnimateOutline());
        }
    }

    private void HideOutline()
    {
        _showOutline = false;
        _outline.SetActive(_showOutline);
        _animator.SetBool(Constants.HighlighterAnimatorParameterName, _showOutline);

        if (_showOutline)
        {
            StartCoroutine(AnimateOutline());
        }
    }

    private IEnumerator AnimateOutline()
    {
        while (_showOutline)
        {
            yield return _waitTime;

            if (_renderer.material.HasFloat(Constants.HighlighterMaterialPropertyName))
            {
                _renderer.material.SetFloat(Constants.HighlighterMaterialPropertyName, Random.Range(_minOutlineThickness, _maxOutlineThickness));
                _renderer.material.color = _outlineColor;
            }
        }
    }
}