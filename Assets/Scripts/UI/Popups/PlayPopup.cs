using System.Collections.Generic;
using Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayPopup : UIBase
{
    [Header("설정")]
    public Image equipItemImage;
    public TextMeshProUGUI equipItemText;
    public GameObject equipSlot;
    public GameObject eventTextBox;
    public GameObject timePanel;

    private Dictionary<string, GameObject> eventTexts = new Dictionary<string, GameObject>();

    //아이템 장착 UI 새로고침
    public void SetEquipItemUI(string spritePath, string itemName)
    {
        equipItemImage.sprite = ResourceManager.Instance.Load<Sprite>(spritePath);
        equipItemText.text = itemName;
    }
    
    //요소들 보여주기
    public void ShowElements()
    {
        equipSlot.SetActive(true);
        eventTextBox.SetActive(true);
    }
    
    //시간 Box보여주기
    public void ShowTimeBox()
    {
        timePanel.SetActive(true);
    }
    
    //이벤트 텍스트 추가
    public void AddEventText(string eventName)
    {
        eventTexts.Add(eventName, ResourceManager.Instance.Instantiate(ResourcePrefabPath.EventTextBox, eventTextBox.transform));
        eventTexts[eventName].GetComponent<TextMeshProUGUI>().text = $"- {eventName}";
    }
    
    //이벤트 텍스트 제거
    public void DeleteEventText(string eventName)
    {
        if (!eventTexts.ContainsKey(eventName))
            return;
        
        Destroy(eventTexts[eventName]);
        eventTexts.Remove(eventName);
    }
}
