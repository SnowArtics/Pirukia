using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildUIPreset : MonoBehaviour
{
    [SerializeField]
    private GameObject dbManageSystem, resourceSystem;
    [SerializeField]
    private List<GameObject> residentButtonLists = new List<GameObject>();
    [SerializeField]
    private List<GameObject> foodButtonLists = new List<GameObject>();
    [SerializeField]
    private List<GameObject> economyButtonLists = new List<GameObject>();
    [SerializeField]
    private List<GameObject> industryButtonLists = new List<GameObject>();
    [SerializeField]
    private List<GameObject> religionButtonLists = new List<GameObject>();
    [SerializeField]
    private List<GameObject> otherButtonLists = new List<GameObject>();


    private DatabaseManage database;
    private BuildingState buildingState;
    private Dictionary<int, string> specBuild = new Dictionary<int, string>();
    private Dictionary<int, string> buildingDicts = new Dictionary<int, string>();

    public string GetSpec(int index) { return specBuild[index]; }
    public void SetSpec(int index, string str) { specBuild[index] = str; }

    public void Awake() {
        buildingState = GetComponent<BuildingState>();
        database = dbManageSystem.GetComponent<DatabaseManage>();
        database.DBCreate();        // DB를 연다.

        // 리스트에 건물 코드 목록 저장
        IDataReader dataReader = database.ExecuteDB("SELECT Id,Name FROM building");
        while (dataReader.Read()) {
            buildingDicts.Add(dataReader.GetInt32(0), dataReader.GetString(1));
        }

        residentButtonLists[0].name = buildingDicts[0];
        foodButtonLists[0].name = buildingDicts[20];
        foodButtonLists[1].name = buildingDicts[21];
        religionButtonLists[0].name = buildingDicts[70];
        /*
        int ri = 0; int fi = 0; int ei = 0; int ii = 0; int li = 0; int oi = 0;
        foreach(KeyValuePair<int, string> building in buildingDicts) {
            string nameKr = database.DBSelectOne("Name_KR", building.Key);
            int grpBuilding = building.Key / 10;
            /* 각 분류 코드 별로 건물을 별도의 리스트에 저장
            switch(grpBuilding) {
                // ID 00~19일 경우 주거 관련 건물
                case 0:
                case 1:
                    residentButtonLists[ri].name = building.Value;
                    residentButtonLists[ri].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    ri++;
                    break;
                // ID 20~39일 경우 식량 관련 건물
                case 2:
                case 3:
                    foodButtonLists[fi].name = building.Value;
                    foodButtonLists[fi].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    fi++;
                    break;
                // ID 40~49일 경우 경제 관련 건물
                case 4:
                    economyButtonLists[ei].name = building.Value;
                    economyButtonLists[ei].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    ei++;
                    break;
                // ID 50~69일 경우 산업 관련 건물
                case 5:
                case 6:
                    industryButtonLists[ii].name = building.Value;
                    industryButtonLists[ii].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    ii++;
                    break;
                // ID 70~79일 경우 신앙 관련 건물
                case 7:
                    religionButtonLists[li].name = building.Value;
                    religionButtonLists[li].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    li++;
                    break;
                // ID 80~99일 경우 기타(군사) 관련 건물
                case 8:
                case 9:
                    otherButtonLists[oi].name = building.Value;
                    otherButtonLists[oi].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    oi++;
                    break;
            }
        } */
    }

    public void Update() {
        foreach (int idx in buildingDicts.Keys) {
            IDataReader content = database.DBSelectLine(idx);                           // DB에서 건물에 해당하는 데이터값을 받아온다.
            SetSpec(idx, StructureInfo(content, idx) + "\n" + StructureCount(idx));     // 건설에 필요한 자원과 현재 개수를 specBuild 리스트에 저장한다.
        }
    }

    /* 건설에 필요한 자원을 DB에서 받아오는 함수 */
    public string StructureInfo(IDataReader data, int index) {
        string info = string.Empty;
        int countWoodPlank = 0;         // 나무 판자 개수(101)
        int countStone = 0;             // 석재 개수(102)
        int countIron = 0;              // 철 개수(103)
        int countGold = 0;              // 금 개수(201)
        int countReligiosity = 0;       // 신앙심 개수(301)
        int countTime = 0;              // 소요 시간

        Dictionary<int, int> countResource = new Dictionary<int, int>();

        // build 테이블에서 필요 값(나무, 금속, 돈, 소요 시간)을 받아온다.
        while (data.Read()) {
            string[] tmpRsc = (data.GetString(4)).Split(',');
            string[] tmpRscAm = (data.GetString(5)).Split(',');
            countTime = data.GetInt32(6);

            for(int i = 0; i < tmpRsc.Length; i++) {
                countResource.Add(int.Parse(tmpRsc[i]), int.Parse(tmpRscAm[i]));
            }
        }

        foreach (KeyValuePair<int, int> pair in countResource) {
            switch(pair.Key) {
                case 101:
                    countWoodPlank = pair.Value;
                    info += "나무 판자\t: " + countWoodPlank.ToString() + "\n";
                    break;
                case 102:
                    countStone = pair.Value;
                    info += "석재\t\t: " + countStone.ToString() + "\n";
                    break;
                case 103:
                    countIron = pair.Value;
                    info += "철\t\t: " + countIron.ToString() + "\n";
                    break;
                case 201:
                    countGold = pair.Value;
                    info += "금\t\t: " + countGold.ToString() + "\n";
                    break;
                case 301:
                    countReligiosity = pair.Value;
                    info += "신앙심\t: " + countReligiosity.ToString() + "\n";
                    break;
            }
        }

        info += "시간\t\t: " + countTime.ToString();
        return info;
    }

    // 건물의 현재 개수와 총 가능 개수를 출력하는 함수
    public string StructureCount(int index) {
        string countText = string.Empty;
        string totalText = database.DBSelectOne("Limit_Building", index);       // build 테이블에서 총 건설 가능 수를 받아온다.
        int count = buildingState.get(index) ;                                   // 현재 건설되어 있는 건축물의 수를 받아온다.
        int total;

        // 버튼이 남으면 DB값이 아니라 공백을 출력한다.
        if (totalText == "-") {
            return countText;
        }
        else {
            total = int.Parse(totalText);           // 위에서 받아온 총 건설 가능 개수를 int형으로 변환한다.
        }

        // DB에서 건설 가능 개수 -1은 무한대 건설 가능이므로, 무한대 기호로 출력한다.
        if (total < 0) {
            countText = "건축물\t: " + count.ToString() + "/∞";
        }
        else {
            countText = "건축물\t: " + count.ToString() + "/" + total.ToString();
        }

        return countText;
    }
}
