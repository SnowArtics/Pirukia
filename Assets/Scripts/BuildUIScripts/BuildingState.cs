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
    private List<int> countStructure = new List<int>() ;

    public int get(int index) { return countStructure[index]; }
    public void addBuilding(int index) { countStructure[index]++; }
    public void removeBuilding(int index) { countStructure[index]--; }

    public void Awake() {
        int count = 0;
        IDataReader dataReader = null;
        database = dbManage.GetComponent<DatabaseManage>();
        database.DBCreate();

        dataReader = database.ExecuteDB("SELECT Count(*) FROM build");

        while (dataReader.Read()) {
            count = dataReader.GetInt32(0);
        }

        for(int i = 0; i < count; i++) {
            countStructure.Add(0);
        }
    }
}
