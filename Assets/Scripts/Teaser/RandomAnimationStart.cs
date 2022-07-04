using UnityEngine;

/// <summary>
/// Used in trailer to randomize jumps of protons/neutrons.
/// </summary>
[RequireComponent(typeof(Animator))]
public class RandomAnimationStart : MonoBehaviour
{
    private Animator _animator;
    private AnimatorStateInfo _animatorStateInfo;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        _animator.speed = Random.Range(.7f, 1.3f);
        _animator.Play(_animatorStateInfo.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
