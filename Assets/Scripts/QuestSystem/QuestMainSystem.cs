using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMainSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject questWindow;
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

    private string[] sQuestNameText = new string[100];
    private string[] sQuestExplainText = new string[100];
    private int[] sQuestCond = new int[100]; // 1 is press any key,
                                             // 11 is build church, 12 is build tent, 13 is build apple, 14 is granary,
                                             // 41 is builder job change ,
                                             // 71 use skill tree, 72 use bless
                                             // 80 Repeat Event, 81 select A event, 82 select B event

    private int iQuestNum = 0;

    [SerializeField]
    GameObject mEventWindow;

    private void Awake()
    {
        nameText = questNameText.GetComponent<TextMeshProUGUI>();
        explainText = questExplainText.GetComponent<TextMeshProUGUI>();
        rewardText1 = questRewardText1.GetComponent<TextMeshProUGUI>();
        rewardText2 = questRewardText2.GetComponent<TextMeshProUGUI>();

        rewardImg1 = questRewardImage1.GetComponent<Image>();
        rewardImg2 = questRewardImage2.GetComponent<Image>();

        SetQuestText();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeQuestExplainText("테스트입니다.");
        Sprite img = Resources.Load<Sprite>("MainGameUI/ResourceUI/Image_Apple");
        ChangeQuestRewardImage1(img);

        iQuestNum++;
        nameText.text = sQuestNameText[iQuestNum];
        explainText.text = sQuestExplainText[iQuestNum];
        StartCoroutine(CheckNextQuest());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CheckNextQuest()
    {
        while (iQuestNum < 18)
        {
            questWindow.SetActive(true);
/*            switch (sQuestCond[iQuestNum])
            {
                case 1://press any key
                    nameText.text = sQuestNameText[iQuestNum];
                    explainText.text = sQuestExplainText[iQuestNum];
                    if (Input.anyKeyDown)
                    {                        
                        iQuestNum++;
                    }
                    break;
                case 11://build church

                    break;
                case 12://build tent

                    break;
                case 13://build apple

                    break;
                case 14://build granary

                    break;
                case 41://builder job change

                    break;
                case 71://use skill tree
                    nameText.text = sQuestNameText[iQuestNum];
                    explainText.text = sQuestExplainText[iQuestNum];
                    break;
                case 72://use bless

                    break;
                case 81://select A event

                    break;
                case 82://select B event

                    break;
                default:
                    break;
            }*/

            nameText.text = sQuestNameText[iQuestNum];
            explainText.text = sQuestExplainText[iQuestNum];

            if (Input.anyKeyDown)
            {
                CheckQuestCond(1);
            }else if(iQuestNum == 13 || iQuestNum ==14)
            {
                mEventWindow.SetActive(true);
            }

            yield return null;
        }

        questWindow.SetActive(false);

        yield return null;
    }

    public void CheckQuestCond(int _questCond)
    {
        if(sQuestCond[iQuestNum] == _questCond)
        {
            iQuestNum++;
        }       
    }

    void SetQuestText()
    {
        sQuestNameText[1] = "안녕";
        sQuestExplainText[1] = "오셨습니까 교주님. 오실 줄 알았습니다..\r\n저를 따라서 퀘스트를 진행해주세요";
        sQuestCond[1] = 1;

        sQuestNameText[2] = "숭배의 시작";
        sQuestExplainText[2] = "신도들에게 예배할 곳이 필요하겠군요.. 재단을 지어주세요";
        sQuestCond[2] = 1;

        sQuestNameText[3] = "판자집을 짓자!";
        sQuestExplainText[3] = "우리의 불쌍한 신도들이 잘 살기 위해서는 잠을 잘 장소가 필요합니다.\r\n판자집을 지어서 신도들이 잘 수 있게 해주세요.";
        sQuestCond[3] = 1;

        sQuestNameText[4] = "?";
        sQuestExplainText[4] = "안돼";
        sQuestCond[4] = 1;

        sQuestNameText[5] = "초보티를 떼며";
        sQuestExplainText[5] = "교주님 잘하셨어요, 이제야 영지다워졌네요.\r\n이제 뭐가 필요할까요?";
        sQuestCond[5] = 1;

        sQuestNameText[6] = "과수원";
        sQuestExplainText[6] = "우리 생활에 중요한 세 가지가 있죠..\r\n이제 음식.. 음식이 있어야 하겠죠?\r\n과수원을 지어 신도들의 건강을 챙겨줍시다";
        sQuestCond[6] = 1;

        sQuestNameText[7] = "식량 저장소";
        sQuestExplainText[7] = "신도들이 아무리 식량을 만든다 한들, 저장하지 못한다면 아무도 먹지 못할것입니다.\r\n신도들이 식량을 생산하면 식량저장소에 보관을 하고, 식량저장소에 있는 식량을 먹을 것입니다.";
        sQuestCond[7] = 1;

        sQuestNameText[8] = "직분을 다 하여라";
        sQuestExplainText[8] = "아아 교주님, 이걸 보세요\r\n제 역할을 받지못해 초조해하는 신도가 많네요..\r\n신의 이름으로 그들에게 역할을 만들어줍시다";
        sQuestCond[8] = 1;

        sQuestNameText[9] = "??";
        sQuestExplainText[9] = "이번에는 실수하지 말아주셨으면 해요\r\n아차! 제가 어디까지 얘기했었죠? ";
        sQuestCond[9] = 1;

        sQuestNameText[10] = "그것이 신의 뜻일지니";
        sQuestExplainText[10] = "교주님! 영지가 어느 정도 활발해진 거 같아요\r\n활발해진 만큼 신도들이 너무 활발해지는 거 같네요\r\n신의 뜻으로 신도들이 엇나가지 않도록 할까요?";
        sQuestCond[10] = 71;

        sQuestNameText[11] = "나는 너희와 함께할지니";
        sQuestExplainText[11] = "교주님, 팍팍한 생활이 이어지니까 신도들이 신의 존재를 의심하기 시작했어요\r\n신도들에게 신의 존재를 느낄 수 있게끔 행동으로 보여주세요";
        sQuestCond[11] = 72;

        sQuestNameText[12] = "???";
        sQuestExplainText[12] = "나를 믿은 어린 양들아, 나를 믿지 못한 어리석은 양들아\r\n내가 너희에게 행하니라";
        sQuestCond[12] = 1;

        sQuestNameText[13] = "작은 불씨";
        sQuestExplainText[13] = "교주님, 영주가 어수선하지 않나요? 신도들의 행동도 수상해보이고요..\r\n빨리 해결해보자구요. 어서요!";
        sQuestCond[13] = 80;

        sQuestNameText[14] = "역사는 반복된다";
        sQuestExplainText[14] = "교주님, 영주가 어수선하지 않나요? 신도들의 행동도 수상해보이고요..\r\n빨리 해결해보자구요. 해결해보자구요, 해결해보자구요";
        sQuestCond[14] = 81;

        sQuestNameText[15] = "????";
        sQuestExplainText[15] = "당신 때문이야, 당신 때문이야, 당신 때문이야, 당신 때문이야.\r\n당신 때문이야, 당신 때문이야, 당신 때문이야, 당신 때문이야.";
        sQuestCond[15] = 1;

        sQuestNameText[16] = "NEVER";
        sQuestExplainText[16] = "교주님, 또 같은 선택을 반복하셨네요.\r\n이렇게 되면 어떤 일이 벌어질 지도 아셨을텐데요";
        sQuestCond[16] = 1;

        sQuestNameText[17] = "안녕";
        sQuestExplainText[17] = "결국 이번에도 이교도가 교회를 장악하고 있네요\r\n그럼 좀 이따 봅시다 교주님?";
        sQuestCond[17] = 1;
    }

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
}
