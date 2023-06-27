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

    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Debug.Log($"Horizontal: {horizontal}");
        float vertical = Input.GetAxis("Vertical");
        Debug.Log($"Vertical: {vertical}");

        // 움직이지 않을 때
        if (Math.Abs(horizontal) + Math.Abs(vertical) < float.Epsilon)
        {
            if (isWalking)
            {
                isWalking = false;
                animator.SetBool("isWalk", false);
            }
            return;
        }
        else
        {
            animator.SetBool("isWalk", true);
            isWalking = true;
        }

        // 앞으로 걷는 모션
        if (vertical < -float.Epsilon)
            animator.SetInteger("toward", 2);
        // 뒤로가는 모션
        if (vertical > float.Epsilon)
            animator.SetInteger("toward", 8);
        // 좌로가는 모션
        if (horizontal < -float.Epsilon)
            animator.SetInteger("toward", 4);
        // 우로가는 모션
        if (horizontal > float.Epsilon)
            animator.SetInteger("toward", 6);

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        // 바닥이 45도 회전해 있어서 캐릭터의 이동 방향도 틀어줘야 카메라 기준 좌우 이동이 가능
        moveDirection = Quaternion.AngleAxis(45, Vector3.up) * moveDirection;
        // 이동속도 추가
        moveDirection *= moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
