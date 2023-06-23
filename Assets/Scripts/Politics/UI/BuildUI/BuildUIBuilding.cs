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

    private int buildingCount;                      // 현재 건설중인 건물 개수
    private int buildedCount;                       // 건설 완료된 건물 개수
    private int limitCount;                         // 건설 가능한 건물 개수

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

            Debug.Log(buildingDataList[1] + " 건설 시작!!");
            buildState.addBuilding(buildId);

            // 건축물에 대한 DB 값을 buildingToEdit 함수로 전달해 편집 모드로 진행한다.
            buildUIEventSystem.GetComponent<BuildUIEditMode>().buildingToEdit(buildingDataList);
        }
        else if (limitCount == -2) { }
        else {
            errEventMng.ExceedLimitError();
        }
    }

    // DB에서 건축물의 건설 제한 수를 받아온다.
    public int chkLimitBuild() {
        if (buttonName == "-" || buttonName.StartsWith("Str")) { return -2; }
        else {
            int buildLimit = int.Parse((dbSystem.DBSelectBuilding(buttonName))[12]);

            return buildLimit;
        }
    }

    // 현재 지어진 건축물의 개수를 받아온다.
    public int chkBuildedBuild() {
        if (buttonName == "-" || buttonName.StartsWith("Str")) { return -2; }
        else {
            int buildCode = int.Parse((dbSystem.DBSelectBuilding(buttonName))[0]);
            int buildCount = buildState.get(buildCode);

            return buildCount;
        }
    }
}
