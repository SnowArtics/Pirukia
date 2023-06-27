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

        // �������� ���� ��
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

        // ������ �ȴ� ���
        if (vertical < -float.Epsilon)
            animator.SetInteger("toward", 2);
        // �ڷΰ��� ���
        if (vertical > float.Epsilon)
            animator.SetInteger("toward", 8);
        // �·ΰ��� ���
        if (horizontal < -float.Epsilon)
            animator.SetInteger("toward", 4);
        // ��ΰ��� ���
        if (horizontal > float.Epsilon)
            animator.SetInteger("toward", 6);

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        // �ٴ��� 45�� ȸ���� �־ ĳ������ �̵� ���⵵ Ʋ����� ī�޶� ���� �¿� �̵��� ����
        moveDirection = Quaternion.AngleAxis(45, Vector3.up) * moveDirection;
        // �̵��ӵ� �߰�
        moveDirection *= moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
