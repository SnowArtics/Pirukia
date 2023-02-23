using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildUITooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject tooltip, ctxTooltip;
    private GameObject dbManage;
    private BuildUIPreset preset;
    private DatabaseManage database;

    private TextMeshProUGUI ctx;
    private IDataReader data;
    private string buttonName;
    private bool isPointerEnter;

    public void Awake() {
        GameObject canvas = GameObject.Find("Canvas");
        tooltip = (canvas.transform.GetChild(5)).gameObject;
        ctxTooltip = (tooltip.transform.GetChild(0)).gameObject;
        dbManage = GameObject.Find("DatabaseSystem");

        ctx = ctxTooltip.GetComponent<TextMeshProUGUI>();
        GameObject buildUIEventMng = GameObject.Find("BuildUIEventSystem");
        preset = buildUIEventMng.GetComponent<BuildUIPreset>();
        
        database = dbManage.GetComponent<DatabaseManage>();
        database.DBCreate();

        tooltip.SetActive(false);
    }

    private void Update() {
        if (isPointerEnter) {
            buttonName = this.name;

            if (buttonName == "-" || buttonName.StartsWith("Str")) {
                ctx.text = "N/A";
            }
            else {
                tooltip.SetActive(true);
                // ��ư�� �̸��� ���� BUILDING_CODE�� DB���� ã�� ������ ����
                IDataReader codeData = database.ExecuteDB("SELECT * from build where BUILDING_NAME=\"" + buttonName + "\"");
                int buildCode = int.Parse(codeData.GetValue(0).ToString());

                string buildData = preset.Get(buildCode - 1);

                ctx.text = buildData;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) { 
        isPointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        isPointerEnter = false;

        buttonName = this.name;
        tooltip.SetActive(false);

        ctx.text = "";
    }
}
