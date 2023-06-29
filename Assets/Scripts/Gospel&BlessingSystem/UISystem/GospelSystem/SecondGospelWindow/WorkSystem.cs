using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkSystem : MonoBehaviour
{
    Image work1Button;
    TextMeshProUGUI work1NameText;
    TextMeshProUGUI Work1Text;
    Image Work2;
    TextMeshProUGUI Work2NameText;
    TextMeshProUGUI Work2Text;

    private void Awake()
    {
        work1Button = transform.GetChild(0).gameObject.GetComponent<Image>();
        work1NameText = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        Work1Text = transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        Work2 = transform.GetChild(3).gameObject.GetComponent<Image>();
        Work2NameText = transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        Work2Text = transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>();
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
