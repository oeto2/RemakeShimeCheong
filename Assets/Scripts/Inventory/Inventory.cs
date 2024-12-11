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
    //보유중인 아이템 목록
    public Dictionary<int, ItemData> InventoryItems = new Dictionary<int, ItemData>(); 
    //보유중인 단서 목록
    public Dictionary<int, ClueData> InventoryClues = new Dictionary<int, ClueData>(); 
 
    
    public GameObject slots;
    public TextMeshProUGUI descriptionText; //설명 텍스트
    public InventoryTab curInventoryTab = InventoryTab.Item; //현재 인벤토리 탭

    public Button itemTab;
    public Button clueTab;

    private void Awake()
    {
        itemTab?.onClick.AddListener(ClickItemTap);
        clueTab?.onClick.AddListener(ClickClueTap);
    }
        
    //아이템 탭 클릭시
    private void ClickItemTap()
    {
        curInventoryTab = InventoryTab.Item;
    }
    
    //단서 탭 클릭시
    private void ClickClueTap()
    {
        curInventoryTab = InventoryTab.Clue;
        GetClue(2000);
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
            GameObject slot = ResourceManager.Instance.Instantiate("Slot", slots.transform); //슬롯 생성
            slot.GetComponent<Slot>().Init(InventoryItems[itemId]); //슬롯 세팅    
        }
    }
    
    //단서 획득
    public void GetClue(int clueId)
    {
        //해당 ID의 단서 데이터 존재하지 않거나
        if (!DBManager.Instance.CheckContainsClue(clueId))
        {
            ConsoleLogger.LogWarning($"{clueId}번 단서를 찾을 수 없습니다.");
            return;
        }
        //이미 보유중이라면
        if (InventoryClues.ContainsKey(clueId))
        {
            ConsoleLogger.LogWarning($"{clueId}번 단서를 이미 보유하고 있습니다");
            return;
        }
        
        InventoryClues.Add(clueId, DBManager.Instance.GetClueData(clueId)); //단서 획득

        //현재 단서 탭이라면
        if (curInventoryTab == InventoryTab.Clue)
        {
            GameObject slot = ResourceManager.Instance.Instantiate("Slot", slots.transform); //슬롯 생성
            slot.GetComponent<Slot>().Init(InventoryClues[clueId]); //슬롯 세팅    
        }
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
}

