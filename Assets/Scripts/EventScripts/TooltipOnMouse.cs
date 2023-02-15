using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipOnMouse : MonoBehaviour
{
    [SerializeField]
    private GameObject Tooltip;
    private RectTransform transformTooltip;

    public void Start() {
        transformTooltip = Tooltip.GetComponent<RectTransform>();
    }

    public void Update() {
        Vector2 mousePos = Input.mousePosition;
        float wTooltip = transformTooltip.rect.width;
        float hTooltip = transformTooltip.rect.height;
        transformTooltip.position = mousePos;
    }
}
