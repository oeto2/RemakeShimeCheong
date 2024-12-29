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
    private Inventory _inventory;
    private bool _hasAcquiredItem = false; //중복 획득 방지
    
    //변수 : 세팅 
    [Header("Setting")] 
    public int itemId = 0; //획득할 Item ID
    [SerializeField] private bool isHide = false; //획득 시 오브젝트를 숨길 것인지

    private void Start()
    {
        _inventory = UIManager.Instance.GetPopupObject<InventoryPopup>().GetComponent<Inventory>();
    }

    //상호작용 시
    public void OnInteract()
    {
        //한번도 아이템을 획득한 적이 없다면
        if (!_hasAcquiredItem)
        {
            //아이템 획득
            _inventory.GetItem(itemId);
            _hasAcquiredItem = true;

            //오브젝트 비활성화
            if (isHide)
                gameObject.SetActive(false);
        }
    }

    //플레이어와 닿았을 경우
    public void OnPlayerCollision()
    {
    }
}