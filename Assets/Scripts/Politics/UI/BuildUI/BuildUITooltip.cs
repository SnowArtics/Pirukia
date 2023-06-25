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
    private string buttonName;
    private bool isPointerEnter;
    private int typeResource;

    public void Awake() {
        GameObject canvas = GameObject.Find("Canvas");
        tooltip = (canvas.transform.GetChild(6)).gameObject;
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
                string buildSpec = string.Empty;

                // 버튼의 이름을 갖는 id를 DB에서 찾아 변수에 저장
                IDataReader dataReader = database.ExecuteDB("SELECT * from building where Name=\"" + buttonName + "\"");
                while (dataReader.Read()) {
                    int buildId = dataReader.GetInt32(0);
                    string[] buildResourceArr = dataReader.GetString(4).Split(',');
                    int countArr = buildResourceArr.Length;
                    buildSpec = preset.GetSpec(buildId);

                    int nHeight = (countArr * 30) + 80;
                    tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(230, nHeight);
                }
                ctx.text = buildSpec;
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
