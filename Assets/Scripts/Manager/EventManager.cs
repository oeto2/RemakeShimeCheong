using System;
using System.Collections.Generic;
using UnityEngine;

//이벤트 관리
public class EventManager : Singleton<EventManager>
{
    private Dictionary<int, EventData> _curActiveEvent = new Dictionary<int, EventData>(); //현재 진행중인 이벤트
 
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
        if (!_curActiveEvent.ContainsKey(id))
        {
            return false;
        }

        return true;
    }
    
    //이벤트 클리어하기
    public void ClearEvent(int id)
    {
        if (!_curActiveEvent.ContainsKey(id))
        {
            ConsoleLogger.LogWarning("해당 퀘스트는 진행중이지 않습니다.");
            return;
        }

        _curActiveEvent[id].IsClear = true;
    }
}