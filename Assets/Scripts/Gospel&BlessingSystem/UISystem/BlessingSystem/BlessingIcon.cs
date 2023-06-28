using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BlessingIcon : MonoBehaviour
        , IPointerClickHandler
        , IDragHandler
        , IPointerEnterHandler
        , IPointerExitHandler
{
    [SerializeField]
    private GameObject explain;
    [SerializeField]
    private GameObject blessingWindow;

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
        blessingWindow.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        explain.SetActive(true);
        explainText.text = "신앙심을 증가시키고, 신도들에게 축복을 내려주세요.";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        explain.SetActive(false);
    }
}
