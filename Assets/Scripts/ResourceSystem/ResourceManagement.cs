using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManagement : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI AppleText, WoodPlanksText, StoneText, ReligiosityText, FoodStorageText, IndustryStorageText;
    // 생산 자원의 값을 저장
    private Dictionary<int, float> resourceNum = new Dictionary<int, float>();

    public float GetResourceNum(int idx) { return resourceNum[idx]; }
    public void SetResourceNum(int idx, float value) {
        resourceNum[idx] = value;

        // 변경된 자원 값을 UI에 반영
        foreach (KeyValuePair<int, float> r in resourceNum) {
            if (r.Key / 100 == 1) { industryUsedSpaceNum += r.Value; }
            if (r.Key / 100 == 0) { foodUsedSpaceNum += r.Value; }
        }
    }

    private float industryUsedSpaceNum = 0;
    private float foodUsedSpaceNum = 0;

    void Awake()
    {
        resourceNum[1] = 0;
        resourceNum[101] = 100;          // UI 상에 나타나지 않게 해야 함
        resourceNum[102] = 100;          // UI 상에 나타나지 않게 해야 함

        resourceNum[301] = 100;
        resourceNum[401] = 0;
        resourceNum[402] = 1000;
        resourceNum[403] = 0;

        foreach (KeyValuePair<int, float> r in resourceNum) {
            if (r.Key / 100 == 1) { industryUsedSpaceNum = r.Value; }
            if (r.Key / 100 == 0) { foodUsedSpaceNum += r.Value; }
        }
    }

    // Update is called once per frame
    void Update()
    {
        IndustryStorageText.text = ((int)industryUsedSpaceNum).ToString() + " / " + ((int)resourceNum[402]).ToString();
        WoodPlanksText.text = ((int)resourceNum[101]).ToString();
        StoneText.text = ((int)resourceNum[102]).ToString();
        FoodStorageText.text = ((int)foodUsedSpaceNum).ToString() + " / " + ((int)resourceNum[401]).ToString();
        AppleText.text = ((int)resourceNum[1]).ToString();
        ReligiosityText.text = ((int)resourceNum[301]).ToString();
    }
}
