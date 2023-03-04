using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: ANGLE_MODIFIER_* ��� ����, �ŵ� ���� ��� �߰�
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
        // TODO: ������ ���� ������ �ð��� �ε�
    }
    // TODO: ���� �ð��� �����ϴ� �ı��� �ۼ�

    // Update is called once per frame
    void Update()
    {
        Update_time();
        Update_lightAngle();
    }

    void Update_time()
    {
        Update_second();
        // 1�ð� = 60�� * 60�� -> 3600
        this.time = (float)(this.second / 3600);
    }

    private void Update_second()
    {
        Set_second(Time.deltaTime / GAME_TIME);
    }

    void Set_second(double sec)
    {
        // 0���� �������� ������ 0���� ����
        this.second += sec > 0 ? sec : 0;
        // �Ϸ簡 86400�� 24�ô� �� ������ 0��
        int deltaDay= (int)(this.second / 86400);
        this.day += deltaDay;
        this.second %= 86400;
        // �ŵ��� ���� ����
        // GetOldAllBeliever(deltaDay);
    }

    void Set_time(float tTime)
    {
        // 0���� �������� ������ 0���� ����
        tTime = tTime > 0 ? tTime : 0;
        // 24�ð��� �Ѿ�� �߶�
        tTime %= 24;
        // 1�ð� = 60�� * 60�� -> 3600
        Set_second(tTime * 3600);
    }
    private void Update_lightAngle()
    {
        // ���� ���� ���̸� ����
        this.angleModifier = (this.time > 4) && (this.time < 22) ? ANGLE_MODIFIER_DAY : ANGLE_MODIFIER_NIGHT;
        // �ð��� ���� �¾��� ���� ���� (�ð��÷ο���Ʈ: ���ǽð����� -> ���ǽð�����)
        // 0�� �ѹ��� -90��
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
