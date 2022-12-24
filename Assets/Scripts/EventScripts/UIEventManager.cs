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

    // 버튼 클릭 이벤트에 대한 함수
    public void ButtonClick() {
        // 현재 클릭한 게임 오브젝트(버튼)를 저장
        clickObject = EventSystem.current.currentSelectedGameObject;
        string buttonName = "";

        // 버튼을 누르지 않으면 발생하는 null 값 무시
        try {
            buttonName = clickObject.name;              // 클릭한 버튼의 이름을 buttonName에 저장
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
