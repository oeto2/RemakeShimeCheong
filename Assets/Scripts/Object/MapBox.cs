using System;
using UnityEngine;

public class MapBox : MonoBehaviour, Iinteractable
{
    private SpriteRenderer _spriteRenderer;
    private Inventory _inventory;
    private ToastMessagePopup _toastMessage;
    
    
    [Header("Setting")] public int itemId; //획득할 아이템 id
    public Sprite closeBoxSprite; //닫힌 상자 스프라이트
    public Sprite openBoxSprite; //열린 상자 스프라이트
    public Sprite openMapBoxSprite; //지도가 있는 열린 상자 스프라이트
    
    
    [Header("Info")] [SerializeField] private bool isOpen; //상자가 열렸는지
    [SerializeField] private bool hasAcquiredMap; //지도를 획득했는지
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _inventory = UIManager.Instance.GetPopupObject<InventoryPopup>().GetComponent<Inventory>();
        _toastMessage = UIManager.Instance.GetUIComponent<ToastMessagePopup>();
    }

    //상호작용 시 
    public void OnInteract()
    {
        //상자가 지도를 포함한 상태로 열려있다면
        if (_spriteRenderer.sprite == openMapBoxSprite)
        {
            isOpen = true;
            _spriteRenderer.sprite = openBoxSprite;
            
            
            //아이템 획득
            _inventory.GetItem(itemId);
            ItemData itemData = _inventory.GetInventoryItemData(itemId);

            //토스트 메세지
            UIManager.Instance.ShowPopup<ToastMessagePopup>();
            _toastMessage.StartHideMessageAnimation();
            _toastMessage.SetToastMessage(ResourceManager.Instance.Load<Sprite>(itemData.SpritePath), itemData.Name,
                $"{itemData.Name} 획득");
            _toastMessage.StartOnMessageAnimation();
            
            //중복 획득 방지
            hasAcquiredMap = true;
            
            return;
        }
        
        //상자가 지도가 없는 상태로 열려있다면
        if (_spriteRenderer.sprite == openBoxSprite)
        {
            _spriteRenderer.sprite = closeBoxSprite;
            isOpen = false;
            return;
        }
        
        
        //상자가 닫혀있고 지도를 아직 획득하지 못했다면
        if (!isOpen && !hasAcquiredMap)
        {
            isOpen = true;
            _spriteRenderer.sprite = openMapBoxSprite;
            return;
        }
        
        //상자가 닫혀있고 지도를 획득 했다면
        if (!isOpen && hasAcquiredMap)
        {
            isOpen = true;
            _spriteRenderer.sprite = openBoxSprite;
        }
    }

    //플레이어와 접촉시
    public void OnPlayerCollision()
    {
        
    }
}
