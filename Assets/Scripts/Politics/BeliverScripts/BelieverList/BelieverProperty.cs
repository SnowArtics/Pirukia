/* 신도목록 리스트에 표시될
 * 개별 개체의 내용물 구성 컴포넌트
*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private TextMeshProUGUI textName;
    private TextMeshProUGUI textLevel;

    // Start is called before the first frame update
    public void initBeliever(GameObject believer)
    {
        // Believer에서 필요한 컴포넌트 추출
        this.believerObj = believer;
        this.believerComp = (Believer) believerObj.GetComponent("Believer");
        // UI에 표시할 TextMesh 추출
        // Name
        this.textNameObj = gameObject.transform.Find("BelieverName").Find("NameText").gameObject;
        this.textName = textNameObj.GetComponent<TextMeshProUGUI>();
        // Level
        //this.textLevelObj = gameObject.transform.Find("LevelButton").Find("Level").gameObject;
        //this.textLevel = textLevelObj.GetComponent<TextMeshProUGUI>();
        // UI초기화
        this.textName.text = this.believerComp.GetName();
        //this.textLevel.text = composeStrLevel();
    }

    private string composeStrLevel()
    {
        // TODO: workGroup을 통해 한글 표출
        // TODO: 
        return "LV." + "0";
    }

    public void composeUI()
    {
        // TODO: 매 프래임 혹은 트리거에 의해
        // 내용물을 갱신해 주는 함수 작성
        this.textName.text = this.believerComp.GetName();
    }

    public void SetWorkGroup(int g)
    {
        believerComp.SetWorkGroup(g);
    }

    private void Start()
    {

    }
    private void Update()
    {
        
    }
}
