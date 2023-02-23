using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BuildingState : MonoBehaviour
{
    [SerializeField]
    private GameObject dbManage;

    private DatabaseManage database;
    private List<int> countStructure = new List<int>();                     // �ǹ� ������ �����ϴ� ����Ʈ

    public int get(int index) { return countStructure[index]; }             // �ǹ� ������ �޾ƿ��� �Լ�
    public void addBuilding(int index) { countStructure[index]++; }         // �ǹ��� �������� ������ +1
    public void removeBuilding(int index) { countStructure[index]--; }      // �ǹ��� ������� ������ -1

    public void Awake() {
        int count = 0;
        IDataReader dataReader;
        database = dbManage.GetComponent<DatabaseManage>();
        database.DBCreate();

        dataReader = database.ExecuteDB("SELECT Count(*) FROM build");      // DB���� �ǹ��� ���� ���� �޾ƿ´�.

        while (dataReader.Read()) {
            count = dataReader.GetInt32(0);
        }

        for(int i = 0; i < 10; i++) {
            countStructure.Add(0);                  // �ǹ� ���� �⺻���� 0���� �����Ѵ�.
        }
    }
}
