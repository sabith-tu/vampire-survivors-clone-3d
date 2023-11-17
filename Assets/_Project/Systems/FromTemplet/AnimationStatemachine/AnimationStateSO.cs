using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationState", menuName = "SO/State/AnimationState")]
public class AnimationStateSO : ScriptableObject
{
    public string AnimationStateName;
    public bool isCrossFade = true;
    [ShowIf("isCrossFade")] public float crossFadeTransition = 0.2f;
}