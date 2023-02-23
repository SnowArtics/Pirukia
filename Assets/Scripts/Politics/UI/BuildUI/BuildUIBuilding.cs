using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using UnityEngine;

public class BuildUIBuilding : MonoBehaviour {
    private GameObject tooltip;
    private GameObject eventSystem;
    private DatabaseManage dbSystem;
    private ErrorEventManager errEventMng;
    private BuildingState buildState;
    private int buildCode;

    private int buildingCount;                      // ���� �Ǽ����� �ǹ� ����
    private int buildedCount;                       // �Ǽ� �Ϸ�� �ǹ� ����
    private int limitCount;                         // �Ǽ� ������ �ǹ� ����

    private void Awake() {
        buildingCount = 0;
        buildedCount = 0;
        limitCount = 0; 

        buildState = (GameObject.Find("BuildUIEventSystem")).GetComponent<BuildingState>();
        dbSystem = GameObject.Find("DatabaseSystem").GetComponent<DatabaseManage>();
        eventSystem = GameObject.Find("EventSystem");
        errEventMng = eventSystem.GetComponent<ErrorEventManager>();

 //       GameObject canvas = GameObject.Find("Canvas");
//       tooltip = (canvas.transform.GetChild(5)).gameObject;
    }

    public void onClickButton() {
        
        buildedCount = chkBuildedBuild();
        limitCount = chkLimitBuild();
        int curBuild = buildingCount + buildedCount;

        if (limitCount == -1 || limitCount > curBuild) {
 //           BuildUITooltip uITooltip = this.GetComponent<BuildUITooltip>();
 //           uITooltip.onTooltip();

            Debug.Log(this.name + " �Ǽ� ����!!");
            buildState.addBuilding(buildCode - 1);
            // �ǹ� ����
        }
        else if (limitCount == -2) { }
        else {
            errEventMng.ExceedLimitError();
        }
    }

    public int chkLimitBuild() {
        string buttonName = this.name;
        if (buttonName == "-" || buttonName.StartsWith("Str")) { return -2; }
        else {
            IDataReader codeData = dbSystem.ExecuteDB("SELECT LIMIT_BUILDING from build where BUILDING_NAME=\"" + buttonName + "\"");
            int buildLimit = int.Parse(codeData.GetValue(0).ToString());

            return buildLimit;
        }
    }

    public int chkBuildedBuild() {
        string buttonName = this.name;
        if (buttonName == "-" || buttonName.StartsWith("Str")) { return -2; }
        else {
            IDataReader codeData = dbSystem.ExecuteDB("SELECT * from build where BUILDING_NAME=\"" + buttonName + "\"");
            buildCode = int.Parse(codeData.GetValue(0).ToString());

            int buildCount = buildState.get(buildCode - 1);

            return buildCount;
        }
    }
}
