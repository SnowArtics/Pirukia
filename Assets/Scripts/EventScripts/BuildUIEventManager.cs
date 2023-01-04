using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildUIEventManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buildUI, villageStructure, religionStructure, otherStructure;
    [SerializeField]
    private Button buildButton;

    
    private bool isPressed;                 // 버튼이 눌러진 상태인지 저장
    public bool Get() { return isPressed; }
    public void Set(bool isPressed) { this.isPressed = isPressed; }


    public void Start() {
        isPressed = false;
    }

    // 마을 건물 버튼을 눌렀을 때 실행
    public void BuildVillage() {
        villageStructure.SetActive(true);
        religionStructure.SetActive(false);
        otherStructure.SetActive(false);
    }

    // 신앙 건물 버튼을 눌렀을 때 실행
    public void BuildReligion() {
        villageStructure.SetActive(false);
        religionStructure.SetActive(true);
        otherStructure.SetActive(false);
    }

    // 기타 건물 버튼을 눌렀을 때 실행
    public void BuildOther() {
        villageStructure.SetActive(false);
        religionStructure.SetActive(false);
        otherStructure.SetActive(true);
    }

    // 버튼의 상태 변화가 발생했을 때 실행(버튼 색을 변경)
    public void OnPressButton() {
        ColorBlock cb = buildButton.colors;

        // 버튼이 눌러지면 진한 색으로 보일 수 있도록 색상을 변경
        if(isPressed) {
            cb.normalColor = Color.gray;
            buildButton.colors = cb;
        }
        else {
            cb.normalColor = Color.white;
            buildButton.colors = cb;
        }
    }
}
