using System;
using System.Collections.Generic;
using Constants;

[Serializable]
public class ItemData
{
    public int Id;
    public ObjectType ObjectType;
    public string Name;
    public string Comment;
    public bool IsUsing;
    public int IndexNum;
    public string SpritePath;

    public ItemData()
    {
    }

    public ItemData(int id, ObjectType objectType, string name, string comment, bool isUsing, int indexNum,
        string spritePath)
    {
        Id = id;
        ObjectType = objectType;
        Name = name;
        Comment = comment;
        IsUsing = isUsing;
        IndexNum = indexNum;
        SpritePath = spritePath;
    }
}


[Serializable]
public class ClueData
{
    public int Id;
    public ObjectType ObjectType;
    public string Name;
    public string Comment;
    public bool IsUsing;
    public int IndexNum;
    public string SpritePath;

    public ClueData()
    {
    }

    public ClueData(int id, ObjectType objectType, string name, string comment, bool isUsing, int indexNum,
        string spritePath)
    {
        Id = id;
        ObjectType = objectType;
        Name = name;
        Comment = comment;
        IsUsing = isUsing;
        IndexNum = indexNum;
        SpritePath = spritePath;
    }
}

[Serializable]
public class DialogueData
{
    public int Id;
    public SpeakerType SpeakerType;
    public string Name;
    public string Comment;
    public bool IsUsing;
    public int IndexNum;
    public int NextCommentNum;
    public int EquipCondition; //대화 조건 : 장착 아이템
    public int EventCondition; //대화 조건 : 이벤트
    public int StartEventID;
    public int EndEventID;
    public int RewardID;

    public DialogueData()
    {
    }

    public DialogueData(int id, SpeakerType speakerType, string name, string comment, bool isUsing, int indexNum,
        int nextCommentNum,
        int equipCondition, int eventCondition, int startEventID, int endEventID, int rewardID)
    {
        Id = id;
        SpeakerType = speakerType;
        Name = name;
        Comment = comment;
        IsUsing = isUsing;
        IndexNum = indexNum;
        NextCommentNum = nextCommentNum;
        EquipCondition = equipCondition;
        EventCondition = eventCondition;
        StartEventID = startEventID;
        EndEventID = endEventID;
        RewardID = rewardID;
    }
}

[Serializable]
public class EventData
{
    public int Id;
    public EventType EventType;
    public string Name;
    public string Comment;
    public string Description;
    public bool IsClear;
    public int IndexNum;

    public EventData()
    {
    }

    public EventData(int id, EventType eventType, string name, string comment, string description, bool isClear,
        int indexNum)
    {
        Id = id;
        EventType = eventType;
        Name = name;
        Comment = comment;
        Description = description;
        IsClear = isClear;
        IndexNum = indexNum;
    }
}

public class DBManager : Singleton<DBManager>
{
    private GoogleSheetSO _dataSO; //전체 데이터
    private readonly Dictionary<int, ItemData> _itemDB = new Dictionary<int, ItemData>(); //아이템 데이터
    private readonly Dictionary<int, ClueData> _clueDB = new Dictionary<int, ClueData>(); //단서 데이터
    private readonly Dictionary<int, DialogueData> _dialogueDB = new Dictionary<int, DialogueData>(); //대화 데이터
    private readonly Dictionary<int, EventData> _eventDB = new Dictionary<int, EventData>(); //이벤트 데이터

    private ItemData _itemDataTemp = new ItemData();
    private ClueData _clueDataTemp = new ClueData();
    private DialogueData _dialogueDataTemp = new DialogueData();
    private EventData _eventDataTemp = new EventData();

    private void Awake()
    {
        _dataSO = ResourceManager.Instance.Load<GoogleSheetSO>("DataSO/GoogleSheetSO");

        Init();
    }

    public override void Init()
    {
        //아이템 DB 데이터 초기화
        foreach (var itemTableData in _dataSO.Item_TableList)
        {
            _itemDataTemp = new ItemData(itemTableData.Id, Enum.Parse<ObjectType>(itemTableData.ObjectType),
                itemTableData.Name,
                itemTableData.Comment,
                itemTableData.IsUsing, itemTableData.IndexNum, itemTableData.SpritePath);

            _itemDB.Add(itemTableData.Id, _itemDataTemp);
        }

        //단서 DB 데이터 초기화
        foreach (var clueTableData in _dataSO.Clue_TableList)
        {
            _clueDataTemp = new ClueData(clueTableData.Id, Enum.Parse<ObjectType>(clueTableData.ObjectType),
                clueTableData.Name,
                clueTableData.Comment,
                clueTableData.IsUsing, clueTableData.IndexNum, clueTableData.SpritePath);

            _clueDB.Add(clueTableData.Id, _clueDataTemp);
        }

        //대화 DB 데이터 초기화
        foreach (var dialogueTableData in _dataSO.Dialogue_TableList)
        {
            _dialogueDataTemp = new DialogueData(dialogueTableData.Id,
                Enum.Parse<SpeakerType>(dialogueTableData.SpeakerType), dialogueTableData.Name,
                dialogueTableData.Comment,
                dialogueTableData.IsUsing, dialogueTableData.IndexNum, dialogueTableData.NextCommentNum,
                dialogueTableData.EquipCondition, dialogueTableData.EventCondition,
                dialogueTableData.StartEventID, dialogueTableData.EndEventID, dialogueTableData.RewardID);

            _dialogueDB.Add(dialogueTableData.Id, _dialogueDataTemp);
        }

        //이벤트 DB 데이터 초기화
        foreach (var eventTableData in _dataSO.Event_TableList)
        {
            _eventDataTemp = new EventData(eventTableData.Id, Enum.Parse<EventType>(eventTableData.EventType),
                eventTableData.Name, eventTableData.Comment,
                eventTableData.Description, eventTableData.IsClear,
                eventTableData.IndexNum);

            _eventDB.Add(eventTableData.Id, _eventDataTemp);
        }
    }

    public ItemData GetItemData(int itemId)
    {
        //해당 ID의 데이터가 존재하지 않다면 
        if (!CheckContainsItem(itemId))
        {
            ConsoleLogger.LogError($"ID:{itemId}의 아이템 데이터를 찾을 수 없습니다.");
            return null;
        }

        return _itemDB[itemId];
    }

    public ClueData GetClueData(int clueId)
    {
        //해당 ID의 데이터가 존재하지 않다면 
        if (!CheckContainsClue(clueId))
        {
            ConsoleLogger.LogError($"ID:{clueId}의 단서 데이터를 찾을 수 없습니다.");
            return null;
        }

        return _clueDB[clueId];
    }

    public Dictionary<int, DialogueData> GetDialogueDatas()
    {
        return _dialogueDB;
    }

    public EventData GetEventData(int id)
    {
        //0은 더미데이터
        if (id == 0)
        {
            return null;
        }

        if (!_eventDB.ContainsKey(id))
        {
            ConsoleLogger.LogError($"{id}번 이벤트는 데이터상 존재하지 않습니다.");
            return null;
        }

        return _eventDB[id];
    }

    public Queue<EventData> GetTutorialEvents()
    {
        Queue<EventData> tutorialEvents = new Queue<EventData>();
        
        foreach (var eventData in _eventDB)
        {
            if (eventData.Value.EventType == EventType.Tutorial)
            {
                tutorialEvents.Enqueue(eventData.Value);
            }
        }

        return tutorialEvents;
    }

//아이템이 DB에 존재하는지 확인
    public bool CheckContainsItem(int itemId)
    {
        return _itemDB.ContainsKey(itemId);
    }

    //단서가 DB에 존재하는지 확인
    public bool CheckContainsClue(int clueId)
    {
        return _clueDB.ContainsKey(clueId);
    }
}