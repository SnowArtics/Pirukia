using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateChangeOverTime : MonoBehaviour
{
    // 신도가 잠에드는지 설정 0:잠, 1:활동
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
