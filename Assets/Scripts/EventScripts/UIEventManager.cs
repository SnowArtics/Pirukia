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
    private Button button;

    private GameObject clickObject;
    private BuildUIEventManager buildUIEvent;
    private StatUIEventManager statUIEvent;
    private RegisterUIEventManager registerUIEvent;

    public void Start() {
        buildUIEvent = buildUI.GetComponent<BuildUIEventManager>();
        statUIEvent = statUI.GetComponent<StatUIEventManager>();
        registerUIEvent = registerUI.GetComponent<RegisterUIEventManager>();

        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(false);

    }

    public void Update() {
        if(!buildUI.activeSelf) {
            buildUIEvent.Set(false);
//            buildUIEvent.OnPressButton(button);
        }
    }

    // ��ư Ŭ�� �̺�Ʈ�� ���� �Լ�
    public void ButtonClick() {      
        // ���� Ŭ���� ���� ������Ʈ(��ư)�� ����
        clickObject = EventSystem.current.currentSelectedGameObject;
        string buttonName = "";

        try {
            // Ŭ���� ��ư�� �̸��� buttonName�� ����
            buttonName = clickObject.name;
            button = clickObject.GetComponent<Button>();
            Debug.Log(buttonName);
        }
        // ��ư�� ������ ������ �߻��ϴ� null �� ����
        catch (NullReferenceException) { }

        // �� Ŭ���� ��ư�� ���� UI ǥ��
        switch (buttonName) {
            case "BuildButton":         // �Ǽ� ��ư Ŭ��
                ClickedBuild();
                buildUIEvent.Set(true);
                buildUIEvent.OnPressButton(button);
                break;
            case "StatButton":          // ��� ��ư Ŭ��
                ClickedStat();
                break;
            case "RegisterButton":      // �ŵ� ��� ��ư Ŭ��
                ClickedRegister();
                break;
            case "ExitButton":          // ������ ��ư(X) Ŭ��
                CloseUI();
                break;
        }
    }

    public void ClickedBuild() {
        buildUI.SetActive(true);
        statUI.SetActive(false);
        registerUI.SetActive(false);

        // �⺻������ ���� �ǹ� UI�� ��Ÿ������ ����
        buildUIEvent.BuildVillage();
    }

    public void ClickedStat() {
        buildUI.SetActive(false);
        statUI.SetActive(true);
        registerUI.SetActive(false);
    }

    public void ClickedRegister() {
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(true);
    }

    public void CloseUI() {
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(false);
    }
}
