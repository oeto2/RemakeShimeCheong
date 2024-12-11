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
    
    public ItemData(int id, ObjectType objectType, string name, string comment, bool isUsing, int indexNum, string spritePath)
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
    
    public ClueData(int id, ObjectType objectType, string name, string comment, bool isUsing, int indexNum, string spritePath)
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

public class DBManager : Singleton<DBManager>
{
    private GoogleSheetSO _dataSO; //전체 데이터
    private readonly Dictionary<int, ItemData> _itemDB = new Dictionary<int, ItemData>(); //아이템 데이터
    private readonly Dictionary<int, ClueData> _clueDB = new Dictionary<int, ClueData>(); //단서 데이터
   
    private ItemData _itemDataTemp = new ItemData();
    private ClueData _clueDataTemp = new ClueData();

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
            _itemDataTemp = new ItemData(itemTableData.Id, Enum.Parse<ObjectType>(itemTableData.ObjectType), itemTableData.Name,
                itemTableData.Comment,
                itemTableData.IsUsing, itemTableData.IndexNum, itemTableData.SpritePath);
            
            _itemDB.Add(itemTableData.Id, _itemDataTemp);
        }
        
        //단서 DB 데이터 초기화
        foreach (var clueTableData in _dataSO.Clue_TableList)
        {
            _clueDataTemp = new ClueData(clueTableData.Id, Enum.Parse<ObjectType>(clueTableData.ObjectType), clueTableData.Name,
                clueTableData.Comment,
                clueTableData.IsUsing, clueTableData.IndexNum, clueTableData.SpritePath);
            
            _clueDB.Add(clueTableData.Id, _clueDataTemp);
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