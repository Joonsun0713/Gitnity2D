using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMoveAnimation(bool isMoving)
    {
        
            animator.SetBool("Move", isMoving);
        
    }

    
    public void PlayAttackAnimation()
    {
        
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
}
