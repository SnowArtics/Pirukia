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

        errorTitle.text = "�ǹ� ���� �ѵ� �ʰ�";
        errorCtx.text = "�ִ� �Ǽ� ���� ������ �ʰ��Ͽ����ϴ�. �Ǽ��� �ǹ��� �ٽ� ������ �ֽʽÿ�.";
    }

    public void CollisionBuildingError() {
        PresetErrorUI();

        errorTitle.text = "�ǹ� �ߺ� �Ǽ�";
        errorCtx.text = "�ش� ��ġ���� �ǹ��� �̹� �Ǽ��Ǿ� �ֽ��ϴ�. �ٽ� �������ֽʽÿ�.";
    }

    public void NotEnoughResourceError() {
        PresetErrorUI();

        errorTitle.text = "���� �ڿ� ����";
        errorCtx.text = "�ڿ��� �����Ͽ� �ǹ��� �Ǽ��� �� �����ϴ�. �Ǽ��� �ǹ��� �ٽ� �������ֽʽÿ�.";
    }
}
