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
    private List<int> countStructure = new List<int>();                     // 건물 개수를 저장하는 리스트

    public int get(int index) { return countStructure[index]; }             // 건물 개수를 받아오는 함수
    public void addBuilding(int index) { countStructure[index]++; }         // 건물이 지어지면 개수를 +1
    public void removeBuilding(int index) { countStructure[index]--; }      // 건물이 사라지면 개수를 -1

    public void Awake() {
        int count = 0;
        IDataReader dataReader;
        database = dbManage.GetComponent<DatabaseManage>();
        database.DBCreate();

        dataReader = database.ExecuteDB("SELECT Count(*) FROM build");      // DB에서 건물의 가지 수를 받아온다.

        while (dataReader.Read()) {
            count = dataReader.GetInt32(0);
        }

        for(int i = 0; i < 10; i++) {
            countStructure.Add(0);                  // 건물 개수 기본값을 0으로 설정한다.
        }
    }
}
