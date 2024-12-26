using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryTab
{
    Item, //아이템
    Clue //단서
}

public class Inventory : MonoBehaviour
{
    public Dictionary<int, ItemData> InventoryItems = new Dictionary<int, ItemData>(); //보유중인 아이템 목록
    public Dictionary<int, ClueData> InventoryClues = new Dictionary<int, ClueData>();  //보유중인 단서 목록
 
    [Header("Settings")]    
    public GameObject slotsObject;
    public TextMeshProUGUI descriptionText; //설명 텍스트
    public Button itemTab;
    public Button clueTab;
    
    [Header("Info")]
    public InventoryTab curInventoryTab = InventoryTab.Item; //현재 인벤토리 탭
    public List<Slot> slots = new List<Slot>(); //슬롯 모음

    private PlayerEquipment _playerEquipment; //플레이어 장비

    private ToastMessagePopup _toastMessage;
    private void Awake()
    {
        itemTab?.onClick.AddListener(ClickItemTap);
        clueTab?.onClick.AddListener(ClickClueTap);

        _playerEquipment = GameManager.Instance.playerObj.GetComponent<PlayerEquipment>();
        _toastMessage = UIManager.Instance.GetUIComponent<ToastMessagePopup>();

    }

    //아이템 탭 클릭시
    private void ClickItemTap()
    {
        curInventoryTab = InventoryTab.Item;
        ClearDescription(); //설명 창 비우기
        DisableSelectSlot(); //현재 선택된 슬롯 비활성화


        //슬롯의 갯수가 보유중인 아이템 수 보다 적다면, 부족한 만큼 슬롯 생성
        for (int i = slots.Count; i < InventoryItems.Count; i++)
        {
            GameObject slot = ResourceManager.Instance.Instantiate("Slot", slotsObject.transform);
            slots.Add(slot.GetComponent<Slot>());
        }

        int index = 0; //슬롯 인덱스
        
        //보유중인 아이템 갯수 만큼 슬롯 세팅
        foreach (var inventoryItem in InventoryItems)
        {
            slots[index].gameObject.SetActive(true); 
            slots[index].Init(inventoryItem.Value);
            index++;
        }
        
        //남은 슬롯은 비활성화
        for (int i = index; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }
    
    //단서 탭 클릭시
    private void ClickClueTap()
    {
        curInventoryTab = InventoryTab.Clue;
        ClearDescription(); //설명 창 비우기
        DisableSelectSlot(); //현재 선택된 슬롯 비활성화
        
        //슬롯의 갯수가 보유중인 단서 수 보다 적다면, 부족한 만큼 슬롯 생성
        for (int i = slots.Count; i < InventoryClues.Count; i++)
        {
            GameObject slot = ResourceManager.Instance.Instantiate("Slot", slotsObject.transform);
            slots.Add(slot.GetComponent<Slot>());
        }

        int index = 0; //슬롯 인덱스
        
        //보유중인 단서 갯수 만큼 슬롯 세팅
        foreach (var inventoryClue in InventoryClues)
        {
            slots[index].gameObject.SetActive(true); 
            slots[index].Init(inventoryClue.Value);
            index++;
        }
        
        //남은 슬롯은 비활성화
        for (int i = index; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }

    //아이템 획득
    public void GetItem(int itemId)
    {
        //해당 ID의 아이템이 데이터 존재하지 않거나 
        if (!DBManager.Instance.CheckContainsItem(itemId))
        {
            ConsoleLogger.LogWarning($"{itemId}번 아이템을 찾을 수 없습니다.");
            return;
        }
        //이미 보유중이라면
        if (InventoryItems.ContainsKey(itemId))
        {
            ConsoleLogger.LogWarning($"{itemId}번 아이템을 이미 보유하고 있습니다");
            return;
        }
        
        InventoryItems.Add(itemId, DBManager.Instance.GetItemData(itemId)); //아이템 획득

        //현재 아이템 탭이라면
        if (curInventoryTab == InventoryTab.Item)
        {
            GameObject slot = ResourceManager.Instance.Instantiate("Slot", slotsObject.transform); //슬롯 생성
            slots.Add(slot.GetComponent<Slot>()); //슬롯 추가
            slot.GetComponent<Slot>().Init(InventoryItems[itemId]); //슬롯 세팅    
        }
        
        //토스트 메세지
        UIManager.Instance.ShowPopup<ToastMessagePopup>();
        _toastMessage.StartHideMessageAnimation();
        _toastMessage.SetToastMessage(ResourceManager.Instance.Load<Sprite>(InventoryItems[itemId].SpritePath), InventoryItems[itemId].Name,
            $"{InventoryItems[itemId].Name} 획득");
        _toastMessage.StartOnMessageAnimation();
    }
    
    //단서 획득
    public void GetClue(int clueId)
    {
        if (clueId == 0)
        {
            return;
        }
        
        //해당 ID의 단서 데이터 존재하지 않거나
        if (!DBManager.Instance.CheckContainsClue(clueId))
        {
            ConsoleLogger.LogWarning($"{clueId}번 단서를 찾을 수 없습니다.");
            return;
        }
        //이미 보유중이라면
        if (InventoryClues.ContainsKey(clueId))
        {
            return;
        }
        
        InventoryClues.Add(clueId, DBManager.Instance.GetClueData(clueId)); //단서 획득

        //현재 단서 탭이라면
        if (curInventoryTab == InventoryTab.Clue)
        {
            GameObject slot = ResourceManager.Instance.Instantiate("Slot", slotsObject.transform); //슬롯 생성
            slots.Add(slot.GetComponent<Slot>()); //슬롯 추가
            slot.GetComponent<Slot>().Init(InventoryClues[clueId]); //슬롯 세팅    
        }
        
        
        //토스트 메세지
        UIManager.Instance.ShowPopup<ToastMessagePopup>();
        _toastMessage.StartHideMessageAnimation();
        _toastMessage.SetToastMessage(ResourceManager.Instance.Load<Sprite>(InventoryClues[clueId].SpritePath), InventoryClues[clueId].Name,
            $"{InventoryClues[clueId].Name} 획득");
        _toastMessage.StartOnMessageAnimation();
    }
    
    //설명란 텍스트 작성하기
    public void WriteDescription(string description)
    {
        descriptionText.text = description;
    }
    
    //설명란 텍스트 비우기
    public void ClearDescription()
    {
        descriptionText.text = "";
    }
    
    //현재 선택된 슬롯 비활성화
    public void DisableSelectSlot()
    {
        Slot slot = slots.Find(x => x.SelectSlotObj.activeSelf);
        
        if (slot != null)
        {
            slot.SelectSlotObj.SetActive(false);
            slot.isUsing = false;
        }
        
        _playerEquipment.UnEquip(); //장착 해제
    }
    
    //아이템 장착하기
    public void EquipItem(ItemData itemData)
    { 
        _playerEquipment.EquipItem(itemData);
    }
    
    //단서 장착하기
    public void EquipClue(ClueData clueData)
    { 
        _playerEquipment.EquipClue(clueData);
    }
    
    //인벤토리 아이템데이터 가져오기
    public ItemData GetInventoryItemData(int id)
    {
        //id의 아이템을 가지고 있지 않다면
        if (!InventoryItems.ContainsKey(id))
        {
            ConsoleLogger.LogWarning($"{id}의 아이템을 인벤토리에 보유하고 있지 않습니다.");
            return null;
        }

        return InventoryItems[id];
    }
    
    //인벤토리 단서 데이터 가져오기
    public ClueData GetInventoryClueData(int id)
    {
        if (id != 0)
        {
            return null;
        }
        
        //id의 단서을 가지고 있지 않다면
        if (!InventoryClues.ContainsKey(id))
        {
            ConsoleLogger.LogWarning($"{id}의 단서를 인벤토리에 보유하고 있지 않습니다.");
            return null;
        }

        return InventoryClues[id];
    }
    
    //해당 아이템을 보유하고있는지
    public bool ContainsItem(int id)
    {
        if (!InventoryItems.ContainsKey(id))
        {
            return false;
        }

        return true;
    }
    
    //해당 단서를 보유하고있는지
    public bool ContainsClue(int id)
    {
        if (!InventoryClues.ContainsKey(id))
        {
            return false;
        }

        return true;
    }
    
    //해당 아이템 제거
    public void RemoveItem(int id)
    {
        if (!InventoryItems.ContainsKey(id))
        {
            return;
        }

        InventoryItems.Remove(id);
    }
    
    //해당 단서 제거
    public void RemoveClue(int id)
    {
        if (!InventoryClues.ContainsKey(id))
        {
            return;
        }

        InventoryClues.Remove(id);
    }
}

