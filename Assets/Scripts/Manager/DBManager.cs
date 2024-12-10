using System;
using System.Collections.Generic;
using Constants;

public class ItemData
{
    public int Id { get; set; }
    public ObjectType ObjectType { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public bool IsUsing { get; set; }
    public int IndexNum { get; set; }

    public ItemData()
    {
        
    }
    
    public ItemData(int id, ObjectType objectType, string name, string comment, bool isUsing, int indexNum)
    {
        Id = id;
        ObjectType = objectType;
        Name = name;
        Comment = comment;
        IsUsing = isUsing;
        IndexNum = indexNum;
    }
}


public class DBManager : Singleton<DBManager>
{
    private GoogleSheetSO _dataSO;
    private readonly Dictionary<int, ItemData> _itemDB = new Dictionary<int, ItemData>();
   
    private ItemData _itemDataTemp = new ItemData();

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
                itemTableData.IsUsing, itemTableData.IndexNum);
            
            _itemDB.Add(itemTableData.Id, _itemDataTemp);
        }
    }

    public ItemData GetItemData(int itemId)
    {
        return _itemDB[itemId];
    }
}