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
    private Dictionary<int, int> countStructure = new Dictionary<int, int>();                     // 건물 개수를 저장하는 리스트

    public int getBuilding(int index) { return countStructure[index]; }             // 건물 개수를 받아오는 함수
    public void addBuilding(int index) { countStructure[index]++; }         // 건물이 지어지면 개수를 +1
    public void removeBuilding(int index) { countStructure[index]--; }      // 건물이 사라지면 개수를 -1

    public void Awake() {
        int count = 0;
        IDataReader dataReader;
        database = dbManage.GetComponent<DatabaseManage>();
        database.DBCreate();

        // DB에서 각 건물의 ID를 받아와 개수를 0으로 초기화 한다.
        dataReader = database.ExecuteDB("SELECT Id FROM building");      
        while (dataReader.Read()) {
            int id = dataReader.GetInt32(0);
            countStructure[id] = 0;
            count++;
        }
    }
}
