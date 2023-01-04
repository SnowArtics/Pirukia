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

    
    private bool isPressed;                 // ��ư�� ������ �������� ����
    public bool Get() { return isPressed; }
    public void Set(bool isPressed) { this.isPressed = isPressed; }


    public void Start() {
        isPressed = false;
    }

    // ���� �ǹ� ��ư�� ������ �� ����
    public void BuildVillage() {
        villageStructure.SetActive(true);
        religionStructure.SetActive(false);
        otherStructure.SetActive(false);
    }

    // �ž� �ǹ� ��ư�� ������ �� ����
    public void BuildReligion() {
        villageStructure.SetActive(false);
        religionStructure.SetActive(true);
        otherStructure.SetActive(false);
    }

    // ��Ÿ �ǹ� ��ư�� ������ �� ����
    public void BuildOther() {
        villageStructure.SetActive(false);
        religionStructure.SetActive(false);
        otherStructure.SetActive(true);
    }

    // ��ư�� ���� ��ȭ�� �߻����� �� ����(��ư ���� ����)
    public void OnPressButton() {
        ColorBlock cb = buildButton.colors;

        // ��ư�� �������� ���� ������ ���� �� �ֵ��� ������ ����
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
