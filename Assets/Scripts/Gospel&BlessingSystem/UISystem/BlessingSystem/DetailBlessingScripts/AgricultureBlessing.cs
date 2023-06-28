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
        windowManagement.ChangeBlessingNameText("농업 축복");
        windowManagement.ChangeBlessingExplainText("신께서 축복을 내려 일부 농경지의 토양에 축복을 내립니다. 이 토양에서 나온 수확을 먹은 신도들은 자연스레 신앙심이 깊어질 것입니다." +
            " 농업의 생산량이 늘어나며, 해당 농작물을 먹은 신도들의 신앙심이 증가합니다.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rightBaseWindow.SetActive(false);
    }
}
