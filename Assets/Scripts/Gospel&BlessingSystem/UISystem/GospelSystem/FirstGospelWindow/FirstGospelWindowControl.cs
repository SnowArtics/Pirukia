using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class FirstGospelWindowControl : MonoBehaviour
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

    void Awake()
    {
        explainName = transform.GetChild(6).gameObject;
        explain = transform.GetChild(7).gameObject;

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
