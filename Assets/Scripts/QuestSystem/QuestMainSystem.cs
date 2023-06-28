using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMainSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject questNameText;
    [SerializeField]
    private GameObject questExplainText;
    [SerializeField]
    private GameObject questRewardText1;
    [SerializeField]
    private GameObject questRewardText2;
    [SerializeField]
    private GameObject questRewardImage1;
    [SerializeField]
    private GameObject questRewardImage2;

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI explainText;
    private TextMeshProUGUI rewardText1;
    private TextMeshProUGUI rewardText2;

    private Image rewardImg1;
    private Image rewardImg2;

    void ChangeQuestNameText(string _QuestNameText)
    {
        nameText.text = _QuestNameText;
    }
    void ChangeQuestExplainText(string _QuestExplainText)
    {
        explainText.text = _QuestExplainText;
    }
    void ChangeQuestRewardText1(string _QuestRewardText1)
    {
        rewardText1.text = _QuestRewardText1;
    }
    void ChangeQuestRewardText2(string _QuestRewardText2)
    {
        rewardText2.text = _QuestRewardText2;
    }
    void ChangeQuestRewardImage1(Sprite _QuestRewardImage1)
    {
        rewardImg1.sprite = _QuestRewardImage1;
    }
    void ChangeQuestRewardImage2(Sprite _QuestRewardImage2)
    {
        rewardImg2.sprite = _QuestRewardImage2;
    }

    private void Awake()
    {
        nameText = questNameText.GetComponent<TextMeshProUGUI>();
        explainText = questExplainText.GetComponent<TextMeshProUGUI>();
        rewardText1 = questRewardText1.GetComponent<TextMeshProUGUI>();
        rewardText2 = questRewardText2.GetComponent<TextMeshProUGUI>();

        rewardImg1 = questRewardImage1.GetComponent<Image>();
        rewardImg2 = questRewardImage2.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeQuestExplainText("테스트입니다.");
        Sprite img = Resources.Load<Sprite>("MainGameUI/ResourceUI/Image_Apple");
        ChangeQuestRewardImage1(img);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
