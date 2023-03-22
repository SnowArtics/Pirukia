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
            // �߰��ٶ�
        }
    }

    public void onEditMode() {
        Debug.Log(buildList[1] + "��(��) ��ġ�� ��ġ�� �������ּ���.");
    }
}
