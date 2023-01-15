using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberUIEventManager : MonoBehaviour
{
    [SerializeField]
    private Button memberButton;

    private bool isPressed;
    public bool Get() { return isPressed; }
    public void Set(bool isPressed) { this.isPressed = isPressed; }

    public void OnPressButton() {
        ColorBlock cb = memberButton.colors;
        if (isPressed) {
            cb.normalColor = Color.gray;
            memberButton.colors = cb;
        }
        else {
            cb.normalColor = Color.white;
            memberButton.colors = cb;
        }
    }
}
