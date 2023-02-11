using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beliver : MonoBehaviour
{
    // beliver�� ������ �̸�
    [SerializeField]
    private string beliverName;

    // beliver�� �̵��ӵ�
    [SerializeField]
    private int speed;

    // beliver�� �����ؾ��ϴ� �ൿ
    [SerializeField]
    private string beliverAnimation;
    
    // beliver�� ���� ����
    [SerializeField]
    private int group;
    
    // beliver�� ���� �漺��
    [SerializeField]
    private int loyalty;
    
    // beliver�� ���� �۾���
    [SerializeField]
    private int workGroup;
    
    // beliver�� �۾��ӵ�
    [SerializeField]
    private float workSpeed;

    // beliver�� �̵��ӵ� ���� �޼���
    public void SetSpeed(int speed)
    {
        // �Էµ� �ӵ��� ���������� Ȯ�� �ʿ�
        this.speed = speed;
    }
    public int GetSpeed()
    {
        return this.speed;
    }

    // beliver�� �漺�� ���� �޼���
    public void SetLoyalty(int loyalty)
    {
        this.loyalty = loyalty;
    }
    public int GetLoyalty()
    {
        return this.loyalty;
    }
    public int AccLoyalty(int loyalty)
    {
        // �漺�� ���� �޼���
        // ��ȯ������ ����� �Ϸ�� �漺���� ����
        // TODO: ����� ���Ἲ�� Ȯ���� �ʿ䰡 ����
        this.loyalty += loyalty;
        return this.loyalty;
    }

    // beliver�� �۾��ӵ� ���� �޼���
    public void SetWorkSpeed(int workspeed)
        // TODO: �Ű������� �Ӽ��� Ÿ���� ��ġ��ų �ʿ䰡 ����
    {
        this.workSpeed = workspeed;
    }
    public float GetWorkSpeed()
    {
        return this.workSpeed;
    }
    public float AccWorkSpeed(float workspeed)
    {
        // �۾��ӵ� ���� �޼���
        // ��ȯ������ ����� �Ϸ�� �۾��ӵ��� ����
        // TODO: ����� ���Ἲ�� Ȯ���� �ʿ䰡 ����
        this.workSpeed += workspeed;
        return this.workSpeed;
    }

    // �ִϸ��̼� ����
    public void SetAnimation(string animation)
    {
        this.beliverAnimation = animation;
    }

    // beliver�� �Ҽ� �۾��� ���� �޼���
    public void SetWorkGroup(int workgroup)
    {
        this.workGroup = workgroup;
    }
    public int GetWorkGroup()
    {
        return this.workGroup;
    }

    // beliver�� ��ġ�̵�
    public void RandomWalk()
    {
        // TODO: ���ӿ�����Ʈ�� �������� �̵����Ѿ� ��.
        return;
    }
    public void Walk(int direction)
    {
        // TODO: ���ӿ�����Ʈ�� Ư�������� �̵����Ѿ� ��.
        return;
    }

    // animation DB����
    // string GetAnimation(int serial)
    // TODO: �����ϸ� Beliver���� Ŭ�������� ó���ϰ� ����
    public string QueryAnimation(int serial)
    {
        // TODO: DB���� ����
        return "";
    }
}
