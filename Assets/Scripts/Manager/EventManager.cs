using System;
using System.Collections.Generic;
using UnityEngine;

//이벤트 관리
public class EventManager : Singleton<EventManager>
{
    private Dictionary<int, EventData> _curActiveEvent = new Dictionary<int, EventData>(); //현재 진행중인 이벤트
    
    //이벤트
    public event Action StartGetBotzimeEvent;  //봇짐 획득 이벤트 시작
    public event Action StartGetMapEvent; //지도 획득; 이벤트 시작 
    
    //일회성 이벤트 관리
    private bool _tutorialEnd; //튜토리얼 끝났는지
    
    //이벤트 활성화
    public void ActiveEvent(EventData event_)
    {
        if (event_ == null)
        {
            return;
        }

        if (_curActiveEvent.ContainsKey(event_.Id))
        {
            if (_curActiveEvent[event_.Id].IsClear)
            {
                return;
            }

            return;
        }

        _curActiveEvent.Add(event_.Id, event_);

        UIManager.Instance.GetUIComponent<QuestPopup>().AddQuest(event_); //퀘스트창 갱신
        UIManager.Instance.GetUIComponent<PlayPopup>().AddEventText(_curActiveEvent[event_.Id].Name);
        
        switch (event_.Id)
        {
            //등잔불 켜기
            case 10000:
                Navigation.Instance.NextNavigation();
                break;
        }
    }

    //현재 진행중인 이벤트가 있는지 확인
    public bool CheckActiveEvent()
    {
        foreach (var eventData in _curActiveEvent)
        {
            if (!eventData.Value.IsClear)
            {
                return true;
            }

            return false;
        }

        return false;
    }

    //현재 id의 이벤트를 진행중인지 확인
    public bool CheckActiveEvent(int id)
    {
        if (!_curActiveEvent.ContainsKey(id)) //이벤트 진행 유무
        {
            return false;
        }

        if (_curActiveEvent[id].IsClear) //이벤트 완료 유무
        {
            return false;
        }

        return true;
    }

    //이벤트 클리어하기
    public void ClearEvent(int id)
    {
        if (id == 0)
        {
            return;
        }
         
        if (!_curActiveEvent.ContainsKey(id))
        {
            ConsoleLogger.LogWarning("해당 퀘스트는 진행중이지 않습니다.");
            return;
        }

        _curActiveEvent[id].IsClear = true; //클리어
        UIManager.Instance.GetUIComponent<QuestPopup>().ClearQuest(id); //퀘스트창 갱신
        UIManager.Instance.GetUIComponent<PlayPopup>().DeleteEventText(_curActiveEvent[id].Name);
        
        switch (id)
        {
            //등잔불 켜기
            case 10000:
                StartGetBotzimeEvent?.Invoke(); 
                Navigation.Instance.NextNavigation();
                break;
            
            //봇짐 획득하기
            case 10010:
                StartGetMapEvent?.Invoke();
                Navigation.Instance.NextNavigation();
                break;
            
            //지도 획득하기
            case 10020:
                Navigation.Instance.NextNavigation();
                break;
            
            //포탈 이동하기
            case 10030:
                Navigation.Instance.NextNavigation();
                break;
            
            //장 승상댁과 대화하기
            case 10040:
                Navigation.Instance.gameObject.SetActive(false);

                if (!_tutorialEnd)
                {
                    DialogueManager.Instance.StartTalk(7060);
                    UIManager.Instance.GetUIComponent<PlayPopup>().ShowTimeBox(); //시간 표시하기
                    _tutorialEnd = true;
                }
                break;
        }
    }
}