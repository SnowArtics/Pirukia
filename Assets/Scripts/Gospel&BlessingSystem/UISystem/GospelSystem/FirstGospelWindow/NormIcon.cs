using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NormIcon : MonoBehaviour
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

        controlScript.SetExplainNameText("규범");
        controlScript.SetExplainText("사람은 어린양과 같아 마땅히 규범으로서 이끌어 주어야 한다.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.SetActive(false);
        controlScript.SetActiveExplain(false);
    }
}
