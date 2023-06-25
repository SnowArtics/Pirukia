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
    private GameObject allowTilePrefab, deniedTilePrefab, parentTilePrefab;
    private GameObject canvas, buildUI, buildingModeUI, toolTip;
    private GameObject[,] tmpBuildingTiles;
    private bool isEdit, isMouseLeftClicked;
    private List<string> buildDatabase = new List<string>();
    private List<GameObject[,]> buildingPosList = new List<GameObject[,]>();
    private int sizeX, sizeZ;                // 건물 크기(칸)을 저장(x, z)
    private Vector3 pos, mousePos;
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
                for(int i = 0; i < sizeX; i++) {
                    for( int j = 0; j < sizeZ; j++) {
                        tmpBuildingTiles[i, j].transform.position = new Vector3((Mathf.Floor(hitPos.x / 2) * 2 + 1) + (i * 2),
                                                                             tmpBuildingTiles[i,j].transform.position.y,
                                                                             (Mathf.Floor(hitPos.z / 2) * 2 + 1) + (j * 2));
                    }
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
        sizeZ = int.Parse(tmpSize[1]);
        int cntTiles = sizeX * sizeZ;
        tmpBuildingTiles = new GameObject[sizeX, sizeZ];

        // 건물 크기만큼 타일 프리팹 생성, 위치 조정
        for(int i = 0; i < sizeX; i++) {
            for(int j = 0; j < sizeZ; j++) {
                tmpBuildingTiles[i, j] = Instantiate(allowTilePrefab);
                tmpBuildingTiles[i, j].transform.SetParent(parentTilePrefab.transform, false);
                tmpBuildingTiles[i, j].transform.position = new Vector3(tmpBuildingTiles[i, j].transform.position.x + (i*2),
                                                                     tmpBuildingTiles[i, j].transform.position.y,
                                                                     tmpBuildingTiles[i, j].transform.position.z + (j*2));
            }
        }

        
    }

    // 건물 설치 실행
    public void StartBuilding() {
        buildingModeUI.SetActive(false);
        parentTilePrefab.SetActive(false);
        buildingPosList.Add(tmpBuildingTiles);

        // 건물 데이터를 초기화 한다.
        buildDatabase.Clear();
        tmpBuildingTiles = null;
    }
}
