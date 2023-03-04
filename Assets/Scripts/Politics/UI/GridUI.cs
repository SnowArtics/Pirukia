using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUI : MonoBehaviour
{
    private float startPosX = 0;                // 그리드 시작하는 x 좌표
    private float startPosY = 0.001f;           // 그리드 시작하는 y 좌표
    private float startPosZ = 0;                // 그리드 시작하는 z 좌표
    private float gridSize = 2;                 // 그리드 한칸의 크기
    private int countRow = 50;                  // 그리드 가로 칸 개수
    private int countCol = 50;                  // 그리드 세로 칸 개수 

    private LineRenderer lineRen;


    public void Awake() {
        transform.position = new Vector3(0, startPosY, 0);
        lineRen = this.GetComponent<LineRenderer>();
        InitLineRenderer(lineRen);

        MakeGrid(lineRen, startPosX, startPosZ, countRow, countCol);
    }

    // LineRenderer 컴포넌트를 설정
    public void InitLineRenderer(LineRenderer lr) {
        lr.startWidth = lr.endWidth = 0.1f;             // 두께를 0.1로 설정
        lr.material.color = Color.gray;                  // 색을 회색으로 설정
    }

    // LineRenderer 그리기
    // ㄹ자 형식으로 하나의 Line만으로 grid를 완성할 수 있도록 한다.
    public void MakeGrid(LineRenderer lr, float startX, float startZ, int countX, int countZ) {
        int toggle = -1;
        List<Vector3> gridPos = new List<Vector3>();
        float endZ = startZ + countZ * gridSize;

        // 각 끝점에 대한 vector값을 gridPos 리스트에 추가 
        gridPos.Add(new Vector3(startX, startPosY, startZ));
        gridPos.Add(new Vector3(startX, startPosY, endZ));
        Vector3 currentPos = new Vector3(startX, startPosY, endZ);

        // grid 가로 수만큼 반복
        for(int i = 0; i < countX; i++) {
            Vector3 nextPos = currentPos;

            // grid의 칸만큼 x좌표를 이동한 좌표를 리스트에 추가
            nextPos.x += gridSize;
            gridPos.Add(nextPos);

            // 직전 좌표의 반대 방향으로 z좌표를 이동
            nextPos.z += (countZ * toggle * gridSize);
            gridPos.Add(nextPos);

            currentPos = nextPos;
            toggle *= -1;
        }

        /* 이하는 x방향으로 ㄹ모양을 만든 뒤, z 방향으로 진행하여 바둑판 모양을 만드는 코드 */

        // x좌표를 시작한 값으로 설정한 후, z 방향으로 움직이게한다.
        currentPos.x = startX;
        gridPos.Add(currentPos);

        // z칸이 홀수이면 z값이 증가하도록, 짝수이면 z값이 감소하도록 그려야 한다.
        // z값의 증가는 항상 일정하므로, 방향을 결정하는 colToggle 변수를 선언해 그 방향을 고정한다.
        int colToggle = toggle = 1;
        if (currentPos.z == endZ) colToggle = -1;

        // grid의 세로 수만큼 반복
        for(int i = 0; i < countZ; i++) {
            Vector3 nextPos = currentPos;

            nextPos.z += (colToggle * gridSize);
            gridPos.Add(nextPos);

            nextPos.x += (countX * toggle * gridSize);
            gridPos.Add(nextPos);

            currentPos = nextPos;
            toggle *= -1;
        }

        // grid를 그릴 각 좌표에 대한 리스트를 LineRenderer 컴포넌트로 전달하여 해당 grid가 나타나도록 한다.
        lr.positionCount = gridPos.Count;
        lr.SetPositions(gridPos.ToArray());
    }
}