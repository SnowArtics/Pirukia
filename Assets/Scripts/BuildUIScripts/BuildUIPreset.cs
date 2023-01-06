using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildUIPreset : MonoBehaviour
{
    [SerializeField]
    private GameObject dbManageSystem;
    [SerializeField]
    private List<TextMeshProUGUI> villageLists = new List<TextMeshProUGUI>();
    [SerializeField]
    private List<TextMeshProUGUI> religionLists = new List<TextMeshProUGUI>();
    [SerializeField]
    private List<TextMeshProUGUI> otherLists = new List<TextMeshProUGUI>();

    private DatabaseManage database;
    private List<string> structureNames = new List<string>();

    public void Awake() {
        database = dbManageSystem.GetComponent<DatabaseManage>();
        database.DBCreate();        // DB를 연다.

        // 버튼의 이름을 DB의 건물 이름(BUILDING_CODE)로 변경
        for (int i = 0; i < villageLists.Count; i++) {
            string name = database.DBRead("SELECT BUILDING_NAME FROM build where BUILDING_CODE=" + (i+1).ToString());
            structureNames.Add(name);                       // 받아온 이름을 structureNames 리스트에 추가
            
            villageLists[i].text = structureNames[i];       // 받아온 이름들을 실제 버튼의 text로 변경
        }
    }
}
