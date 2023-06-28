using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RightBaseWindowManage : MonoBehaviour
{
    private Image blessingImage;

    private TextMeshProUGUI blessingNameText;
    private TextMeshProUGUI blessingExplainText;

    public void ChangeBlessingImage(Sprite _BlessingImage)
    {
        blessingImage.sprite = _BlessingImage;
    }
    public void ChangeBlessingNameText(string _BlessingNameText)
    {
        blessingNameText.text = _BlessingNameText;
    }
    public void ChangeBlessingExplainText(string _BlessingExplainText)
    {
        blessingExplainText.text = _BlessingExplainText;
    }

    private void Awake()
    {
        blessingImage = transform.GetChild(0).GetComponent<Image>();
        blessingNameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        blessingExplainText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(blessingImage);
        Debug.Log(blessingNameText);
        Debug.Log(blessingExplainText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
