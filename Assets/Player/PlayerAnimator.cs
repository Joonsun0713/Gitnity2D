using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerAttack pattack;

    void Start()
    {
        animator = GetComponent<Animator>();
        pattack = GetComponentInChildren<PlayerAttack>();
    }

    void PlayerAttackEnvent() // 콤보 공격 애니메이션 이벤트
    {
        pattack.Attack();
        Debug.Log("공격 애니메이션 이벤트 성공");
    }   

    public void SetMoveAnimation(bool isMoving) // 이동 애니메이션
    {
        
       animator.SetBool("Move", isMoving);
        
    } 

    
    public void PlayComboAttackAnimation(int ComboStep) // 콤보별 공격 애니메이션
    {
        
        animator.SetInteger("ComboStep", ComboStep);
        animator.SetTrigger("Attack");
        
    }   

    public void PlayerJumpUpAnimation() // 점프 애니메이션
    {
        animator.SetTrigger("Jump");
    }   

    public void PlayerJumpFallAnimation(bool IsFall) //낙하 애니메이션
    {
       
        animator.SetBool("Fall", IsFall);
        
    }   

    public void PlayerRollAnimation() //쉴드 애니메이션
    {
        animator.SetTrigger("Roll");
    }   

    public void PlayerShieldAnimation(bool isShield) //쉴드 애니메이션
    {
        animator.SetBool("Shield",  isShield);
    }   

    public void PlayerDeathAnimation() // 사망 애니메이션
    {
        animator.SetTrigger("Death");
    }   

    public void PlayerIsShieldAnimation() //튕겨내기 애니메이션
    {
        animator.SetTrigger("IsShield");
    }       

}
