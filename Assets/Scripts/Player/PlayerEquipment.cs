using Constants;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public ObjectType equipType; //장착중인 오브젝트 타입
    [SerializeField] private ItemData equipItemData; //장착중인 아이템
    [SerializeField] private ClueData equipClueData; //장착중인 단서

    public void EquipItem(ItemData itemData)
    {
        equipType = ObjectType.Item;
        equipItemData = itemData;
    }

    public void EquipClue(ClueData clueData)
    {
        equipType = ObjectType.Clue;
        equipClueData = clueData;
    }

    public void UnEquip()
    {
        equipType = ObjectType.None;
        equipItemData = null;
        equipClueData = null;
    }
}
