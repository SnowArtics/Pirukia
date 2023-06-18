using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField] private float cellSize = 1f; // ĭ�� ũ��
    [SerializeField] private float lineWidth = 0.1f; // ���� ��
    [SerializeField] private Color lineColor = Color.blue; // ���� ����
    [SerializeField] private float lineOpacity = 0.5f; // ���� ����

    private GameObject gridObject; // Grid ������Ʈ
    private Material lineMaterial; // ���� ��Ƽ����

    private void Start()
    {
        CreateGrid();
    }

    // ���ڹ��� �׸���
    public void DrawGrid()
    {
        if (gridObject == null)
            CreateGrid();
    }

    // �׷��� ���ڹ��� �����
    public void ClearGrid()
    {
        if (gridObject != null)
            Destroy(gridObject);
    }

    // �̹� �׷��� ������ �� ����
    public void SetLineWidth(float width)
    {
        lineWidth = width;
        UpdateLineProperties();
    }

    // �̹� �׷��� ������ ���� ����
    public void SetLineOpacity(float opacity)
    {
        lineOpacity = opacity;
        UpdateLineProperties();
    }

    private void CreateGrid()
    {
        gridObject = new GameObject("Grid");
        gridObject.transform.SetParent(transform);

        for (float x = 0; x <= 1f; x += cellSize)
        {
            DrawLine(new Vector3(x, 0f, 0f), new Vector3(x, 0f, 1f));
        }

        for (float z = 0; z <= 1f; z += cellSize)
        {
            DrawLine(new Vector3(0f, 0f, z), new Vector3(1f, 0f, z));
        }

        UpdateLineProperties();
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject lineObject = new GameObject("Line");
        lineObject.transform.SetParent(gridObject.transform);

        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.TransformPoint(start));
        lineRenderer.SetPosition(1, transform.TransformPoint(end));
    }

    private void UpdateLineProperties()
    {
        if (gridObject == null)
            return;

        if (lineMaterial == null)
        {
            lineMaterial = new Material(Shader.Find("Standard"));
            lineMaterial.SetColor("_Color", lineColor);
        }

        Renderer[] lineRenderers = gridObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in lineRenderers)
        {
            renderer.sharedMaterial = lineMaterial;
            renderer.transform.localScale = new Vector3(lineWidth, 1f, lineWidth);
            renderer.sharedMaterial.color = new Color(lineColor.r, lineColor.g, lineColor.b, lineOpacity);
        }
    }
}