using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class BuildUIPreset : MonoBehaviour
{
    [SerializeField]
    private GameObject dbManageSystem;
    [SerializeField]
    private List<TextMeshProUGUI> villageNameLists = new List<TextMeshProUGUI>();
    [SerializeField]
    private List<TextMeshProUGUI> villageInfoLists = new List<TextMeshProUGUI>();

    private DatabaseManage database;
//    private List<string> structureNames = new List<string>();

    public void Awake() {
        database = dbManageSystem.GetComponent<DatabaseManage>();
        database.DBCreate();        // DB를 연다.

        // 버튼의 이름과 정보를 build 테이블의 정보로 변경
        for (int i = 0; i < villageNameLists.Count; i++) {
            string name = database.DBReadOne("BUILDING_NAME", i+1);        // DB에서 건물 이름을 받아온다.
//            structureNames.Add(name);
            villageNameLists[i].text = name;                                // 받아온 이름들을 실제 버튼의 text로 변경

            IDataReader content = database.DBReadLine(i+1);                 // DB에서 건물에 해당하는 데이터값을 받아온다.
            StructureInfo(content, i);                                      // 버튼의 정보를 DB 값으로 변경

        }
    }
    
    public void StructureInfo(IDataReader data, int index) {
        string info = string.Empty;
        int countWood = 0;
        int countMetal = 0;
        int countMoney = 0;
        int countTime = 0;

        while (data.Read()) {
            countWood = data.GetInt32(3);
            countMetal = data.GetInt32(4);
            countMoney = data.GetInt32(5);
            countTime = data.GetInt32(6);
        }

            info = countWood.ToString() + "/" + countMetal.ToString() + "/" + countMoney.ToString() + "/" + countTime.ToString();

        villageInfoLists[index].text = info;
    }
}
