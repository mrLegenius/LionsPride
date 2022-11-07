using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LionessAnimator : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsEatingOrDrinking = Animator.StringToHash("IsEatingOrDrinking");
    private Animator _animator;

    private Animator Animator => _animator ??= GetComponent<Animator>();
    
    public void SetSpeed(float value)
    {
        Animator.SetFloat(Speed, value);
    }

    public void SetEatOrDrink(bool value)
    {
        Animator.SetBool(IsEatingOrDrinking, value);
    }
}
