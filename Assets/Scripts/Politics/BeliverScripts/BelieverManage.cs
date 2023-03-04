using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelieverManage : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> believers = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        // �׽�Ʈ��: Believer�±׸� ���� ��ü���� ��������
        believers.AddRange(GameObject.FindGameObjectsWithTag("Believer"));
        // TODO: ����� �����Ϳ��� �ŵ������� ������ �ʱ�ȭ

    }

    // Update is called once per frame
    void Update()
    {

    }

    // �ŵ� ����
    public void AddBeliever()
    {

    }

    public List<GameObject> sortByName(bool isDes)
    {
        this.believers.Sort((b1, b2) => 
            { return b1.GetComponent<Believer>().GetName().CompareTo(b2.GetComponent<Believer>().GetName()); });
        if (isDes)
        {
            this.believers.Reverse();
        }
        return this.believers;
    }
}
