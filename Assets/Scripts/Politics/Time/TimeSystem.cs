using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: ANGLE_MODIFIER_* 상수 조절, 신도 관련 기능 추가
public class TimeSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;
    [SerializeField]
    private float lightAngle;
    [SerializeField]
    private float time;
    [SerializeField]
    private double second;
    [SerializeField]
    private float angleModifier;
    [SerializeField]
    private int day;
    [SerializeField]
    private float targetTime;

    private const double GAME_TIME = 0.011111111111111;
    private const float ANGLE_MODIFIER_DAY = 0.25f;
    private const float ANGLE_MODIFIER_NIGHT = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: 마지막 실행 시점의 시간을 로드
    }
    // TODO: 현재 시간을 저장하는 파괴자 작성

    // Update is called once per frame
    void Update()
    {
        Update_time();
    }

    void Update_time()
    {
        // this.targetTime이 0이 아니면 해당시간으로 바꾸고 0으로 설정
        Set_time();

        // 시간을 흐르게 함
        Update_second();
        // 1시간 = 60분 * 60초 -> 3600
        this.time = (float)(this.second / 3600);

        // 태양이 움직임
        Update_lightAngle();
    }

    private void Update_second()
    {
        // 매 프래임 마다 시간이 흐르도록 함
        // this.second에 프레임 시간을 누산
        // 게임의 하루가 약 960초가 되도록 상수를 계산
        double sec = Time.deltaTime / GAME_TIME;
        this.second += sec;

        // 하루가 86400초 24시는 곧 다음날 0시
        int deltaDay= (int)(this.second / 86400);
        this.day += deltaDay;
        this.second %= 86400;
        // 신도의 나이 증가
        // GetOldAllBeliever(deltaDay);
    }

    void Set_second(double sec)
    {
        // lightAngle은 update에서 시간계산 후 실행되므로 여기서 실행하지 말 것
        // second의 sec가 하루가 넘어가면(86400) 나머지만 사용 day는 처리하지 않음.
        sec %= 86400;
        this.second = sec;
    }

    void Set_time(float tTime=0.0f)
    {
        // tTime이 0이 아닌 값을 받으면 해당 시간으로 변경
        // tTime이 0이면 this.targetTime을 확인
        // 확인한 파라미터가 모두 0이면 종료
        float t = tTime + this.targetTime;
        if (-float.Epsilon < t && t < float.Epsilon)
            return;

        // 둘다 0이 아닐경우 tTime을 우선 적용하고 이후 this.targetTime을 사용
        if(tTime == 0.0f)
        {
            tTime = this.targetTime;
            this.targetTime = 0.0f;
        }
        
        // 0보다 작은값이 들어오면 0으로 받음
        // (과거로 돌아가려면 모든 상태를 시간별로 저장해야 함)
        tTime = tTime > 0 ? tTime : 0;

        // 24시간이 넘어가면 잘라내고 day를 추가할 것
        float tmpTime = tTime + this.time;
        int deltaDay = (int)(tmpTime) / 24;
        this.day += deltaDay;

        // 날짜 계산 후 남은 시간 처리
        tTime = tmpTime % 24;

        // 1시간 = 60분 * 60초 -> 3600
        Set_second(tTime * 3600);

        // TODO: 스킵된 시간을 시스템에 알려 수확 및 신도의 수명에 관한 처리를 하도록 함
        // TODO: deltaDay*89400 + deltaSecond
    }
    private void Update_lightAngle()
    {
        // this.time을 참조하여 태양의 고도를 조정
        // 아래 줄은 꼭 필요하진 않지만 명세서에 명시된 동작임
        this.angleModifier = (this.time > 4) && (this.time < 22) ? ANGLE_MODIFIER_DAY : ANGLE_MODIFIER_NIGHT;
        // 4시에 해가 뜰 수 있도록(0도) 4를 빼고 '음수'일 경우 t+24 로 계산
        // 0 < t < 24 (time=3.9, t'=-0.1, t=23.9)
        double t = this.time - 4.0;
        t = t < 0 ? t + 24.0 : t;

        // 일출~일몰(180도) time=22 -> t=18
        double dayAngle, nightAngle;
        dayAngle = t < 18 ? t * 10 : 180;
        // 일몰~새벽녘
        // 18 < t < 24 time=3.9 -> t=23.9
        nightAngle = t > 18 ? (t - 18) * 30 : 0;

        // 일출 직전 고도 (time=3.9, t'=-0.1, t=23.9, 180+177=357)
        this.lightAngle = (float)(dayAngle + nightAngle);
        sun.transform.localEulerAngles = new Vector3(this.lightAngle, 0, 0);
    }

    GameObject Get_sun()
    {
        return this.sun;
    }

    float Get_time()
    {
        return this.time;
    }

    double Get_second()
    {
        return this.second;
    }

    float Get_angleModifier()
    {
        return this.angleModifier;
    }

    int Get_day()
    {
        return this.day;
    }
}
