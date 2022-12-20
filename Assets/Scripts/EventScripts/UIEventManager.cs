using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MainUI;
    [SerializeField]
    private GameObject BuildUI;

    public void Start() {
        BuildUI.SetActive(false);
    }
    // ��ư Ŭ�� �̺�Ʈ�� ���� �Լ�
    public void ButtonClick() {
        BuildUI.SetActive(true);
    }
}
