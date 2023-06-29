using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarIcon : MonoBehaviour
                    , IPointerClickHandler
                , IDragHandler
                , IPointerEnterHandler
                , IPointerExitHandler
{
    GameObject text;
    GameObject parentObject;

    FirstGospelWindowControl controlScript;

    void Awake()
    {
        text = transform.GetChild(0).gameObject;
        parentObject = transform.parent.gameObject;

        controlScript = parentObject.GetComponent<FirstGospelWindowControl>();
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
        text.SetActive(true);
        controlScript.SetActiveExplain(true);

        controlScript.SetExplainNameText("전쟁");
        controlScript.SetExplainText("신께서 보우하사 이교도에게 철퇴를!");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.SetActive(false);
        controlScript.SetActiveExplain(false);
    }
}
