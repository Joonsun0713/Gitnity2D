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

    public void PlayerJumpFallAnimation()
    {
        animator.SetTrigger("Fall");
    }

    public void PlayerRollAnimaiton()
    {
        animator.SetTrigger("Roll");
    }
}
