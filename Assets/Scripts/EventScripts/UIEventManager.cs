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

    // 버튼 클릭 이벤트에 대한 함수
    public void ButtonClick() {      
        // 현재 클릭한 게임 오브젝트(버튼)를 저장
        clickObject = EventSystem.current.currentSelectedGameObject;
        string buttonName = "";

        try {
            // 클릭한 버튼의 이름을 buttonName에 저장
            buttonName = clickObject.name;
            if (buttonName != "ExitButton") {
                button = clickObject.GetComponent<Button>();
                Debug.Log(button.name);
            }
        }
        // 버튼을 누르지 않으면 발생하는 null 값 무시
        catch (NullReferenceException) { }

        // 각 클릭한 버튼에 따른 UI 표출
        switch (buttonName) {
            case "BuildButton":         // 건설 버튼 클릭
                ClickedBuild();
                buildUIEvent.Set(true);
                buildUIEvent.OnPressButton();
                break;
            case "StatButton":          // 통계 버튼 클릭
                ClickedStat();
                statUIEvent.Set(true);
                statUIEvent.OnPressButton();
                break;
            case "RegisterButton":      // 신도 등록 버튼 클릭
                ClickedRegister();
                registerUIEvent.Set(true);
                registerUIEvent.OnPressButton();
                break;
            case "ExitButton":          // 나가기 버튼(X) 클릭
                CloseUI();
                break;
        }
    }

    public void ClickedBuild() {
        buildUI.SetActive(true);
        statUI.SetActive(false);
        registerUI.SetActive(false);

        // 기본적으로 마을 건물 UI만 나타나도록 설정
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
