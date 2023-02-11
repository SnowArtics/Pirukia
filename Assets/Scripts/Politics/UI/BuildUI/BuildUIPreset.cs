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
        database.DBCreate();        // DB�� ����.

        // ��ư�� �̸��� ������ build ���̺��� ������ ����
        for (int i = 0; i < villageNameLists.Count; i++) {
            string name = database.DBSelectOne("BUILDING_NAME", i+1);               // DB���� �ǹ� �̸��� �޾ƿ´�.
            villageNameLists[i].text = name;                                        // �޾ƿ� �̸����� ���� ��ư�� text�� ����
            villageButtonLists[i].name = name;                                      // ��ư�� �̸��� ���� �ǹ� ������ ����
            
        }
    }

    public void Update() {
        for(int i = 0; i < 10; i++) {
            IDataReader content = database.DBSelectLine(i + 1);                         // DB���� �ǹ��� �ش��ϴ� �����Ͱ��� �޾ƿ´�.
            strRequire = StructureInfo(content, i) + "\n" + StructureCount(i);          // �Ǽ��� �ʿ��� �ڿ��� ���� ������ string������ �����Ѵ�.
//            villageButtonLists[i].text = strRequire;                                      // �� ����Ʈ�� �ش� ���ڿ��� �����Ѵ�.
        }
    }

    // �Ǽ��� �ʿ��� �ڿ��� DB���� �޾ƿ��� �Լ�
    public string StructureInfo(IDataReader data, int index) {
        string info = string.Empty;
        int countWood = 0;
        int countMetal = 0;
        int countMoney = 0;
        int countTime = 0;

        // build ���̺��� �ʿ� ��(����, �ݼ�, ��, �ҿ� �ð�)�� �޾ƿ´�.
        while (data.Read()) {
            countWood = data.GetInt32(3);
            countMetal = data.GetInt32(4);
            countMoney = data.GetInt32(5);
            countTime = data.GetInt32(6);
        }

        // ��ư�� ������ DB���� �ƴ϶� ��ĭ���� ����Ѵ�.
        if (villageNameLists[index].text == "-") {
            return "N/A";
        }
        else {
            info = "����\t: " + countWood.ToString() + "\n����\t: " + countMetal.ToString() + "\n��\t: " + countMoney.ToString() + "\n�ð�\t: " + countTime.ToString();
            return info;
        }
    }

    // �ǹ��� ���� ������ �� ���� ������ ����ϴ� �Լ�
    public string StructureCount(int index) {
        string countText = string.Empty;
        string totalText = database.DBSelectOne("LIMIT_BUILDING", index + 1);       // build ���̺��� �� �Ǽ� ���� ���� �޾ƿ´�.
        int count = buildingState.get(index);                                       // ���� �Ǽ��Ǿ� �ִ� ���๰�� ���� �޾ƿ´�.
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
