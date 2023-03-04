using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUI : MonoBehaviour
{
    private float startPosX = 0;                // �׸��� �����ϴ� x ��ǥ
    private float startPosY = 0.001f;           // �׸��� �����ϴ� y ��ǥ
    private float startPosZ = 0;                // �׸��� �����ϴ� z ��ǥ
    private float gridSize = 2;                 // �׸��� ��ĭ�� ũ��
    private int countRow = 50;                  // �׸��� ���� ĭ ����
    private int countCol = 50;                  // �׸��� ���� ĭ ���� 

    private LineRenderer lineRen;


    public void Awake() {
        transform.position = new Vector3(0, startPosY, 0);
        lineRen = this.GetComponent<LineRenderer>();
        InitLineRenderer(lineRen);

        MakeGrid(lineRen, startPosX, startPosZ, countRow, countCol);
    }

    // LineRenderer ������Ʈ�� ����
    public void InitLineRenderer(LineRenderer lr) {
        lr.startWidth = lr.endWidth = 0.1f;             // �β��� 0.1�� ����
        lr.material.color = Color.gray;                  // ���� ȸ������ ����
    }

    // LineRenderer �׸���
    // ���� �������� �ϳ��� Line������ grid�� �ϼ��� �� �ֵ��� �Ѵ�.
    public void MakeGrid(LineRenderer lr, float startX, float startZ, int countX, int countZ) {
        int toggle = -1;
        List<Vector3> gridPos = new List<Vector3>();
        float endZ = startZ + countZ * gridSize;

        // �� ������ ���� vector���� gridPos ����Ʈ�� �߰� 
        gridPos.Add(new Vector3(startX, startPosY, startZ));
        gridPos.Add(new Vector3(startX, startPosY, endZ));
        Vector3 currentPos = new Vector3(startX, startPosY, endZ);

        // grid ���� ����ŭ �ݺ�
        for(int i = 0; i < countX; i++) {
            Vector3 nextPos = currentPos;

            // grid�� ĭ��ŭ x��ǥ�� �̵��� ��ǥ�� ����Ʈ�� �߰�
            nextPos.x += gridSize;
            gridPos.Add(nextPos);

            // ���� ��ǥ�� �ݴ� �������� z��ǥ�� �̵�
            nextPos.z += (countZ * toggle * gridSize);
            gridPos.Add(nextPos);

            currentPos = nextPos;
            toggle *= -1;
        }

        /* ���ϴ� x�������� ������� ���� ��, z �������� �����Ͽ� �ٵ��� ����� ����� �ڵ� */

        // x��ǥ�� ������ ������ ������ ��, z �������� �����̰��Ѵ�.
        currentPos.x = startX;
        gridPos.Add(currentPos);

        // zĭ�� Ȧ���̸� z���� �����ϵ���, ¦���̸� z���� �����ϵ��� �׷��� �Ѵ�.
        // z���� ������ �׻� �����ϹǷ�, ������ �����ϴ� colToggle ������ ������ �� ������ �����Ѵ�.
        int colToggle = toggle = 1;
        if (currentPos.z == endZ) colToggle = -1;

        // grid�� ���� ����ŭ �ݺ�
        for(int i = 0; i < countZ; i++) {
            Vector3 nextPos = currentPos;

            nextPos.z += (colToggle * gridSize);
            gridPos.Add(nextPos);

            nextPos.x += (countX * toggle * gridSize);
            gridPos.Add(nextPos);

            currentPos = nextPos;
            toggle *= -1;
        }

        // grid�� �׸� �� ��ǥ�� ���� ����Ʈ�� LineRenderer ������Ʈ�� �����Ͽ� �ش� grid�� ��Ÿ������ �Ѵ�.
        lr.positionCount = gridPos.Count;
        lr.SetPositions(gridPos.ToArray());
    }
}