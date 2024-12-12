using UnityEngine;

//플레이어 애니메이션 동작 관리
public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    
    //애니메이션 시작
    public void StartAnimation(int animationHash)
    {
        animator.SetBool(animationHash, true);
    }
    
    //애니메이션 정지
    public void StopAnimation(int animationHash)
    {
        animator.SetBool(animationHash, false);
    }
}
