using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelieverManage : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> believers = new List<GameObject>();
    // Start is called before the first frame update
    [SerializeField]
    private GameObject believerPref;

    void Start()
    {
        // 테스트용: Believer태그를 가진 객체들을 가져오기
        believers.AddRange(GameObject.FindGameObjectsWithTag("Believer"));
        // TODO: 저장된 데이터에서 신도정보를 가져와 초기화
        for (int i=0; i<1; i++)
            AddBeliever();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 신도 생성
    public void AddBeliever()
    {
        GameObject believer = Instantiate(believerPref);
        believers.Add(believer);
        believer.GetComponent<Believer>().SetName($"안개{believers.Count}");
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
