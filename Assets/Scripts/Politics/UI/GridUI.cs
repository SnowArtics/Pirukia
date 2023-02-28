using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUI : MonoBehaviour
{
    private float startPosX = 0;                // 그리드 시작하는 x 좌표
    private float startPosZ = 0;                // 그리드 시작하는 z 좌표
    private float gridSize = 2;                 // 그리드 한칸의 크기
    private int countRow = 50;                  // 그리드 가로 칸 개수
    private int countCol = 50;                  // 그리드 세로 칸 개수 

    private LineRenderer lineRen;

    public void Awake() {
        lineRen = this.GetComponent<LineRenderer>();
        InitLineRenderer(lineRen);

        MakeGrid(lineRen, startPosX, startPosZ, countRow, countCol);
    }

//    public void OnValidate() {
//        if((countRow + countCol) > 0) {
//            MakeGrid(lineRen, startPosX, startPosZ, countRow, countCol);
//        }
//   }

    public void InitLineRenderer(LineRenderer lr) {
        lr.startWidth = lr.endWidth = 0.1f;
        lr.material.color = Color.red;
    }

    public void MakeGrid(LineRenderer lr, float sr, float sc, int rc, int cc) {
        int toggle = -1;
        List<Vector3> gridPos = new List<Vector3>();
        float ec = sc + cc * gridSize;

        gridPos.Add(new Vector3(sr, this.transform.position.y, sc));
        gridPos.Add(new Vector3(sr, this.transform.position.y, ec));

        Vector3 currentPos = new Vector3(sr, this.transform.position.y, ec);
        for(int i = 0; i < rc; i++) {
            Vector3 nextPos = currentPos;

            nextPos.x += gridSize;
            gridPos.Add(nextPos);

            nextPos.z += (cc * toggle * gridSize);
            gridPos.Add(nextPos);

            currentPos = nextPos;
            toggle *= -1;
        }

        currentPos.x = sr;
        gridPos.Add(currentPos);

        int colToggle = toggle = 1;
        if (currentPos.z == ec) colToggle = -1;

        for(int i = 0; i < cc; i++) {
            Vector3 nextPos = currentPos;

            nextPos.z += (colToggle * gridSize);
            gridPos.Add(nextPos);

            nextPos.x += (rc * toggle * gridSize);
            gridPos.Add(nextPos);

            currentPos = nextPos;
            toggle *= -1;
        }

        lr.positionCount = gridPos.Count;
        lr.SetPositions(gridPos.ToArray());
    }
}
