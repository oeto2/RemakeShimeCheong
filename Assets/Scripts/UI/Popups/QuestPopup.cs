using TMPro;
using UnityEngine;
using Constants;
using UnityEngine.UI;

public class QuestPopup : UIBase
{
    [Header("세팅")]
    public TextMeshProUGUI _titleText; //퀘스트 제목
    public TextMeshProUGUI _contextText; //퀘스트 내용
    public Transform questNameTextBoxTr; //퀘스트 제목이 추가될 textboxTR

    private string _noneQuestText = "진행중인 퀘스트가 없습니다.";
    private void OnEnable()
    {
        //이벤트 유무에 따른 퀘스트창 세팅
        if (!EventManager.Instance.CheckActiveEvent())
        {
            _titleText.text = _noneQuestText;
        }
        else
        {
            if (_titleText.text == _noneQuestText)
            {
                _titleText.text = "";
            }
        }
    }
    
    //퀘스트 추가
    public void AddQuest(EventData eventData_)
    {
        GameObject questTextObj = ResourceManager.Instance.Instantiate(ResourcePrefabPath.QuestTextBox, questNameTextBoxTr);
        questTextObj.GetComponent<TextMeshProUGUI>().text = eventData_.Name;
        questTextObj.GetComponent<Button>().onClick.AddListener(() => ShowQuestDetails(eventData_));
    }
    
    //퀘스트 자세히 보기
    public void ShowQuestDetails(EventData eventData_)
    {
        _titleText.text = eventData_.Comment;
        _contextText.text = eventData_.Description;
    }
}
