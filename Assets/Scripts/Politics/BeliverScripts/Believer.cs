using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Believer : MonoBehaviour
{
    // believer�� ������ �̸�
    [SerializeField]
    private string believerName;

    // believer�� ����
    [SerializeField]
    private int age;

    // believer�� �̵��ӵ�
    [SerializeField]
    private int speed;

    // believer�� �����ؾ��ϴ� �ൿ
    [SerializeField]
    private string believerAnimation;
    
    // believer�� ���� ����
    [SerializeField]
    private int group;
    
    // believer�� ���� �漺��
    [SerializeField]
    private int loyalty;
    
    // believer�� ���� �۾���
    [SerializeField]
    private int workGroup;
    
    // believer�� �۾��ӵ�
    [SerializeField]
    private float workSpeed;

    // DB�� ������ �� �ʿ��� ���� PK
    private int believerId;
    
    // �ŵ��� ����
    public enum Status
    {
        IDLE,
        ASSIGN,
        SLEEP,
        DEAD
    }
    private Status condition;

    // believer�� �̸�Ȯ��
    public string GetName()
    {
        return believerName;
    }

    // believer�� ���� ����
    public void IncreaseOneAge()
    {
        // ���� üũ�� ���⼭ �ϸ� ���� �� ��.
        this.age += 1;
    }

    // believer�� �̵��ӵ� ���� �޼���
    public void SetSpeed(int speed)
    {
        // �Էµ� �ӵ��� ���������� Ȯ�� �ʿ�
        this.speed = speed;
    }
    public int GetSpeed()
    {
        return this.speed;
    }

    // believer�� �漺�� ���� �޼���
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

    // believer�� �۾��ӵ� ���� �޼���
    public void SetWorkSpeed(float workspeed)
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
        this.believerAnimation = animation;
    }

    // believer�� �Ҽ� �۾��� ���� �޼���
    public void SetWorkGroup(int workgroup)
    {
        this.workGroup = workgroup;
    }
    public int GetWorkGroup()
    {
        return this.workGroup;
    }

    // believer�� ��ġ�̵�
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

    // �ŵ��� ���� û�ϵ��� ��
    // ������ ���������� ����
    public void FallAsleep()
    {
        // TODO: ���� ���� ����� �����ص� �ʿ䰡 ����
        // TODO: �� �� ���� 'ZZZ'�� ǥ���ϸ� ���� ��
    }

    public void WakeUp()
    {
        // �ڰ����� ��츸 �����ϸ� �װų� ��ħ���� ��쵵 ó�� ����
        if (condition == Status.SLEEP)
        {
            // workgroup�� 0�� �Ҵ���� ���� ������ ���
            condition = (this.workGroup == 0) ? Status.IDLE : Status.ASSIGN; 
        }
    }

    // animation DB����
    // string GetAnimation(int serial)
    // TODO: �����ϸ� Beliver���� Ŭ�������� ó���ϰ� ����
    public string GetAnimation(int serial)
    {
        // TODO: DB���� ����
        return "";
    }

    // ���� ������ �� �ŵ���Ͽ� ���� �߰�
    public GameObject believerList;
    public GameObject believerListElementPref;
    public void Awake()
    {
        Transform believerListObj = believerList.transform;
        Debug.Log(believerListObj);
        GameObject element = Instantiate(believerListElementPref, believerListObj);

        // ��� ���� ä���ֱ�
        BelieverProperty elementComp = (BelieverProperty)element.GetComponent("BelieverProperty");
        elementComp.initBeliever(gameObject);
    }
}
