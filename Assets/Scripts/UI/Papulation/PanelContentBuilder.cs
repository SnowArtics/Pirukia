// * This script manage the Belivers Status Panel
// *
// * �ŵ���Ȳ �г� ���� ��ũ��Ʈ
// * ����Ű�� �Է� �����̷��� ó���� �ʿ��� (�����ʿ�)
// * �ŵ� ��ü�� �����ϴ� ��ü�� �����ϰ�
// * �̷κ��� �����͸� �޾ƿ����� �ؾ���

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum StatusCat
{
    PeopleShare,
    Agricultural,
    Mining,
    Industrial,
    CivilEngineering,
    Commercial
}

public class PanelContentBuilder : MonoBehaviour
{
    // �ŵ� ��Ȳ �г��� ��ġ ǥ�� ���� ����.
    private TextMeshProUGUI valueArea;

    // ǥ���� ������ ���� �迭
    private int numData;
    private float [] statusData;

    [SerializeField]
    private string currentUnit = "$";

    PanelContentBuilder()
    {
        numData = System.Enum.GetValues(typeof(StatusCat)).Length;
        statusData = new float[numData];
    }


    // Start is called before the first frame update
    void Start()
    {
        // ��ġ�� ǥ���� TextMesh�� ã��
        valueArea = transform.Find("Value").GetComponent<TextMeshProUGUI>();
        // TextMesh�� ã�� ������ ��� �α�
        if (valueArea == null)
            Debug.LogWarning("Cannot found Value in PapulationUI");
        refreshValue();
    }

    // �ŵ� ��Ȳ�� ������ ���� �� ȣ���Ͽ� ǥ�� ����
    public void refreshValue()
    {
        // TODO: �ŵ� ���� ��ü ���� �� ������ �޾ƿ���
        // Text Mesh�� ǥ�õ� ����
        string statusText = "";
        statusText += rateText(statusData[(int)StatusCat.PeopleShare]);
        statusText += massText(statusData[(int)StatusCat.Agricultural]);
        statusText += massText(statusData[(int)StatusCat.Mining]);
        statusText += massText(statusData[(int)StatusCat.CivilEngineering]);
        statusText += currentText(statusData[(int)StatusCat.Industrial]);
        statusText += currentText(statusData[(int)StatusCat.Commercial]);
        statusText = statusText.TrimEnd();

        valueArea.text = statusText;
        Debug.Log(statusText);
    }

    // si �������ξ�
    private string siPrefix(double num)
    {
        // 0���� ���� �������ξ�� �ٷ��� ����
        string[] prefix = { "", "k", "M", "G", "T" };

        int exp = (int)Math.Truncate(Math.Log10(num));
        exp = exp > 0 ? exp : 0;
        int si = (int)Math.Truncate(exp / 3.0);
        si = si < 5 ? si : 4;

        return String.Format("{0:F1} {1}", num/Math.Pow(10, si*3), prefix[si]);
    }

    // ������ ǥ���ϴ� ���ڿ� ����
    private string rateText(double num)
    {
        return String.Format("{0:F1}%\n", num*100);
    }

    // ���Ը� ǥ���ϴ� ���ڿ� ���� �׻� g����
    private string massText(double num)
    {
        string si = siPrefix(num);

        return String.Format("{0}g\n", si);
    }

    // ȭ�� ǥ���ϴ� ���ڿ� ����
    private string currentText(double num)
    {
        string si = siPrefix(num);

        return String.Format("{0}{1}\n", si, currentUnit);
    }
}
