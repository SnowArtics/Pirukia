using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgricultureBlessing : MonoBehaviour
            , IPointerClickHandler
            , IDragHandler
            , IPointerEnterHandler
            , IPointerExitHandler
{
    [SerializeField]
    private GameObject rightBaseWindow;

    private RightBaseWindowManage windowManagement;

    void Awake()
    {
        windowManagement = rightBaseWindow.GetComponent<RightBaseWindowManage>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rightBaseWindow.SetActive(true);
        windowManagement.ChangeBlessingImage(Resources.Load<Sprite>("MainGameUI/Gospel&BlessingUI/BlessingUI/AgricultureBlessing"));
        windowManagement.ChangeBlessingNameText("��� �ູ");
        windowManagement.ChangeBlessingExplainText("�Ų��� �ູ�� ���� �Ϻ� ������� ��翡 �ູ�� �����ϴ�. �� ��翡�� ���� ��Ȯ�� ���� �ŵ����� �ڿ����� �žӽ��� ����� ���Դϴ�." +
            " ����� ���귮�� �þ��, �ش� ���۹��� ���� �ŵ����� �žӽ��� �����մϴ�.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rightBaseWindow.SetActive(false);
    }
}
