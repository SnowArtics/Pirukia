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
        database.DBCreate();        // DB�� ����.

        // ����Ʈ�� �ǹ� �ڵ� ��� ����
        IDataReader dataReader = database.ExecuteDB("SELECT Id,Name FROM building");
        while (dataReader.Read()) {
            buildingDicts.Add(dataReader.GetInt32(0), dataReader.GetString(1));
        }

        int ri = 0; int fi = 0; int ei = 0; int ii = 0; int li = 0; int oi = 0;
        foreach(KeyValuePair<int, string> building in buildingDicts) {
            string nameKr = database.DBSelectOne("Name_KR", building.Key);
            int grpBuilding = building.Key / 10;

            switch(grpBuilding) {
                case 0:
                case 1:
                    residentButtonLists[ri].name = building.Value;
                    residentButtonLists[ri].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    ri++;
                    break;
                case 2:
                case 3:
                    foodButtonLists[fi].name = building.Value;
                    foodButtonLists[fi].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    fi++;
                    break;
                case 4:
                    economyButtonLists[ei].name = building.Value;
                    economyButtonLists[ei].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    ei++;
                    break;
                case 5:
                case 6:
                    industryButtonLists[ii].name = building.Value;
                    industryButtonLists[ii].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    ii++;
                    break;
                case 7:
                    religionButtonLists[li].name = building.Value;
                    religionButtonLists[li].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    li++;
                    break;
                case 8:
                case 9:
                    otherButtonLists[oi].name = building.Value;
                    otherButtonLists[oi].GetComponentInChildren<TextMeshProUGUI>().text = nameKr;
                    oi++;
                    break;
            }
        }
    }

    public void Update() {
        foreach (int idx in buildingDicts.Keys) {
            IDataReader content = database.DBSelectLine(idx);                           // DB���� �ǹ��� �ش��ϴ� �����Ͱ��� �޾ƿ´�.
            SetSpec(idx, StructureInfo(content, idx) + "\n" + StructureCount(idx));     // �Ǽ��� �ʿ��� �ڿ��� ���� ������ specBuild ����Ʈ�� �����Ѵ�.
        }
    }

    // �Ǽ��� �ʿ��� �ڿ��� DB���� �޾ƿ��� �Լ�
    public string StructureInfo(IDataReader data, int index) {
        string info = string.Empty;
        int countWoodPlank = 0;         // ���� ���� ����(101)
        int countStone = 0;             // ���� ����(102)
        int countIron = 0;              // ö ����(103)
        int countGold = 0;              // �� ����(201)
        int countReligiosity = 0;       // �žӽ� ����(301)
        int countTime = 0;              // �ҿ� �ð�

        Dictionary<int, int> countResource = new Dictionary<int, int>();

        // build ���̺��� �ʿ� ��(����, �ݼ�, ��, �ҿ� �ð�)�� �޾ƿ´�.
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
                    info += "���� ����\t: " + countWoodPlank.ToString() + "\n";
                    break;
                case 102:
                    countStone = pair.Value;
                    info += "����\t\t: " + countStone.ToString() + "\n";
                    break;
                case 103:
                    countIron = pair.Value;
                    info += "ö\t\t: " + countIron.ToString() + "\n";
                    break;
                case 201:
                    countGold = pair.Value;
                    info += "��\t\t: " + countGold.ToString() + "\n";
                    break;
                case 301:
                    countReligiosity = pair.Value;
                    info += "�žӽ�\t: " + countReligiosity.ToString() + "\n";
                    break;
            }
        }

        info += "�ð�\t\t: " + countTime.ToString();
        return info;
    }

    // �ǹ��� ���� ������ �� ���� ������ ����ϴ� �Լ�
    public string StructureCount(int index) {
        string countText = string.Empty;
        string totalText = database.DBSelectOne("Limit_Building", index);       // build ���̺��� �� �Ǽ� ���� ���� �޾ƿ´�.
        int count = buildingState.get(index) ;                                   // ���� �Ǽ��Ǿ� �ִ� ���๰�� ���� �޾ƿ´�.
        int total;

        // ��ư�� ������ DB���� �ƴ϶� ������ ����Ѵ�.
        if (totalText == "-") {
            return countText;
        }
        else {
            total = int.Parse(totalText);           // ������ �޾ƿ� �� �Ǽ� ���� ������ int������ ��ȯ�Ѵ�.
        }

        // DB���� �Ǽ� ���� ���� -1�� ���Ѵ� �Ǽ� �����̹Ƿ�, ���Ѵ� ��ȣ�� ����Ѵ�.
        if (total < 0) {
            countText = "���๰\t: " + count.ToString() + "/��";
        }
        else {
            countText = "���๰\t: " + count.ToString() + "/" + total.ToString();
        }

        return countText;
    }
}
