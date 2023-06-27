using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManagement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���⿡�� ���ҽ� UI�� �� �����ϴ� �θ� �־��ּ���")]
    private GameObject resource;

    private float industryStorageNum = -1;
    private float industryUsedSpaceNum = 0;
    private float woodPlanksNum = -1;
    private float stoneNum = -1;

    private float foodStorageNum = 0;
    private float foodUsedSpaceNum = 0;
    private float appleNum = 0;

    private float religiosityNum = 0;

    [SerializeField]
    private GameObject IndustryStorage;
    [SerializeField]
    private GameObject WoodPlanks;
    [SerializeField]
    private GameObject Stone;
    [SerializeField]
    private GameObject FoodStorage;
    [SerializeField]
    private GameObject Apple;
    [SerializeField]
    private GameObject Religiosity;

    private TextMeshProUGUI IndustryStorageText;
    private TextMeshProUGUI WoodPlanksText;
    private TextMeshProUGUI StoneText;
    private TextMeshProUGUI FoodStorageText;
    private TextMeshProUGUI AppleText;
    private TextMeshProUGUI ReligiosityText;

    void Awake()
    {
        industryStorageNum = 1000;
        woodPlanksNum = 100;
        stoneNum = 100;
        industryUsedSpaceNum = woodPlanksNum + stoneNum;
        religiosityNum = 100;

        IndustryStorageText = IndustryStorage.GetComponent<TextMeshProUGUI>();
        WoodPlanksText = WoodPlanks.GetComponent<TextMeshProUGUI>();
        StoneText = Stone.GetComponent<TextMeshProUGUI>();
        FoodStorageText = FoodStorage.GetComponent<TextMeshProUGUI>();
        AppleText = Apple.GetComponent<TextMeshProUGUI>();
        ReligiosityText = Religiosity.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        industryUsedSpaceNum = woodPlanksNum + stoneNum;
        foodUsedSpaceNum = appleNum;

        IndustryStorageText.text = ((int)industryUsedSpaceNum).ToString() + " / " + ((int)industryStorageNum).ToString();
        WoodPlanksText.text = ((int)woodPlanksNum).ToString();
        StoneText.text = ((int)stoneNum).ToString();
        FoodStorageText.text = ((int)foodUsedSpaceNum).ToString() + " / " + ((int)foodStorageNum).ToString();
        AppleText.text = ((int)appleNum).ToString();
        ReligiosityText.text = ((int)religiosityNum).ToString();
    }

    
    //�� ���� Get, Set �Լ����Դϴ�.
    float GetIndustryStorage()
    {
        return industryStorageNum;
    }
    float GetWoodPlanks()
    {
        return woodPlanksNum;
    }
    float GetStone()
    {
        return stoneNum;
    }
    float GetFoodStorage()
    {
        return foodStorageNum;
    }
    float GetApple()
    {
        return appleNum;
    }
    float GetReligiosity()
    {
        return religiosityNum;
    }

    void SetIndustryStorage(float _industryStoramge)
    {
        industryStorageNum = _industryStoramge;
    }
    void SetWoodPlanks(float _woodPlanks)
    {
        woodPlanksNum = _woodPlanks;
    }
    void SetStone(float _stone)
    {
        stoneNum = _stone;
    }
    void SetFoodStorage(float _foodStorage)
    {
        foodStorageNum = _foodStorage;
    }
    void SetApple(float _apple)
    {
        appleNum = _apple;
    }
    void SetReligiosity(float _religiosity)
    {
        religiosityNum = _religiosity;
    }
}
