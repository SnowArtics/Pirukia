using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField] private float cellSize = 1f; // 칸의 크기
    [SerializeField] private float lineWidth = 0.1f; // 선의 폭
    [SerializeField] private Color lineColor = Color.blue; // 선의 색상
    [SerializeField] private float lineOpacity = 0.5f; // 선의 투명도

    private GameObject gridObject; // Grid 오브젝트
    private Material lineMaterial; // 선의 머티리얼

    private void Start()
    {
        CreateGrid();
    }

    // 격자무늬 그리기
    public void DrawGrid()
    {
        if (gridObject == null)
            CreateGrid();
    }

    // 그려진 격자무늬 지우기
    public void ClearGrid()
    {
        if (gridObject != null)
            Destroy(gridObject);
    }

    // 이미 그려진 라인의 폭 조절
    public void SetLineWidth(float width)
    {
        lineWidth = width;
        UpdateLineProperties();
    }

    // 이미 그려진 라인의 투명도 조절
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