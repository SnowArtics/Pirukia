using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUIEditMode : MonoBehaviour
{
    private bool isEdit;
    private List<string> buildList = new List<string>();

    public void buildingToEdit (List<string> list) {
        buildList = list;
        isEdit = true;
    }

    public void Awake() {
        isEdit = false;
    }

    public void Update() {
        if (isEdit) {
            onEditMode();
            isEdit = false;
            // 추가바람
        }
    }

    public void onEditMode() {
        Debug.Log(buildList[1] + "을(를) 설치한 위치를 선택해주세요.");
    }
}
