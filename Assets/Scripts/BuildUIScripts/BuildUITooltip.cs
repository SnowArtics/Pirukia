using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildUITooltip : MonoBehaviour
{
    [SerializeField]
//    private GameObject dbManage;

    private DatabaseManage database;
    IDataReader dataReader;
    private string buttonName;

    public void Awake() {
//        database = dbManage.GetComponent<DatabaseManage>();
//        database.DBCreate();
    }

    private void Update() {

    }

    public void onTooltip() {
        buttonName = this.name;
        Debug.Log(buttonName + " is on.");
    }

    public void offTooltip() {
        buttonName = this.name;
        Debug.Log(buttonName + " is out.");
    }
}
