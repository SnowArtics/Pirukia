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

        titleError.text = "�ǹ� ���� �ѵ� �ʰ�";
        ctxError.text = "�Ǽ��� �ǹ��� �ٽ� ������ �ֽʽÿ�.";
    }
}
