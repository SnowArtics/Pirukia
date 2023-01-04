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

        // 각 UI 창을 닫은 채로 씬을 시작한다.
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(false);

    }

    public void Update() {
        // 각 UI창이 닫히면 내부 변수를 false로 변경 후, 눌러진 버튼의 색을 기본으로 되돌린다.
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
            if (buttonName != "ExitButton") {                   // 닫기 버튼(ExitButton)이면 무시
                button = clickObject.GetComponent<Button>();
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

    // 건설 버튼을 눌렀을 때 실행
    public void ClickedBuild() {
        buildUI.SetActive(true);
        statUI.SetActive(false);
        registerUI.SetActive(false);

        // 기본적으로 마을 건물 UI만 나타나도록 설정
        buildUIEvent.BuildVillage();
    }

    // 통계 버튼을 눌렀을 때 실행
    public void ClickedStat() {
        buildUI.SetActive(false);
        statUI.SetActive(true);
        registerUI.SetActive(false);
    }

    // 신도 등록 버튼을 눌렀을 때 실행
    public void ClickedRegister() {
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(true);
    }

    // 닫기 버튼을 눌렀을 때 실행
    public void CloseUI() {
        buildUI.SetActive(false);
        statUI.SetActive(false);
        registerUI.SetActive(false);
    }
}
