using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUIEventManager : MonoBehaviour
{
    [SerializeField]
    private Button registerButton;

    private bool isPressed;
    public bool Get() { return isPressed; }
    public void Set(bool isPressed) { this.isPressed = isPressed; }

    public void Test() {
        Debug.Log("신도 등록 UI 접근 테스트 =)");

    }

    public void OnPressButton() {
        ColorBlock cb = registerButton.colors;
        if (isPressed) {
            cb.normalColor = Color.gray;
            registerButton.colors = cb;
        }
        else {
            cb.normalColor = Color.white;
            registerButton.colors = cb;
        }
    }
}
