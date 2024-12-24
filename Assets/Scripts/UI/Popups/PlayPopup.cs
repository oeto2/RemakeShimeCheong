using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayPopup : UIBase
{
    [Header("설정")]
    public Image equipItemImage;
    public TextMeshProUGUI equipItemText;
    public GameObject equipSlot;
    public GameObject eventTextBox;
    public GameObject timePanel;


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
}
