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

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerAttackEnvent()
    {
        pattack.Attack();
        Debug.Log("공격 애니메이션 이벤트 성공");
    }
    public void SetMoveAnimation(bool isMoving)
    {
        
       animator.SetBool("Move", isMoving);
        
    }

    
    public void PlayComboAttackAnimation(int ComboStep)
    {
        
        animator.SetInteger("ComboStep", ComboStep);
        animator.SetTrigger("Attack");
        
    }

    public void PlayerJumpUpAnimation()
    {
        animator.SetTrigger("Jump");
    }

    public void PlayerJumpFallAnimation(bool IsFall)
    {
       
        animator.SetBool("Fall", IsFall);
        
    }

    public void PlayerRollAnimation()
    {
        animator.SetTrigger("Roll");
    }

    public void PlayerShieldAnimation(bool isShield)
    {
        animator.SetBool("Shield",  isShield);
    }

    public void PlayerDeathAnimation()
    {
        animator.SetTrigger("Death");
    }

    public void PlayerIsShieldAnimation()
    {
        animator.SetTrigger("IsShield");
    }

   

}
