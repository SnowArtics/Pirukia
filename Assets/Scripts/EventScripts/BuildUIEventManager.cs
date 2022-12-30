using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildUIEventManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buildUI;
    [SerializeField]
    private GameObject villageStructure;
    [SerializeField]
    private GameObject religionStructure;
    [SerializeField]
    private GameObject otherStructure;

    private bool isPressed;
    public bool Get() { return isPressed; }
    public void Set(bool isPressed) { this.isPressed = isPressed; }


    public void Start() {
        isPressed = false;
    }

    public void Update() {
        
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

    public void OnPressButton(Button button) {
        ColorBlock cb = button.colors;
        if(isPressed) {
            cb.normalColor = Color.gray;
            button.colors = cb;
        }
        else {
            cb.normalColor = Color.white;
            button.colors = cb;
        }
    }
}
