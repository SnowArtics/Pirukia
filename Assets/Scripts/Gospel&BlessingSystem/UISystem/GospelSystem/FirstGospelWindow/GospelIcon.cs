using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GospelIcon : MonoBehaviour
            , IPointerClickHandler
            , IDragHandler
            , IPointerEnterHandler
            , IPointerExitHandler
{
    [SerializeField]
    private GameObject explain;
    [SerializeField]
    private GameObject gospelWindow;

    private GameObject parentObject;

    private TextMeshProUGUI explainText;

    private void Awake()
    {
        parentObject = transform.parent.gameObject;
        explainText = explain.GetComponent<TextMeshProUGUI>();
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
        parentObject.SetActive(false);
        gospelWindow.SetActive(true);
        Debug.Log("Click");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        explain.SetActive(true);
        explainText.text = "교리를 세우고, 신도들을 올바른 방향으로 이끌어주세요.";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        explain.SetActive(false);
    }
}
