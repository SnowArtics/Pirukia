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

        // �� UI â�� ���� ä�� ���� �����Ѵ�.
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(false);

    }

    public void Update() {
        // �� UIâ�� ������ ���� ������ false�� ���� ��, ������ ��ư�� ���� �⺻���� �ǵ�����.
        if(!buildUI.activeSelf) {
            buildUIEvent.Set(false);
            buildUIEvent.OnPressButton();
        }
        if(!statUI.activeSelf) {
            statUIEvent.Set(false);
            statUIEvent.OnPressButton();
        }
        if(!registerUI.activeSelf) {
            registerUIEvent.Set(false);
            registerUIEvent.OnPressButton();
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
            if (buttonName != "ExitButton") {                   // �ݱ� ��ư(ExitButton)�̸� ����
                button = clickObject.GetComponent<Button>();
            }
        }
        // ��ư�� ������ ������ �߻��ϴ� null �� ����
        catch (NullReferenceException) { }

        // �� Ŭ���� ��ư�� ���� UI ǥ��
        switch (buttonName) {
            case "BuildButton":         // �Ǽ� ��ư Ŭ��
                ClickedBuild();
                buildUIEvent.Set(true);
                buildUIEvent.OnPressButton();
                break;
            case "StatButton":          // ��� ��ư Ŭ��
                ClickedStat();
                statUIEvent.Set(true);
                statUIEvent.OnPressButton();
                break;
            case "RegisterButton":      // �ŵ� ��� ��ư Ŭ��
                ClickedRegister();
                registerUIEvent.Set(true);
                registerUIEvent.OnPressButton();
                break;
            case "ExitButton":          // ������ ��ư(X) Ŭ��
                CloseUI();
                break;
        }
    }

    // �Ǽ� ��ư�� ������ �� ����
    public void ClickedBuild() {
        buildUI.SetActive(true);
        statUI.SetActive(false);
        registerUI.SetActive(false);

        // �⺻������ ���� �ǹ� UI�� ��Ÿ������ ����
        buildUIEvent.BuildVillage();
    }

    // ��� ��ư�� ������ �� ����
    public void ClickedStat() {
        buildUI.SetActive(false);
        statUI.SetActive(true);
        registerUI.SetActive(false);
    }

    // �ŵ� ��� ��ư�� ������ �� ����
    public void ClickedRegister() {
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(true);
    }

    // �ݱ� ��ư�� ������ �� ����
    public void CloseUI() {
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(false);
    }
}
