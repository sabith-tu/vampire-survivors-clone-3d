using Sirenix.OdinInspector;
using UnityEngine;

[AddComponentMenu("_SABI/SimpleStateMachine/SabiAnimationStateMachineSOs")]
public class SabiAnimationontroller : MonoBehaviour
{
    [Space(5)] [DisplayAsString] [SerializeField]
    private AnimationStateSO currentAnimationState = null;

    [Space(5)] [SerializeField] private Animator animator;

    [Space(5)] [SerializeField] private bool getAnimatorOnRunTime = false;

    [ShowIf("getAnimatorOnRunTime")] [SerializeField]
    private bool useGetComponent = false;

    [ShowIf("getAnimatorOnRunTime")] [SerializeField]
    private bool useGetComponentInChild = false;

    [ShowIf("getAnimatorOnRunTime")] [SerializeField]
    private bool useGetComponentInParent = false;


    [Space(5)] [SerializeField] protected bool allowSameStateToSetAgain = false;

    [Space(5)] [SerializeField] protected bool overRideStartingState = false;

    [ShowIf("overRideStartingState")] [Space(5)] [SerializeField]
    protected AnimationStateSO startingState;

    private void Start()
    {
        if (animator == null && getAnimatorOnRunTime)
        {
            if (useGetComponent) animator = GetComponent<Animator>();
            else if (useGetComponentInChild) animator = GetComponentInChildren<Animator>();
            else if (useGetComponentInParent) animator = GetComponentInParent<Animator>();
        }

        if (overRideStartingState) SetAndRunState(startingState);
    }

    private void PlayAnimationStateCrossFade(string animationStateName) => animator.CrossFade(animationStateName, 0.2f);

    private void PlayAnimationStateCrossFade(string animationStateName, float transition) =>
        animator.CrossFade(animationStateName, transition);

    private void PlayAnimationStateJustPlay(string animationStateName) => animator.Play(animationStateName);

    public void SetAnimationLayer(AnimationLayerSO animationLayerLayer) =>
        animator.SetLayerWeight(animationLayerLayer.Layer, animationLayerLayer.shouldEnable ? 1 : 0);

    public void SetAndRunState(AnimationStateSO newState)
    {
        if (currentAnimationState == newState && !allowSameStateToSetAgain) return;
        SetState(newState);
        RunCurrentState();
    }

    private void SetState(AnimationStateSO newState)
    {
        if (animator.HasState(0, Animator.StringToHash(newState.AnimationStateName)))
        {
            currentAnimationState = newState;
        }
        else if (animator.HasState(1, Animator.StringToHash(newState.AnimationStateName)))
        {
            currentAnimationState = newState;
        }
        else if (animator.HasState(2, Animator.StringToHash(newState.AnimationStateName)))
        {
            currentAnimationState = newState;
        }
        else
        {
            Debug.LogError(
                $"{newState.AnimationStateName} is not a valid state at animator on {animator.gameObject.name} game object -SABI");
        }
    }

    public void RunCurrentState()
    {
        if (currentAnimationState.isCrossFade)
        {
            PlayAnimationStateCrossFade(currentAnimationState.AnimationStateName,
                currentAnimationState.crossFadeTransition);
        }
        else PlayAnimationStateJustPlay(currentAnimationState.AnimationStateName);
    }
}