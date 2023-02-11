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
    private GameObject dbManageSystem;
    [SerializeField]
    private List<TextMeshProUGUI> villageNameLists = new List<TextMeshProUGUI>();
    [SerializeField]
    private List<GameObject> villageButtonLists = new List<GameObject>();

    private DatabaseManage database;
    private BuildingState buildingState;
    private string strRequire;

    public void Awake() {
        strRequire = string.Empty;
        buildingState = GetComponent<BuildingState>();
        database = dbManageSystem.GetComponent<DatabaseManage>();
        database.DBCreate();        // DB를 연다.

        // 버튼의 이름과 정보를 build 테이블의 정보로 변경
        for (int i = 0; i < villageNameLists.Count; i++) {
            string name = database.DBSelectOne("BUILDING_NAME", i+1);               // DB에서 건물 이름을 받아온다.
            villageNameLists[i].text = name;                                        // 받아온 이름들을 실제 버튼의 text로 변경
            villageButtonLists[i].name = name;                                      // 버튼의 이름을 실제 건물 명으로 변경
            
        }
    }

    public void Update() {
        for(int i = 0; i < 10; i++) {
            IDataReader content = database.DBSelectLine(i + 1);                         // DB에서 건물에 해당하는 데이터값을 받아온다.
            strRequire = StructureInfo(content, i) + "\n" + StructureCount(i);          // 건설에 필요한 자원과 현재 개수를 string형으로 저장한다.
//            villageButtonLists[i].text = strRequire;                                      // 각 리스트에 해당 문자열을 저장한다.
        }
    }

    // 건설에 필요한 자원을 DB에서 받아오는 함수
    public string StructureInfo(IDataReader data, int index) {
        string info = string.Empty;
        int countWood = 0;
        int countMetal = 0;
        int countMoney = 0;
        int countTime = 0;

        // build 테이블에서 필요 값(나무, 금속, 돈, 소요 시간)을 받아온다.
        while (data.Read()) {
            countWood = data.GetInt32(3);
            countMetal = data.GetInt32(4);
            countMoney = data.GetInt32(5);
            countTime = data.GetInt32(6);
        }

        // 버튼이 남으면 DB값이 아니라 빈칸임을 출력한다.
        if (villageNameLists[index].text == "-") {
            return "N/A";
        }
        else {
            info = "목재\t: " + countWood.ToString() + "\n석재\t: " + countMetal.ToString() + "\n돈\t: " + countMoney.ToString() + "\n시간\t: " + countTime.ToString();
            return info;
        }
    }

    // 건물의 현재 개수와 총 가능 개수를 출력하는 함수
    public string StructureCount(int index) {
        string countText = string.Empty;
        string totalText = database.DBSelectOne("LIMIT_BUILDING", index + 1);       // build 테이블에서 총 건설 가능 수를 받아온다.
        int count = buildingState.get(index);                                       // 현재 건설되어 있는 건축물의 수를 받아온다.
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
