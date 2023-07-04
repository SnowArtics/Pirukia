using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public float moveSpeed = 10f;

    private Animator animator;
    private bool isWalking;
    private int directionPrev;

    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void resetAnim()
    {
        animator.ResetTrigger("idle");
        animator.ResetTrigger("front");
        animator.ResetTrigger("left");
        animator.ResetTrigger("right");
        animator.ResetTrigger("back");
        animator.SetTrigger("idle");
    }
    private void changeDirection(int dir)
    {
        if (dir != directionPrev)
        {
            resetAnim();
            directionPrev = dir;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float horizontalAbs = MathF.Abs(horizontal);
        float verticalAbs = MathF.Abs(vertical);
        // 움직이지 않을 때
        if (horizontalAbs + verticalAbs < float.Epsilon*100)
        {
            if (isWalking)
            {
                isWalking = false;
                resetAnim();
            }
            return;
        }
        else
        {
            isWalking = true;
        }


        if (verticalAbs > horizontalAbs)
        {
            // 앞으로 걷는 모션
            if (vertical < -float.Epsilon)
            {
                changeDirection(2);
                animator.SetTrigger("front");
            }
            // 뒤로가는 모션
            if (vertical > float.Epsilon)
            {
                changeDirection(8);
                animator.SetTrigger("back");
            }
        }
        else
        {
            // 좌로가는 모션
            if (horizontal < -float.Epsilon)
            {
                changeDirection(4);
                animator.SetTrigger("left");
            }
            // 우로가는 모션
            if (horizontal > float.Epsilon)
            {
                changeDirection(6);
                animator.SetTrigger("right");
            }
        }

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        // 바닥이 45도 회전해 있어서 캐릭터의 이동 방향도 틀어줘야 카메라 기준 좌우 이동이 가능
        moveDirection = Quaternion.AngleAxis(45, Vector3.up) * moveDirection;
        // 이동속도 추가
        moveDirection *= moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
