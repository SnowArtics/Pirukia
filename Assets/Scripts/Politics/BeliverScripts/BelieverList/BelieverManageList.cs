using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelieverManageList : MonoBehaviour
{
    [SerializeField]
    public GameObject believerButton;

    private GameObject believerList;

    // Start is called before the first frame update
    void Start()
    {
        believerList = GameObject.FindWithTag("BelieverList");

        // 리스트 업데이트 리스너 등록
        believerList.GetComponent<BelieverList>().AddUpdateListener(gameObject);

        UpdateList();
    }


    public void UpdateList()
    {
        EraseList();

        int count = believerList.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            // 목록 생성
            GameObject element = Instantiate(believerButton, gameObject.transform);
            // 목록 내용 채워넣기
            BelieverProperty elementComp = element.GetComponent<BelieverProperty>();
            elementComp.initBeliever(believerList.transform.GetChild(i).gameObject);
        }
    }
    
    void EraseList()
    {
        Debug.Log($"first: {gameObject.transform.childCount}");
        GameObject[] listObj = new GameObject[gameObject.transform.childCount];
        int count = gameObject.transform.childCount;
        for (int i=0; i<count; i++)
        {
            listObj[i] = gameObject.transform.GetChild(i).gameObject;
        }
        for (int i=0; i<count; i++)
        {
            Destroy(listObj[i]);
        }
    }

}
