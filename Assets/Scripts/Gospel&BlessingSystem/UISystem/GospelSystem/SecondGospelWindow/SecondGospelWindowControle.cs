using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SecondGospelWindowControle : MonoBehaviour
{
    GameObject explainName;
    GameObject explain;

    private TextMeshProUGUI explainNameText;
    private TextMeshProUGUI explainText;

    public void SetActiveExplain(bool _activeExplain)
    {
        explainName.SetActive(_activeExplain);
        explain.SetActive(_activeExplain);
    }

    public void SetExplainNameText(string _explainNameText)
    {
        explainNameText.text = _explainNameText;
    }
    public void SetExplainText(string _explainText)
    {
        explainText.text = _explainText;
    }

    private void Awake()
    {
        explainName = transform.GetChild(5).gameObject;
        explain = transform.GetChild(6).gameObject;

        explainNameText = explainName.GetComponent<TextMeshProUGUI>();
        explainText = explain.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
