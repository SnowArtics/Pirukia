/* �ŵ���� ����Ʈ�� ǥ�õ�
 * ���� ��ü�� ���빰 ���� ������Ʈ
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

    // GameObject���� �ʿ��� ������Ʈ ��ü�� ������ ����
    private Believer believerComp;
    private TextMeshProUGUI textName;
    private TextMeshProUGUI textLevel;

    // Start is called before the first frame update
    public void initBeliever(GameObject believer)
    {
        // Believer���� �ʿ��� ������Ʈ ����
        this.believerObj = believer;
        this.believerComp = (Believer) believerObj.GetComponent("Believer");
        // UI�� ǥ���� TextMesh ����
        // Name
        this.textNameObj = gameObject.transform.Find("NameButton").Find("Name").gameObject;
        this.textName = textNameObj.GetComponent<TextMeshProUGUI>();
        // Level
        this.textLevelObj = gameObject.transform.Find("LevelButton").Find("Level").gameObject;
        this.textLevel = textLevelObj.GetComponent<TextMeshProUGUI>();
        // UI�ʱ�ȭ
        Debug.Log(this.textName.ToString());
        Debug.Log(this.believerComp.ToString());
        this.textName.text = this.believerComp.GetName();
        this.textLevel.text = composeStrLevel();
    }

    private string composeStrLevel()
    {
        // TODO: workGroup�� ���� �ѱ� ǥ��
        // TODO: 
        return "LV." + "0";
    }

    private void composeUI()
    {
        // TODO: �� ������ Ȥ�� Ʈ���ſ� ����
        // ���빰�� ������ �ִ� �Լ� �ۼ�
    }

    private void Start()
    {

    }
    private void Update()
    {
        
    }
}
