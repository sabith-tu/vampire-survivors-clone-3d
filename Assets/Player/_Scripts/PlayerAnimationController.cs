using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private int _idleAnimation = Animator.StringToHash("Idle");
    private int _runAnimation = Animator.StringToHash("Run");
    private int _selfieAnimation = Animator.StringToHash("Selfie");
    private int _caghtAnimation = Animator.StringToHash("Caught");

    public void PlayIdleAnimation()
    {
        animator.CrossFade(_idleAnimation, 0.1f);
    }

    public void PlayRunAnimation()
    {
        animator.CrossFade(_runAnimation, 0.1f);
    }

    public void PlaySelfieAnimation()
    {
        animator.CrossFade(_selfieAnimation, 1f);
    }

    public void PlayCaghtAnimation()
    {
        animator.CrossFade(_caghtAnimation, 0.1f);
    }
}