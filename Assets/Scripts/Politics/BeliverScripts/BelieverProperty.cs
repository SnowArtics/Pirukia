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

    // GameObject���� �ʿ��� ������Ʈ ��ü�� ������ ����
    private Believer believerComp;
    private TextMesh textName;
    private TextMesh textLevel;

    // Start is called before the first frame update
    public void initBeliever(GameObject bel)
    {
        // Believer���� �ʿ��� ������Ʈ ����
        this.believerObj = bel;
        this.believerComp = (Believer) believerObj.GetComponent("Believer");
        // UI�� ǥ���� TextMesh ����
        this.textName = (TextMesh)textNameObj.GetComponent("TextMesh");
        this.textLevel = (TextMesh)textLevel.GetComponent("TextMesh");
        // UI�ʱ�ȭ
        this.textName.text = this.believerComp.GetName();
        this.textLevel.text = composeLevelUI();
    }

    private string composeLevelUI()
    {
        // TODO: workGroup�� ���� �ѱ� ǥ��
        // TODO: 
        return "LV." + "0";
    }
}