using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUIEventManager : MonoBehaviour
{
    [SerializeField]
    private Button statButton;

    private bool isPressed;
    public bool Get() { return isPressed; }
    public void Set(bool isPressed) { this.isPressed = isPressed; }

    public void Test() {
        Debug.Log("통계 UI 접근 테스트 =)");

    }

    public void OnPressButton() {
        ColorBlock cb = statButton.colors;
        if (isPressed) {
            cb.normalColor = Color.gray;
            statButton.colors = cb;
        }
        else {
            cb.normalColor = Color.white;
            statButton.colors = cb;
        }
    }
}