using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUIEditMode : MonoBehaviour
{
    private GameObject canvas, buildUI, buildingModeUI, toolTip;
    private bool isEdit;
    private List<string> buildList = new List<string>();

    public void buildingToEdit (List<string> list) {
        buildList = list;
        isEdit = true;
    }

    public void Awake() {
        canvas = GameObject.Find("Canvas");
        buildingModeUI = canvas.transform.GetChild(0).gameObject;
        buildUI = canvas.transform.GetChild(2).gameObject;
        toolTip = canvas.transform.GetChild(6).gameObject;
        isEdit = false;
    }

    public void Update() {
        if (isEdit) {
            OnEditMode();
            isEdit = false;
            // 추가바람
        }
    }

    public void OnEditMode() {
        buildingModeUI.SetActive(true);
        buildUI.SetActive(false);
        toolTip.SetActive(false);
        Debug.Log(buildList[2] + "을(를) 설치할 위치를 선택해주세요.");

    }
}
