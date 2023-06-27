using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodStorageScript : MonoBehaviour
            , IPointerClickHandler
            , IDragHandler
            , IPointerEnterHandler
            , IPointerExitHandler
{
    [SerializeField]
    private GameObject apple;

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
        apple.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        apple.SetActive(false);
    }
}
