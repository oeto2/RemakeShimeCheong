using UnityEngine;
using UnityEngine.UI;

//상호 작용가능한 인터페이스
public interface Iinteractable
{
    public void OnInteract();
    public void OnPlayerCollision();
}

public class ItemObject : MonoBehaviour, Iinteractable
{
    public int itemId = 0; //획득할 Item ID
    private Inventory _inventory;
    private ToastMessagePopup _toastMessage;

    private void Start()
    {
        _inventory = UIManager.Instance.GetPopupObject<InventoryPopup>().GetComponent<Inventory>();
        _toastMessage = UIManager.Instance.GetUIComponent<ToastMessagePopup>();
    }
  
    //상호작용 시
    public void OnInteract()
    {
        _inventory.GetItem(itemId); //아이템 획득
        ItemData itemData = _inventory.GetInventoryItemData(itemId);
        UIManager.Instance.ShowPopup<ToastMessagePopup>(); //토스트 메세지 띄우기
        _toastMessage.SetToastMessage(ResourceManager.Instance.Load<Sprite>(itemData.SpritePath), itemData.Name, 
            $"{itemData.Name} 획득"); //토스트 메세지 설정
        gameObject.SetActive(false);
    }

    //플레이어와 닿았을 경우
    public void OnPlayerCollision()
    {
        
    }
}
