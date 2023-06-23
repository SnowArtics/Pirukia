using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using UnityEngine;

public class BuildUIBuilding : MonoBehaviour {
    private GameObject eventSystem;
    private GameObject buildUIEventSystem;
    private DatabaseManage dbSystem;
    private ErrorEventManager errEventMng;
    private BuildingState buildState;
    private int buildId;
    private string buttonName;
    private List<string> buildingDataList = new List<string>();

    private int buildingCount;                      // ���� �Ǽ����� �ǹ� ����
    private int buildedCount;                       // �Ǽ� �Ϸ�� �ǹ� ����
    private int limitCount;                         // �Ǽ� ������ �ǹ� ����

    private void Awake() {
        buildingCount = 0;
        buildedCount = 0;
        limitCount = 0;
        buildUIEventSystem = GameObject.Find("BuildUIEventSystem");

        buildState = buildUIEventSystem.GetComponent<BuildingState>();
        dbSystem = GameObject.Find("DatabaseSystem").GetComponent<DatabaseManage>();
        eventSystem = GameObject.Find("EventSystem");
        errEventMng = eventSystem.GetComponent<ErrorEventManager>();

 //       GameObject canvas = GameObject.Find("Canvas");
//       tooltip = (canvas.transform.GetChild(5)).gameObject;
    }

    public void onClickButton() {
        buttonName = this.name;
        List<string> buildingDataList = dbSystem.DBSelectBuilding(buttonName);
        buildId = int.Parse(buildingDataList[0]);
        buildedCount = chkBuildedBuild();
        limitCount = chkLimitBuild();
        int curBuild = buildingCount + buildedCount;

        if (limitCount == -1 || limitCount > curBuild) {
 //           BuildUITooltip uITooltip = this.GetComponent<BuildUITooltip>();
 //           uITooltip.onTooltip();

            Debug.Log(buildingDataList[1] + " �Ǽ� ����!!");
            buildState.addBuilding(buildId);

            // ���๰�� ���� DB ���� buildingToEdit �Լ��� ������ ���� ���� �����Ѵ�.
            buildUIEventSystem.GetComponent<BuildUIEditMode>().buildingToEdit(buildingDataList);
        }
        else if (limitCount == -2) { }
        else {
            errEventMng.ExceedLimitError();
        }
    }

    // DB���� ���๰�� �Ǽ� ���� ���� �޾ƿ´�.
    public int chkLimitBuild() {
        if (buttonName == "-" || buttonName.StartsWith("Str")) { return -2; }
        else {
            int buildLimit = int.Parse((dbSystem.DBSelectBuilding(buttonName))[12]);

            return buildLimit;
        }
    }

    // ���� ������ ���๰�� ������ �޾ƿ´�.
    public int chkBuildedBuild() {
        if (buttonName == "-" || buttonName.StartsWith("Str")) { return -2; }
        else {
            int buildCode = int.Parse((dbSystem.DBSelectBuilding(buttonName))[0]);
            int buildCount = buildState.get(buildCode);

            return buildCount;
        }
    }
}
