using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beliver : MonoBehaviour
{
    // beliver의 고유한 이름
    [SerializeField]
    private string beliverName;

    // beliver의 이동속도
    [SerializeField]
    private int speed;

    // beliver가 수행해야하는 행동
    [SerializeField]
    private string beliverAnimation;
    
    // beliver가 속한 세력
    [SerializeField]
    private int group;
    
    // beliver가 지닌 충성도
    [SerializeField]
    private int loyalty;
    
    // beliver가 속한 작업장
    [SerializeField]
    private int workGroup;
    
    // beliver의 작업속도
    [SerializeField]
    private float workSpeed;

    // beliver의 이동속도 조정 메서드
    public void SetSpeed(int speed)
    {
        // 입력된 속도가 정상적인지 확인 필요
        this.speed = speed;
    }
    public int GetSpeed()
    {
        return this.speed;
    }

    // beliver의 충성도 조정 메서드
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
        // 충성도 가감 메서드
        // 반환값으로 계산이 완료된 충성도를 가짐
        // TODO: 계산의 무결성을 확인할 필요가 있음
        this.loyalty += loyalty;
        return this.loyalty;
    }

    // beliver의 작업속도 조정 메서드
    public void SetWorkSpeed(int workspeed)
        // TODO: 매개변수와 속성의 타임을 일치시킬 필요가 있음
    {
        this.workSpeed = workspeed;
    }
    public float GetWorkSpeed()
    {
        return this.workSpeed;
    }
    public float AccWorkSpeed(float workspeed)
    {
        // 작업속도 가감 메서드
        // 반환값으로 계산이 완료된 작업속도를 가짐
        // TODO: 계산의 무결성을 확인할 필요가 있음
        this.workSpeed += workspeed;
        return this.workSpeed;
    }

    // 애니메이션 설정
    public void SetAnimation(string animation)
    {
        this.beliverAnimation = animation;
    }

    // beliver의 소속 작업장 조정 메서드
    public void SetWorkGroup(int workgroup)
    {
        this.workGroup = workgroup;
    }
    public int GetWorkGroup()
    {
        return this.workGroup;
    }

    // beliver의 위치이동
    public void RandomWalk()
    {
        // TODO: 게임오브젝트를 무작위로 이동시켜야 함.
        return;
    }
    public void Walk(int direction)
    {
        // TODO: 게임오브젝트를 특정방위로 이동시켜야 함.
        return;
    }

    // animation DB접근
    // string GetAnimation(int serial)
    // TODO: 가능하면 Beliver관리 클래스에서 처리하고 싶음
    public string QueryAnimation(int serial)
    {
        // TODO: DB접근 구현
        return "";
    }
}
