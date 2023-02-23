using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorEventManager : MonoBehaviour
{
    [SerializeField]
    public GameObject errorUI;

    private TextMeshProUGUI titleError;
    private TextMeshProUGUI ctxError;

    private void Awake() {
        errorUI.SetActive(false);
    }

    public void PresetErrorUI() {
        titleError = ((errorUI.transform.GetChild(0)).gameObject).GetComponent<TextMeshProUGUI>();
        ctxError = ((errorUI.transform.GetChild(1)).gameObject).GetComponent<TextMeshProUGUI>();

        errorUI.SetActive(true);
    }

    public void ExceedLimitError() {
        PresetErrorUI();

        titleError.text = "건물 개수 한도 초과";
        ctxError.text = "건설할 건물을 다시 지정해 주십시오.";
    }
}
