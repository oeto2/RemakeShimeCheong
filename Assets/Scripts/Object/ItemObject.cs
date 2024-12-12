using UnityEngine;

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

    private void Start()
    {
        _inventory = UIManager.Instance.GetPopupObject<InventoryPopup>().GetComponent<Inventory>();
    }
  
    public void OnInteract()
    {
        _inventory.GetItem(itemId); //아이템 획득
        gameObject.SetActive(false);
    }

    //플레이어와 닿았을 경우
    public void OnPlayerCollision()
    {
        
    }
}
