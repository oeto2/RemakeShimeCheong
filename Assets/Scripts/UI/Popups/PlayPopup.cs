using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayPopup : UIBase
{
    public Image equipItemImage;
    public TextMeshProUGUI equipItemText;


    //아이템 장착 UI 새로고침
    public void SetEquipItemUI(string spritePath, string itemName)
    {
        equipItemImage.sprite = ResourceManager.Instance.Load<Sprite>(spritePath);
        equipItemText.text = itemName;
    }
}
