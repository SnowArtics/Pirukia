using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateChangeOverTime : MonoBehaviour
{
    // �ŵ��� �ῡ����� ���� 0:��, 1:Ȱ��
    [SerializeField]
    private int WakeUpTrigger = 1;

    public int Get_WakeUpTrigger()
    {
        return WakeUpTrigger;
    }

    public void Set_WakeUpTrigger(int trig)
    {
        this.WakeUpTrigger = trig;
    }

    public void GetOldAllBeliever()
    {

    }

    public void FallAsleepAllBeliever()
    {

    }

    public void WakeUpAllBeliever()
    {

    }
}
