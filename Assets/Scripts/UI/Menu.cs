using System.Collections;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public const string EnabledState = "Enabled";
    public const string DisabledState = "Disabled";
    public const string InitialState = "None";

    public MenuType Type { get => _type; private set => _type = value; }
    public string TargetState { get; protected set; }
    public bool UseAnimation { get => _useAnimation; private set => _useAnimation = value; }
    public bool IsEnabled { get; private set; }


    [SerializeField]
    private MenuType _type;
    [SerializeField]
    private bool _useAnimation;

    private Animator _animator;
    private Coroutine _animationCoroutine;

    public void Animate(bool enable)
    {
        if (_useAnimation)
        {
            _animator.SetBool("Enabled", enable);

            if(_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = StartCoroutine(AwaitAnimation(enable));
        }
        else
        {
            IsEnabled = enable;

            if (!enable)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator AwaitAnimation(bool enable)
    {
        TargetState = enable ? EnabledState : DisabledState;


        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(TargetState))
        {
            yield return null;
        }

        //If it hits 1 it means that animation finished
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        TargetState = InitialState;
        _animationCoroutine = null;

        IsEnabled = enable;

        if (!enable)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnEnable()
    {
        if (_useAnimation)
        {
            _animator = GetComponent<Animator>();
            if (!_animator)
            {
                Debug.LogWarning("Komponent animator nie jest umieszczony na obiekcie, który chcesz animować");
            }
        }
    }
}