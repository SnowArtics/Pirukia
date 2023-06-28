using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IndustryBlessing : MonoBehaviour
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
        windowManagement.ChangeBlessingImage(Resources.Load<Sprite>("MainGameUI/Gospel&BlessingUI/BlessingUI/IndustryBlessing"));
        windowManagement.ChangeBlessingNameText("산업 축복");
        windowManagement.ChangeBlessingExplainText("신께서 축복을 내려 일부 광산에 축복을 내립니다. 이 광산에서 나온 수확물은 더욱 제련하기가 쉬워집니다." +
            " 해당 광물을 사용하는 제련시설의 생산량이 증가합니다.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rightBaseWindow.SetActive(false);
    }
}
