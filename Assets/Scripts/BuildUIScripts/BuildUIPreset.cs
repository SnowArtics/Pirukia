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
        database.DBCreate();        // DB�� ����.

        // ��ư�� �̸��� DB�� �ǹ� �̸�(BUILDING_CODE)�� ����
        for (int i = 0; i < villageLists.Count; i++) {
            string name = database.DBRead("SELECT BUILDING_NAME FROM build where BUILDING_CODE=" + (i+1).ToString());
            structureNames.Add(name);                       // �޾ƿ� �̸��� structureNames ����Ʈ�� �߰�
            
            villageLists[i].text = structureNames[i];       // �޾ƿ� �̸����� ���� ��ư�� text�� ����
        }
    }
}
