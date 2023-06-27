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
    private GameObject canvas, buildUI, buildingModeUI, toolTip;
    // private GameObject[,] tmpBuildingTiles;
    private GameObject tmpBuildingTiles;
    private BuildUITileCollision tileCollision;
    private bool isEdit, isMouseLeftClicked;
    private List<string> buildDatabase = new List<string>();
    private List<GameObject[,]> buildingPosList = new List<GameObject[,]>();
    private int sizeX, sizeY;                // 건물 크기(칸)을 저장(x, y)
    private Vector3 pos, mousePos;

    public Tuple<int, int> getSizeBuilding() { return new Tuple<int, int>(sizeX, sizeY);  }
    public void buildingToEdit (List<string> list) {
        buildDatabase = list;
        isEdit = true;
    }

    public void Start() {
        
    }

    public void Awake() {
        canvas = GameObject.Find("Canvas");
        buildingModeUI = canvas.transform.GetChild(0).gameObject;
        buildUI = canvas.transform.GetChild(2).gameObject;
        toolTip = canvas.transform.GetChild(6).gameObject;
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
            if(tmpBuildingTiles != null && !isMouseLeftClicked) {
                float newPosX = Mathf.Floor(hitPos.x / 2) * 2;
                float newPosZ = Mathf.Floor(hitPos.z / 2) * 2;

                if(sizeX % 2 != 0) { newPosX += 1; }
                if(sizeY % 2 != 0) { newPosZ += 1; }

                tmpBuildingTiles.transform.position = new Vector3(newPosX, tmpBuildingTiles.transform.position.y, newPosZ);

                if(tileCollision.getCollision()) {
                    SpriteRenderer renderer = tmpBuildingTiles.GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1f, 0f, 0f, 0.3f);   // green
                } else {
                    SpriteRenderer renderer = tmpBuildingTiles.GetComponent<SpriteRenderer>();
                    renderer.color = new Color(0f, 1f, 0f, 0.3f);   // red
                }

                if (Input.GetMouseButtonDown(0)) {
                    StartBuilding();
                    isMouseLeftClicked = false;
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
        tmpBuildingTiles = Instantiate(editTilePrefab, parentTilePrefab.transform);
        tmpBuildingTiles.name = buildDatabase[2];
        tmpBuildingTiles.transform.localScale = new Vector3(sizeX, sizeY, 1);
        tileCollision = tmpBuildingTiles.GetComponent<BuildUITileCollision>();
    }

    // 건물 설치 실행
    public void StartBuilding() {
        buildingModeUI.SetActive(false);
        parentTilePrefab.SetActive(false);
        // buildingPosList.Add(tmpBuildingTiles);

        // 건물 데이터를 초기화 한다.
        buildDatabase.Clear();
        tmpBuildingTiles = null;
    }
}
