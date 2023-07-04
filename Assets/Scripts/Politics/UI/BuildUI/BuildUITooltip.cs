using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildUITooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject tooltip, dbSystem;
    private BuildUIPreset preset;
    private DatabaseManage database;

    private TextMeshProUGUI ctx, woodplanks, stones;
    private string buttonName;
    private bool isPointerEnter;
    private int needWoodPlanks, needStones;
    private Dictionary<int, int> buildResourceDict = new Dictionary<int, int>();

    public void Awake() {
        GameObject mainUISystem = GameObject.Find("MainUISystem");
        dbSystem = GameObject.Find("DatabaseSystem");
        tooltip = mainUISystem.transform.GetChild(11).gameObject;

        woodplanks = tooltip.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        stones = tooltip.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();

        GameObject buildUIEventMng = GameObject.Find("BuildUIEventSystem");
        preset = buildUIEventMng.GetComponent<BuildUIPreset>();
        
        database = dbSystem.GetComponent<DatabaseManage>();
        database.DBCreate();
    }

    private void Update() {
        if (isPointerEnter) {
            buttonName = this.name;
            string buildSpec = string.Empty;

            // 버튼의 이름을 갖는 id를 DB에서 찾아 변수에 저장
            IDataReader dataReader = database.ExecuteDB("SELECT * from building where Name=\"" + buttonName + "\"");
            while (dataReader.Read()) {
                int buildId = dataReader.GetInt32(0);
                string[] buildResource = dataReader.GetString(3).Split(',');
                string[] buildResourceAmount = dataReader.GetString(4).Split(',');

                for (int i = 0; i < buildResource.Length; i++) {
                    int tmpBR = int.Parse(buildResource[i]);
                    int tmpBRA = int.Parse(buildResourceAmount[i]);

                    buildResourceDict[tmpBR] = tmpBRA;
                }

                needWoodPlanks = buildResourceDict[101];
                needStones = buildResourceDict[102];
                buildSpec = preset.GetSpec(buildId);

                woodplanks.text = needWoodPlanks.ToString();
                stones.text = needStones.ToString();

                int nHeight = (2 * 30) + 80;
                tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(230, nHeight);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.SetActive(true);
        isPointerEnter = true;

        Debug.Log("Tooltip On!!!!!");
    }

    public void OnPointerExit(PointerEventData eventData) {
        isPointerEnter = false;
        tooltip.SetActive(false);
        Debug.Log("Tooltip Off!!!!!");
    }
}
