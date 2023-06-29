using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LifeIcon : MonoBehaviour
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

        controlScript.SetExplainNameText("삶");
        controlScript.SetExplainText("사람은 삶을 어떻게 해쳐나가야 하는가");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.SetActive(false);
        controlScript.SetActiveExplain(false);
    }
}
