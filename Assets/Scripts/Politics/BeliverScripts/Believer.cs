using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Believer : MonoBehaviour
{
    // believer의 고유한 이름
    [SerializeField]
    private string believerName;

    // believer의 나이
    [SerializeField]
    private int age;

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

    // DB에 저장할 때 필요한 고유 PK
    private int believerId;
    
    // 신도의 상태
    public enum Status
    {
        IDLE,
        ASSIGN,
        SLEEP,
        DEAD
    }
    private Status condition;

    // believer의 이름확인
    public string GetName()
    {
        return believerName;
    }

    // believer의 나이 증가
    public void IncreaseOneAge()
    {
        // 수명 체크를 여기서 하면 편할 듯 함.
        this.age += 1;
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
    public void SetWorkSpeed(float workspeed)
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

    // 신도가 잠을 청하도록 함
    // 본인의 판자촌으로 복귀
    public void FallAsleep()
    {
        // TODO: 본인 집이 어딘지 저장해둘 필요가 있음
        // TODO: 들어간 집 위에 'ZZZ'를 표시하면 좋을 듯
    }

    public void WakeUp()
    {
        // 자고있을 경우만 한정하면 죽거나 불침번일 경우도 처리 가능
        if (condition == Status.SLEEP)
        {
            // workgroup의 0이 할당되지 않은 상태일 경우
            condition = (this.workGroup == 0) ? Status.IDLE : Status.ASSIGN; 
        }
    }

    // animation DB접근
    // string GetAnimation(int serial)
    // TODO: 가능하면 Beliver관리 클래스에서 처리하고 싶음
    public string GetAnimation(int serial)
    {
        // TODO: DB접근 구현
        return "";
    }

    // 씬에 생성될 때 신도목록에 정보 추가
    public GameObject believerList;
    public GameObject believerListElementPref;
    public void Awake()
    {
        Transform believerListObj = believerList.transform;
        Debug.Log(believerListObj);
        GameObject element = Instantiate(believerListElementPref, believerListObj);

        // 목록 내용 채워넣기
        BelieverProperty elementComp = (BelieverProperty)element.GetComponent("BelieverProperty");
        elementComp.initBeliever(gameObject);
    }
}
