using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildUITooltip : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltipUI;
    [SerializeField]
    private TextMeshProUGUI infoBuild;

    public void ShowTooltip(string info) {
        tooltipUI.SetActive(true);
        infoBuild.text = info;

        for(; ; ) {

        }
    }

    public void HideTooltip(string info) {
        tooltipUI.SetActive(false);
    }
}
