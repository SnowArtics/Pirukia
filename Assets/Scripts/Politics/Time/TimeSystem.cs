using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: ANGLE_MODIFIER_* ��� ����, �ŵ� ���� ��� �߰�
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
        // TODO: ������ ���� ������ �ð��� �ε�
    }
    // TODO: ���� �ð��� �����ϴ� �ı��� �ۼ�

    // Update is called once per frame
    void Update()
    {
        Update_time();
    }

    void Update_time()
    {
        // this.targetTime�� 0�� �ƴϸ� �ش�ð����� �ٲٰ� 0���� ����
        Set_time();

        // �ð��� �帣�� ��
        Update_second();
        // 1�ð� = 60�� * 60�� -> 3600
        this.time = (float)(this.second / 3600);

        // �¾��� ������
        Update_lightAngle();
    }

    private void Update_second()
    {
        // �� ������ ���� �ð��� �帣���� ��
        // this.second�� ������ �ð��� ����
        // ������ �Ϸ簡 �� 960�ʰ� �ǵ��� ����� ���
        double sec = Time.deltaTime / GAME_TIME;
        this.second += sec;

        // �Ϸ簡 86400�� 24�ô� �� ������ 0��
        int deltaDay= (int)(this.second / 86400);
        this.day += deltaDay;
        this.second %= 86400;
        // �ŵ��� ���� ����
        // GetOldAllBeliever(deltaDay);
    }

    void Set_second(double sec)
    {
        // lightAngle�� update���� �ð���� �� ����ǹǷ� ���⼭ �������� �� ��
        // second�� sec�� �Ϸ簡 �Ѿ��(86400) �������� ��� day�� ó������ ����.
        sec %= 86400;
        this.second = sec;
    }

    void Set_time(float tTime=0.0f)
    {
        // tTime�� 0�� �ƴ� ���� ������ �ش� �ð����� ����
        // tTime�� 0�̸� this.targetTime�� Ȯ��
        // Ȯ���� �Ķ���Ͱ� ��� 0�̸� ����
        float t = tTime + this.targetTime;
        if (-float.Epsilon < t && t < float.Epsilon)
            return;

        // �Ѵ� 0�� �ƴҰ�� tTime�� �켱 �����ϰ� ���� this.targetTime�� ���
        if(tTime == 0.0f)
        {
            tTime = this.targetTime;
            this.targetTime = 0.0f;
        }
        
        // 0���� �������� ������ 0���� ����
        // (���ŷ� ���ư����� ��� ���¸� �ð����� �����ؾ� ��)
        tTime = tTime > 0 ? tTime : 0;

        // 24�ð��� �Ѿ�� �߶󳻰� day�� �߰��� ��
        float tmpTime = tTime + this.time;
        int deltaDay = (int)(tmpTime) / 24;
        this.day += deltaDay;

        // ��¥ ��� �� ���� �ð� ó��
        tTime = tmpTime % 24;

        // 1�ð� = 60�� * 60�� -> 3600
        Set_second(tTime * 3600);

        // TODO: ��ŵ�� �ð��� �ý��ۿ� �˷� ��Ȯ �� �ŵ��� ���� ���� ó���� �ϵ��� ��
        // TODO: deltaDay*89400 + deltaSecond
    }
    private void Update_lightAngle()
    {
        // this.time�� �����Ͽ� �¾��� ���� ����
        // �Ʒ� ���� �� �ʿ����� ������ ������ ��õ� ������
        this.angleModifier = (this.time > 4) && (this.time < 22) ? ANGLE_MODIFIER_DAY : ANGLE_MODIFIER_NIGHT;
        // 4�ÿ� �ذ� �� �� �ֵ���(0��) 4�� ���� '����'�� ��� t+24 �� ���
        // 0 < t < 24 (time=3.9, t'=-0.1, t=23.9)
        double t = this.time - 4.0;
        t = t < 0 ? t + 24.0 : t;

        // ����~�ϸ�(180��) time=22 -> t=18
        double dayAngle, nightAngle;
        dayAngle = t < 18 ? t * 10 : 180;
        // �ϸ�~������
        // 18 < t < 24 time=3.9 -> t=23.9
        nightAngle = t > 18 ? (t - 18) * 30 : 0;

        // ���� ���� �� (time=3.9, t'=-0.1, t=23.9, 180+177=357)
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
