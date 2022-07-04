using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
public class RadialProgress : MonoBehaviour
{
    public float Progress {
        get => _progress;
        set {

            if (value >= 1f)
            {
                StopAnimation();
            }

            _progress = value;
            _image.fillAmount = _progress;
        }
    }

    [SerializeField]
    private List<Sprite> _sprites;
    [SerializeField]
    private float _spriteSwapDelay;
    [SerializeField]
    private string _fadeAnimationName;
    [SerializeField]
    private string _defaultAnimationName;

    private Image _image;
    private Animator _animator;
    private WaitForSeconds _wait;
    private float _progress;
    private Coroutine _spriteAnimationCoroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(_spriteSwapDelay);
    }

    private void OnEnable()
    {
        CardboardInteractable.OnHoverEnter += StartAnimation;
        CardboardInteractable.OnHoverStay += UpdateProgress;
        CardboardInteractable.OnHoverExit += StopAnimation;
    }

    private void OnDisable()
    {
        CardboardInteractable.OnHoverEnter -= StartAnimation;
        CardboardInteractable.OnHoverStay -= UpdateProgress;
        CardboardInteractable.OnHoverExit -= StopAnimation;
    }

    private void Start()
    {
        _image = GetComponent<Image>();
        _animator = GetComponent<Animator>();
    }

    //Reciever of animation event
    public void ResetProgress()
    {
        Progress = 0;
    }

    private void StartAnimation()
    {
        _animator.Play(_defaultAnimationName);
        _spriteAnimationCoroutine = StartCoroutine(AnimateSprite());
    }

    private void StopAnimation()
    {
        _animator.Play(_fadeAnimationName);
        StopCoroutine(_spriteAnimationCoroutine);
    }

    private void UpdateProgress(float progress){
        Progress = progress;
    }

    private IEnumerator AnimateSprite()
    {
        while (true)
        {
            yield return _wait;
            _image.sprite = _sprites[Random.Range(0, _sprites.Count)];
        }
    }

}
