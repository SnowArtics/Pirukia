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
        Debug.Log("Click");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        explain.SetActive(true);
        explainText.text = "������ �����, �ŵ����� �ùٸ� �������� �̲����ּ���.";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        explain.SetActive(false);
    }
}
