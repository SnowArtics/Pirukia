using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: ANGLE_MODIFIER_* 상수 조절, 신도 관련 기능 추가
public class TimeManagement : MonoBehaviour
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
        Update_lightAngle();
    }

    void Update_time()
    {
        Update_second();
        // 1시간 = 60분 * 60초 -> 3600
        this.time = (float)(this.second / 3600);
    }

    private void Update_second()
    {
        Set_second(Time.deltaTime / GAME_TIME);
    }

    void Set_second(double sec)
    {
        // 0보다 작은값이 들어오면 0으로 받음
        this.second += sec > 0 ? sec : 0;
        // 하루가 86400초 24시는 곧 다음날 0시
        int deltaDay= (int)(this.second / 86400);
        this.day += deltaDay;
        this.second %= 86400;
        // 신도의 나이 증가
        // GetOldAllBeliever(deltaDay);
    }

    void Set_time(float tTime)
    {
        // 0보다 작은값이 들어오면 0으로 받음
        tTime = tTime > 0 ? tTime : 0;
        // 24시간이 넘어가면 잘라냄
        tTime %= 24;
        // 1시간 = 60분 * 60초 -> 3600
        Set_second(tTime * 3600);
    }
    private void Update_lightAngle()
    {
        // 낮과 밤의 길이를 조정
        this.angleModifier = (this.time > 4) && (this.time < 22) ? ANGLE_MODIFIER_DAY : ANGLE_MODIFIER_NIGHT;
        // 시간에 따라 태양의 고도를 조정 (시간플로우차트: 현실시간으로 -> 현실시간에선)
        // 0은 한밤중 -90도
        // 260 / 24 = 15
        // this.lightAngle = (float)(this.time * 15 * this.angleModifier) - 90;
        this.lightAngle = (float)(this.time * 15) - 90;
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
