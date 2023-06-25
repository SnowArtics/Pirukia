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
    private int sizeX, sizeZ;                // �ǹ� ũ��(ĭ)�� ����(x, z)
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
        // ���콺 Ŀ���� ��ǥ�� �޾Ƽ� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ���콺 Ŀ�� ��ġ�� ���� �ǹ� ��ġ Ÿ�� ��ġ �̵�
        if (Physics.Raycast(ray, out hit, 100f)) {
            Vector3 hitPos = hit.point;
            hitPos.y = 0.3f;

            // Ÿ���� ĭ ������ �����̵��� ����
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

        // �ǹ� ��ġ ��忡 ���� ��, �Լ� ����
        if (isEdit) {
            OnEditMode();
            isEdit = false;
        }
    }

    // ���� ��忡 ���� �� ����
    public void OnEditMode() {
        buildingModeUI.SetActive(true);
        parentTilePrefab.SetActive(true);
        buildUI.SetActive(false);
        toolTip.SetActive(false);
        Debug.Log(buildDatabase[2] + "��(��) ��ġ�� ��ġ�� �������ּ���.");

        // DB���� �ǹ� ũ�� ���� �޾ƿ� Array�� ����
        string[] tmpSize = buildDatabase[3].Split('*');
        sizeX = int.Parse(tmpSize[0]);
        sizeZ = int.Parse(tmpSize[1]);
        int cntTiles = sizeX * sizeZ;
        tmpBuildingTiles = new GameObject[sizeX, sizeZ];

        // �ǹ� ũ�⸸ŭ Ÿ�� ������ ����, ��ġ ����
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

    // �ǹ� ��ġ ����
    public void StartBuilding() {
        buildingModeUI.SetActive(false);
        parentTilePrefab.SetActive(false);
        buildingPosList.Add(tmpBuildingTiles);

        // �ǹ� �����͸� �ʱ�ȭ �Ѵ�.
        buildDatabase.Clear();
        tmpBuildingTiles = null;
    }
}
