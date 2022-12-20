using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MainUI;
    [SerializeField]
    private GameObject BuildUI;

    public void Start() {
        BuildUI.SetActive(false);
    }
    // 버튼 클릭 이벤트에 대한 함수
    public void ButtonClick() {
        BuildUI.SetActive(true);
    }
}
