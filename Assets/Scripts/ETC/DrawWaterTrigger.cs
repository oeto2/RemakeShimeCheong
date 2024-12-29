using Constants;
using UnityEngine;

public class DrawWaterTrigger : MonoBehaviour, Iinteractable
{
    private Inventory _inventory;

    private void Start()
    {
        _inventory = UIManager.Instance.GetPopupObject<InventoryPopup>().GetComponent<Inventory>();
    }
    
    public void OnInteract()
    {
        //바가지를 보유중이라면
        if (_inventory.ContainsItem(1003))
        {
            DialogueManager.Instance.StartTalk(7070); //대화 시작
            _inventory.GetItem(1004); //물이 든 바가지 획득
            _inventory.RemoveItem(1003); //바가지 아이템 제거
        }
    }

    public void OnPlayerCollision()
    {
        throw new System.NotImplementedException();
    }
}
