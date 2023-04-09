using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Believer : MonoBehaviour
{
    // believer의 고유한 이름
    [SerializeField]
    private string believerName;

    // believer의 이동속도
    [SerializeField]
    private int speed;

    // believer가 수행해야하는 행동
    [SerializeField]
    private string believerAnimation;
    
    // believer가 속한 세력
    [SerializeField]
    private int group;
    
    // believer가 지닌 충성도
    [SerializeField]
    private int loyalty;
    
    // believer가 속한 작업장
    [SerializeField]
    private int workGroup;
    
    // believer의 작업속도
    [SerializeField]
    private float workSpeed;

    // believer의 이름확인
    public string GetName()
    {
        // 테스트용 임시 신도이름
        this.believerName = "John";
        return believerName;
    }

    // believer의 이동속도 조정 메서드
    public void SetSpeed(int speed)
    {
        // 입력된 속도가 정상적인지 확인 필요
        this.speed = speed;
    }
    public int GetSpeed()
    {
        return this.speed;
    }

    // believer의 충성도 조정 메서드
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

    // believer의 작업속도 조정 메서드
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
        this.believerAnimation = animation;
    }

    // believer의 소속 작업장 조정 메서드
    public void SetWorkGroup(int workgroup)
    {
        this.workGroup = workgroup;
    }
    public int GetWorkGroup()
    {
        return this.workGroup;
    }

    // believer의 위치이동
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

    // 생성될 때 신도목록에 정보 추가
    public GameObject believerListElementPref;
    public void Start()
    {
        Transform believerListObj = GameObject.FindWithTag("BelieverList").transform;
        GameObject element = Instantiate(believerListElementPref, believerListObj);

        // 목록 내용 채워넣기
        BelieverProperty elementComp = (BelieverProperty)element.GetComponent("BelieverProperty");
        elementComp.initBeliever(gameObject);
    }
}
