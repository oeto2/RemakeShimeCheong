using System;
using Constants;
using UnityEditor;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public ObjectType equipType; //장착중인 오브젝트 타입
    [SerializeField] private ItemData equipItemData; //장착중인 아이템
    [SerializeField] private ClueData equipClueData; //장착중인 단서

    private PlayPopup _playPopup; //플레이 팝업
    
    private void Start()
    {
        _playPopup = UIManager.Instance.GetUIComponent<PlayPopup>();
    }

    public void EquipItem(ItemData itemData)
    {
        equipType = ObjectType.Item;
        equipItemData = itemData;
        
        _playPopup.SetEquipItemUI(itemData.SpritePath, itemData.Name);
    }

    public void EquipClue(ClueData clueData)
    {
        equipType = ObjectType.Clue;
        equipClueData = clueData;
        
        _playPopup.SetEquipItemUI(clueData.SpritePath, clueData.Name);
    }

    public void UnEquip()
    {
        equipType = ObjectType.None;
        equipItemData = null;
        equipClueData = null;
    }

    //장착한 아이템,단서의 ID값 얻기
    public int GetEquipDataID()
    {
        if (equipClueData == null && equipItemData == null)
        {
            ConsoleLogger.LogWarning("장착한 아이템이 존재하지 않습니다");
            return 0;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        switch (equipType)
        {
            case ObjectType.Item :
                return equipItemData.Id;
                break;
            
            case ObjectType.Clue :
                return equipClueData.Id;
                break;
        }
        
        ConsoleLogger.LogWarning("장착한 아이템 ID를 불러오는데 실패했습니다");
        return 0;
    }
}
