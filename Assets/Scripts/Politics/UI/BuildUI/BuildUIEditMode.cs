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
    private GameObject shackSprite, granarySprite, appleOrchardSprite, altarSprite;
    private GameObject canvas, buildUI, buildingModeUI, toolTip;
    private GameObject tmpBuildingTiles, tmpBuilding;
    private ErrorEventManager errorEventManage;
    private BuildUITileCollision tileCollision;
    private bool isEdit, isMouseLeftClicked;
    private List<string> buildDatabase = new List<string>();
    private List<GameObject[,]> buildingPosList = new List<GameObject[,]>();
    private int sizeX, sizeY;                // �ǹ� ũ��(ĭ)�� ����(x, y)
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
        errorEventManage = GameObject.Find("EventSystem").GetComponent<ErrorEventManager>();
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
            if (tmpBuildingTiles != null && !isMouseLeftClicked) {
                float newPosX = Mathf.Floor(hitPos.x / 2) * 2;
                float newPosZ = Mathf.Floor(hitPos.z / 2) * 2;

                if (sizeX % 2 != 0) { newPosX += 1; }
                if (sizeY % 2 != 0) { newPosZ += 1; }

                tmpBuildingTiles.transform.position = new Vector3(newPosX, tmpBuildingTiles.transform.position.y, newPosZ);
                tmpBuilding.transform.position = new Vector3(newPosX, 0f, newPosZ);

                if (tileCollision.getCollision()) {
                    SpriteRenderer renderer = tmpBuildingTiles.GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1f, 0f, 0f, 0.3f);   // green
                }
                else {
                    SpriteRenderer renderer = tmpBuildingTiles.GetComponent<SpriteRenderer>();
                    renderer.color = new Color(0f, 1f, 0f, 0.3f);   // red
                }

                if (Input.GetMouseButtonDown(0)) {
                    if (tileCollision.getCollision()) {
                        errorEventManage.CollisionBuildingError();
                    }
                    else {
                        StartBuilding();
                        isMouseLeftClicked = false;
                    }
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
        sizeY = int.Parse(tmpSize[1]);
        int cntTiles = sizeX * sizeY;

        if (buildDatabase[1] == "Shack") { tmpBuilding = Instantiate(shackSprite); }
        else if (buildDatabase[1] == "Granary") { tmpBuilding = Instantiate(granarySprite); }
        else if (buildDatabase[1] == "AppleOrchard") { tmpBuilding = Instantiate(appleOrchardSprite); }
        else if (buildDatabase[1] == "Altar") { tmpBuilding = Instantiate(altarSprite); }

        // Ÿ�� �������� �����ϰ� ��ġ�� �����Ѵ�.
        tmpBuildingTiles = Instantiate(editTilePrefab, parentTilePrefab.transform);
        tmpBuildingTiles.name = buildDatabase[1];
        tmpBuildingTiles.transform.localScale = new Vector3(sizeX, sizeY, 1);
        tileCollision = tmpBuildingTiles.GetComponent<BuildUITileCollision>();
    }

    // �ǹ� ��ġ ����
    public void StartBuilding() {
        // buildingModeUI.SetActive(false);
        // parentTilePrefab.SetActive(false);
        // buildingPosList.Add(tmpBuildingTiles);

        // �ǹ� �����͸� �ʱ�ȭ �Ѵ�.
        buildDatabase.Clear();
        tmpBuildingTiles = null;
    }
}
