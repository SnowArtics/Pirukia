// * This script manage the Belivers Status Panel
// *
// * 신도현황 패널 관리 스크립트
// * 단축키는 입력 리다이렉션 처리가 필요함 (구현필요)
// * 신도 객체를 관리하는 주체를 구현하고
// * 이로부터 데이터를 받아오도록 해야함

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
    // 신도 현황 패널의 수치 표시 영역 저장.
    private TextMeshProUGUI valueArea;

    // 표시할 데이터 저장 배열
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
        // 수치를 표시할 TextMesh를 찾음
        valueArea = transform.Find("Value").GetComponent<TextMeshProUGUI>();
        // TextMesh를 찾지 못했을 경우 로그
        if (valueArea == null)
            Debug.LogWarning("Cannot found Value in PapulationUI");
        refreshValue();
    }

    // 신도 현황이 변경이 있을 때 호출하여 표시 갱신
    public void refreshValue()
    {
        // TODO: 신도 관리 주체 구현 후 데이터 받아오기
        // Text Mesh에 표시될 변수
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

    // si 십진접두어
    private string siPrefix(double num)
    {
        // 0보다 작은 십진접두어는 다루지 않음
        string[] prefix = { "", "k", "M", "G", "T" };

        int exp = (int)Math.Truncate(Math.Log10(num));
        exp = exp > 0 ? exp : 0;
        int si = (int)Math.Truncate(exp / 3.0);
        si = si < 5 ? si : 4;

        return String.Format("{0:F1} {1}", num/Math.Pow(10, si*3), prefix[si]);
    }

    // 비율을 표시하는 문자열 생성
    private string rateText(double num)
    {
        return String.Format("{0:F1}%\n", num*100);
    }

    // 무게를 표시하는 문자열 생성 항상 g단위
    private string massText(double num)
    {
        string si = siPrefix(num);

        return String.Format("{0}g\n", si);
    }

    // 화폐를 표시하는 문자열 생성
    private string currentText(double num)
    {
        string si = siPrefix(num);

        return String.Format("{0}{1}\n", si, currentUnit);
    }
}
