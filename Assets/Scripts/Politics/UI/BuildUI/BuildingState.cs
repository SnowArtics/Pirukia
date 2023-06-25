using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class BuildingState : MonoBehaviour
{
    [SerializeField]
    private GameObject dbManage;

    private DatabaseManage database;
    private Dictionary<int, int> countStructure = new Dictionary<int, int>();                     // �ǹ� ������ �����ϴ� ����Ʈ

    public int get(int index) { return countStructure[index]; }             // �ǹ� ������ �޾ƿ��� �Լ�
    public void addBuilding(int index) { countStructure[index]++; }         // �ǹ��� �������� ������ +1
    public void removeBuilding(int index) { countStructure[index]--; }      // �ǹ��� ������� ������ -1

    public void Awake() {
        int count = 0;
        IDataReader dataReader;
        database = dbManage.GetComponent<DatabaseManage>();
        database.DBCreate();

        // DB���� �� �ǹ��� ID�� �޾ƿ� ������ 0���� �ʱ�ȭ �Ѵ�.
        dataReader = database.ExecuteDB("SELECT Id FROM building");      
        while (dataReader.Read()) {
            int id = dataReader.GetInt32(0);
            countStructure[id] = 0;
            count++;
        }
    }
}
