using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildUITooltip : MonoBehaviour
{
    private GameObject tooltip, ctxTooltip;
    private GameObject dbManage;
    private BuildUIPreset preset;
    private DatabaseManage database;

    TextMeshProUGUI ctx;
    IDataReader data;
    private string buttonName;

    public void Awake() {
        GameObject canvas = GameObject.Find("Canvas");
        tooltip = (canvas.transform.GetChild(4)).gameObject;
        ctxTooltip = (tooltip.transform.GetChild(0)).gameObject;
        dbManage = GameObject.Find("DatabaseSystem");

        ctx = ctxTooltip.GetComponent<TextMeshProUGUI>();
        GameObject buildUI = canvas.transform.GetChild(1).gameObject;
        preset = buildUI.GetComponent<BuildUIPreset>();
        
        database = dbManage.GetComponent<DatabaseManage>();
        database.DBCreate();

        tooltip.SetActive(false);
    }

    private void Update() {

    }

    public void onTooltip() { 
        buttonName = this.name;
        Debug.Log(buttonName + " is on.");
        tooltip.SetActive(true);

        if (buttonName == "-" || buttonName.StartsWith("Str")) {
            ctx.text = "N/A";
        }
        else {
            // 버튼의 이름을 갖는 BUILDING_CODE를 DB에서 찾아 변수에 저장
            IDataReader codeData = database.ExecuteDB("SELECT * from build where BUILDING_NAME=\"" + buttonName + "\"");
            int buildCode = int.Parse(codeData.GetValue(0).ToString());

            string buildData = preset.Get(buildCode-1);

            ctx.text = buildData;
        }
    }

    public void offTooltip() {
        buttonName = this.name;
        Debug.Log(buttonName + " is out.");
        tooltip.SetActive(false);

        ctx.text = "";
    }
}
