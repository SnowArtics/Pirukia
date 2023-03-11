using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelieverProperty : MonoBehaviour
{
    [SerializeField]
    GameObject believerObj;
    [SerializeField]
    GameObject textNameObj;
    [SerializeField]
    GameObject textLevelObj;

    // GameObject에서 필요한 컴포넌트 객체를 저장할 변수
    private Believer believerComp;
    private TextMesh textName;
    private TextMesh textLevel;

    // Start is called before the first frame update
    public void initBeliever(GameObject bel)
    {
        // Believer에서 필요한 컴포넌트 추출
        this.believerObj = bel;
        this.believerComp = (Believer) believerObj.GetComponent("Believer");
        // UI에 표시할 TextMesh 추출
        this.textName = (TextMesh)textNameObj.GetComponent("TextMesh");
        this.textLevel = (TextMesh)textLevel.GetComponent("TextMesh");
        // UI초기화
        this.textName.text = this.believerComp.GetName();
        this.textLevel.text = composeLevelUI();
    }

    private string composeLevelUI()
    {
        // TODO: workGroup을 통해 한글 표출
        // TODO: 
        return "LV." + "0";
    }
}
