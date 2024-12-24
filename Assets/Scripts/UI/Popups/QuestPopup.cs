using System.Collections.Generic;
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

    private Dictionary<int, GameObject> _curActiveQuest = new Dictionary<int, GameObject>();
    private int _activeEventID; //현재 보여주고 있는 이벤트 ID

    protected override void Start()
    {
        base.Start();
        _playerController.OnQuestPopupEvent += ClearQuestPopupText;
    }

    //퀘스트 추가
    public void AddQuest(EventData eventData_)
    {
        GameObject questTextObj = ResourceManager.Instance.Instantiate(ResourcePrefabPath.QuestTextBox, questNameTextBoxTr);
        questTextObj.GetComponent<TextMeshProUGUI>().text = eventData_.Name;
        questTextObj.GetComponent<Button>().onClick.AddListener(() => ShowQuestDetails(eventData_));
        
        _curActiveQuest.Add(eventData_.Id,questTextObj); //퀘스트 추가
    }
    
    //퀘스트 클리어
    public void ClearQuest(int id)
    {
        //현재 진행중인 이벤트라면
        if (EventManager.Instance.CheckActiveEvent(id))
        {
            EventManager.Instance.ClearEvent(id); //이벤트 완료하기
        }
        
        //존재하지 않는 이벤트일 경우
        if (!_curActiveQuest.ContainsKey(id))
        {
            return;
        }
        
        Destroy(_curActiveQuest[id]);
        _curActiveQuest.Remove(id);
    }
    
    //퀘스트 자세히 보기
    public void ShowQuestDetails(EventData eventData_)
    {
        _activeEventID = eventData_.Id;
        _titleText.text = eventData_.Comment;
        _contextText.text = eventData_.Description;
    }
    
    //퀘스트 창 글자 비우기
    private void ClearQuestPopupText()
    {
        //현재 보여주고 있는 퀘스트가 아직 진행중이면 비우지 않기
        if (_curActiveQuest.ContainsKey(_activeEventID))
            return;
        
        _titleText.text = "";
        _contextText.text = "";
    }
}
