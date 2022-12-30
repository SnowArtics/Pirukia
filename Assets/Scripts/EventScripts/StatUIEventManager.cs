using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUIEventManager : MonoBehaviour
{
    private bool isPressed;
    public bool Get() { return isPressed; }
    public void Set(bool isPressed) { this.isPressed = isPressed; }

    public void Test() {
        Debug.Log("통계 UI 접근 테스트 =)");

    }


}