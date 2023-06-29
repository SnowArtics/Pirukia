using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorEventManager : MonoBehaviour
{
    [SerializeField]
    public GameObject errorUI;

    private TextMeshProUGUI errorTitle;
    private TextMeshProUGUI errorCtx;

    private void Awake() {
        errorUI.SetActive(false);
    }

    public void PresetErrorUI() {
        errorTitle = ((errorUI.transform.GetChild(0)).gameObject).GetComponent<TextMeshProUGUI>();
        errorCtx = ((errorUI.transform.GetChild(1)).gameObject).GetComponent<TextMeshProUGUI>();

        errorUI.SetActive(true);
    }

    public void ExceedLimitError() {
        PresetErrorUI();

        errorTitle.text = "건물 개수 한도 초과";
        errorCtx.text = "최대 건설 가능 개수를 초과하였습니다. 건설할 건물을 다시 지정해 주십시오.";
    }

    public void CollisionBuildingError() {
        PresetErrorUI();

        errorTitle.text = "건물 중복 건설";
        errorCtx.text = "해당 위치에는 건물이 이미 건설되어 있습니다. 다시 선택해주십시오.";
    }

    public void NotEnoughResourceError() {
        PresetErrorUI();

        errorTitle.text = "보유 자원 부족";
        errorCtx.text = "자원이 부족하여 건물을 건설할 수 없습니다. 건설할 건물을 다시 선택해주십시오.";
    }
}
