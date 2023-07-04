using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using static UnityEditor.Progress;

public class BuildUIEditMode : MonoBehaviour
{
    [SerializeField]
    private GameObject editTilePrefab, parentTilePrefab;
    [SerializeField]
    private GameObject shackSprite, granarySprite, appleOrchardSprite, altarSprite, buildingDust;
    [SerializeField]
    private GameObject canvas, buildUI, buildingModeUI, toolTip;
    private GameObject tmpBuildingTiles, tmpBuilding, tmpDust;
    private ResourceManagement resourceManagement;
    private ErrorEventManager errorEventManage;
    private BuildUITileCollision tileCollision;
    private bool isEdit, isMouseLeftClicked;
    private List<string> buildDatabase = new List<string>();
    private List<GameObject> buildingPosList = new List<GameObject>();
    private int sizeX, sizeY;                // 건물 크기(칸)을 저장(x, y)
    private Vector3 pos, mousePos;

    public Tuple<int, int> GetSizeBuilding() { return new Tuple<int, int>(sizeX, sizeY); }
    public void BuildingToEdit (List<string> list) {
        buildDatabase = list;
        isEdit = true;
    }

    public void Awake() {
        canvas = GameObject.Find("MainUISystem");
        resourceManagement = GameObject.Find("ResourceSystem").GetComponent<ResourceManagement>();
        errorEventManage = GameObject.Find("EventSystem").GetComponent<ErrorEventManager>();
        isEdit = false;
        isMouseLeftClicked = false;
    }

    public void Update() {
        // 마우스 커서의 좌표를 받아서 저장
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 마우스 커서 위치에 따라 건물 설치 타일 위치 이동
        if (Physics.Raycast(ray, out hit, 100f)) {
            Vector3 hitPos = hit.point;
            hitPos.y = 0.3f;

            // 타일을 칸 단위로 움직이도록 설정
            if (tmpBuildingTiles != null && !isMouseLeftClicked) {
                float newPosX = Mathf.Floor(hitPos.x / 2) * 2;
                float newPosZ = Mathf.Floor(hitPos.z / 2) * 2;

                if (sizeX % 2 != 0) { newPosX += 1; }
                if (sizeY % 2 != 0) { newPosZ += 1; }

                tmpBuildingTiles.transform.position = new Vector3(newPosX, tmpBuildingTiles.transform.position.y, newPosZ);
                tmpBuilding.transform.position = new Vector3(newPosX, 0f, newPosZ);

                if (tileCollision.GetCollision()) {
                    SpriteRenderer renderer = tmpBuildingTiles.GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1f, 0f, 0f, 0.3f);   // green
                }
                else {
                    SpriteRenderer renderer = tmpBuildingTiles.GetComponent<SpriteRenderer>();
                    renderer.color = new Color(0f, 1f, 0f, 0.3f);   // red
                }

                if (Input.GetMouseButtonDown(0)) {
                    if (tileCollision.GetCollision()) {
                        errorEventManage.CollisionBuildingError();
                    }
                    else {
                        StartBuilding(newPosX, newPosZ);
                        isMouseLeftClicked = false;
                    }
                }
            }
        }

        // 건물 설치 모드에 진입 시, 함수 실행
        if (isEdit) {
            OnEditMode();
            isEdit = false;
        }
    }

    // 편집 모드에 들어갔을 때 수행
    public void OnEditMode() {
        buildingModeUI.SetActive(true);
        parentTilePrefab.SetActive(true);
        buildUI.SetActive(false);
        toolTip.SetActive(false);
        Debug.Log(buildDatabase[2] + "을(를) 설치할 위치를 선택해주세요.");

        // DB에서 건물 크기 값을 받아와 Array에 저장
        string[] tmpSize = buildDatabase[3].Split('*');
        sizeX = int.Parse(tmpSize[0]);
        sizeY = int.Parse(tmpSize[1]);
        int cntTiles = sizeX * sizeY;

        if (buildDatabase[1] == "Shack") { tmpBuilding = Instantiate(shackSprite); }
        else if (buildDatabase[1] == "Granary") { tmpBuilding = Instantiate(granarySprite); }
        else if (buildDatabase[1] == "AppleOrchard") { tmpBuilding = Instantiate(appleOrchardSprite); }
        else if (buildDatabase[1] == "Altar") { tmpBuilding = Instantiate(altarSprite); }

        // 타일 프리팹을 생성하고 위치를 조정한다.
        tmpBuildingTiles = Instantiate(editTilePrefab, parentTilePrefab.transform);
        tmpBuildingTiles.name = buildDatabase[1];
        tmpBuildingTiles.transform.localScale = new Vector3(sizeX, sizeY, 1);
        tileCollision = tmpBuildingTiles.GetComponent<BuildUITileCollision>();
    }

    // 건물 설치 실행
    public void StartBuilding(float posX, float posZ) {
        buildingPosList.Add(tmpBuilding);
        /* 건물 건설 중일 때 구름 스프라이트로 가린다. */
        tmpDust = Instantiate(buildingDust);
        tmpDust.transform.position = new Vector3(posX-1, 0f, posZ-1);
        StartCoroutine(CompleteBuild());

        /* 건물 자원 체크 후 차감 */
        CheckResource();

        // 건물 데이터를 초기화 한다.
        parentTilePrefab.SetActive(false);
        buildDatabase.Clear();
        tmpBuildingTiles = null;
        buildingModeUI.SetActive(false);
        buildUI.SetActive(true);
    }

    public void CheckResource() {
        Dictionary<int, float> storeResource = new Dictionary<int, float>();        // 현재 생산된 자원들의 정보
        string[] needResource = buildDatabase[4].Split(',');                        // 건물 건설에 필요한 자원 종류
        string[] needResourceAmount = buildDatabase[5].Split(',');                  // 건물 건설에 필요한 자원 양
        string[] produceResource = buildDatabase[7].Split(',');                     // 건물 건설 시, 생산되는 자원 종류
        string[] produceResourceAmount = buildDatabase[8].Split(",");               // 건물 건설 시, 생산되는 자원 양

        int idxNeed, idxProduce;
        float amountNeed, amountProduce;

        for(int i = 0; i < needResource.Length; i++) {
            idxNeed = int.Parse(needResource[i]);
            amountNeed = float.Parse(needResourceAmount[i]);

            storeResource[idxNeed] = resourceManagement.GetResourceNum(idxNeed);

            if (storeResource[idxNeed] < amountNeed) {
                errorEventManage.NotEnoughResourceError();
            } else {
                resourceManagement.SetResourceNum(idxNeed, -amountNeed);
            }
        }

    }

    /* 건물 건설 시간이 지나면 건설중 스프라이트 삭제 */
    public IEnumerator CompleteBuild() {
        float buildTime = float.Parse(buildDatabase[6]);
        Debug.Log(buildTime);

        yield return new WaitForSecondsRealtime(buildTime);
        Debug.Log(buildTime.ToString() + "초 경과");
        tmpDust.SetActive(false);
    }
}
