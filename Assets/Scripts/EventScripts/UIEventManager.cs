using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIEventManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainUI, buildUI, statUI, registerUI;

    private GameObject clickObject;
    private BuildUIEventManager buildUIEvent = new BuildUIEventManager();
    private StatUIEventManager statUIEvent = new StatUIEventManager();
    private RegisterUIEventManager registerUIEvent = new RegisterUIEventManager();

    public void Start() {
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(false);

    }

    public void Update() {

    }

    // ��ư Ŭ�� �̺�Ʈ�� ���� �Լ�
    public void ButtonClick() {
        // ���� Ŭ���� ���� ������Ʈ(��ư)�� ����
        clickObject = EventSystem.current.currentSelectedGameObject;
        string buttonName = "";

        // ��ư�� ������ ������ �߻��ϴ� null �� ����
        try {
            buttonName = clickObject.name;              // Ŭ���� ��ư�� �̸��� buttonName�� ����
            Debug.Log(buttonName);
        }
        catch (NullReferenceException) { }

        switch (buttonName) {
            case "BuildButton":
                ClickedBuild();
                break;
            case "StatButton":
                ClickedStat();
                break;
            case "RegisterButton":
                ClickedRegister();
                break;
            case "ExitButton":
                CloseUI();
                break;
        }
    }

    public void ClickedBuild() {
        buildUI.SetActive(true);
        statUI.SetActive(false);
        registerUI.SetActive(false);

        buildUIEvent.Test();
    }

    public void ClickedStat() {
        buildUI.SetActive(false);
        statUI.SetActive(true);
        registerUI.SetActive(false);

        statUIEvent.Test();
    }

    public void ClickedRegister() {
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(true);

        registerUIEvent.Test();
    }

    public void CloseUI() {
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(false);
    }
}
