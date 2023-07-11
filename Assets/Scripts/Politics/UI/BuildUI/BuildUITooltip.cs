using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildUITooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject tooltip, dbSystem;
    private BuildingState buildingState;
    private BuildUIPreset preset;
    private DatabaseManage database;

    private TextMeshProUGUI ctx, woodplanksText, stonesText, buildingText;
    private string buttonName;
    private bool isPointerEnter;
    private int buildId, needWoodPlanks, needStones, limitBuildings;
    private Dictionary<int, int> buildResourceDict;

    public void Awake() {
        GameObject mainUISystem = GameObject.Find("MainUISystem");
        dbSystem = GameObject.Find("DatabaseSystem");
        tooltip = mainUISystem.transform.GetChild(11).gameObject;
        buildResourceDict = new Dictionary<int, int>();
        buildingState = GameObject.Find("BuildUIEventSystem").GetComponent<BuildingState>();

        buildResourceDict.Add(101, 0);
        buildResourceDict.Add(102, 0);

        woodplanksText = tooltip.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        stonesText = tooltip.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        buildingText = tooltip.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>();

        GameObject buildUIEventMng = GameObject.Find("BuildUIEventSystem");
        preset = buildUIEventMng.GetComponent<BuildUIPreset>();
        
        database = dbSystem.GetComponent<DatabaseManage>();
        database.DBCreate();
    }

    private void Update() {
        if (isPointerEnter) {
            buttonName = transform.parent.name;
            string buildSpec = string.Empty;

            // 버튼의 이름을 갖는 id를 DB에서 찾아 변수에 저장
            IDataReader dataReader = database.ExecuteDB("SELECT * from building where Name=\"" + buttonName + "\"");
            while (dataReader.Read()) {
                buildId = dataReader.GetInt32(0);
                string[] buildResource = dataReader.GetString(4).Split(',');
                string[] buildResourceAmount = dataReader.GetString(5).Split(',');
                limitBuildings = dataReader.GetInt32(12);

                for (int i = 0; i < buildResource.Length; i++) {
                    int tmpBR = int.Parse(buildResource[i]);
                    int tmpBRA = int.Parse(buildResourceAmount[i]);

                    buildResourceDict[tmpBR] = tmpBRA;
                }

                needWoodPlanks = buildResourceDict[101];
                needStones = buildResourceDict[102];
                //buildSpec = preset.GetSpec(buildId);

                woodplanksText.text = needWoodPlanks.ToString();
                stonesText.text = needStones.ToString();
                buildingText.text = buildingState.GetBuilding(buildId) + "/" + limitBuildings.ToString();

                // int nHeight = (2 * 30) + 80;
                //tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(230, nHeight);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.SetActive(true);
        isPointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        isPointerEnter = false;
        tooltip.SetActive(false);
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   