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

    private bool isPressed;
    public bool Get() { return isPressed; }
    public void Set(bool isPressed) { this.isPressed = isPressed; }


    public void Start() {
        isPressed = false;
    }

    public void Update() {
        Debug.Log(isPressed);
    }

    public void Test() {
        Debug.Log("빌드 UI 접근 테스트 =)");
    }

    public void BuildVillage() {
        villageStructure.SetActive(true);
        religionStructure.SetActive(false);
        otherStructure.SetActive(false);
    }

    public void BuildReligion() {
        villageStructure.SetActive(false);
        religionStructure.SetActive(true);
        otherStructure.SetActive(false);
    }

    public void BuildOther() {
        villageStructure.SetActive(false);
        religionStructure.SetActive(false);
        otherStructure.SetActive(true);
    }

    public void OnPressButton() {
        ColorBlock cb = buildButton.colors;
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
