using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("SlotInfo")]
    public bool isUsing = false; //현재 슬롯이 사용중인지
    public TextMeshProUGUI objectNameText;
    public Image objectImage;
    public GameObject SelectSlotObj;

    [Header("ObjectInfo")]
    [SerializeField] public ItemData itemData;
    [SerializeField] public ClueData clueData;
    
    private Button _slotButton;
    private Inventory _inventory;
    
    private void Awake()
    {
        _slotButton = GetComponent<Button>();
        _inventory = UIManager.Instance.GetPopupObject<InventoryPopup>().GetComponent<Inventory>();
    }

    private void Start()
    {
        _slotButton?.onClick.AddListener(SlotButtonClick);
    }

    //슬롯 버튼 클릭 시
    public void SlotButtonClick()
    {
        //선택중이 아닐 경우
        if (!isUsing)
        {
            _inventory.DisableSelectSlot(); //현재 선택중인 슬롯 비활성화
            
            SelectSlotObj.SetActive(true);
            isUsing = true;

            switch (_inventory.curInventoryTab)
            {
                case InventoryTab.Item :
                _inventory.WriteDescription(itemData.Comment);
                _inventory.EquipItem(itemData); //아이템 장착
                break;
                
                
                case InventoryTab.Clue :
                    _inventory.WriteDescription(clueData.Comment);
                    _inventory.EquipClue(clueData); //단서 장착
                    break;
            }
        }

        //선택중일 경우
        else
        {
            _inventory.DisableSelectSlot();
            _inventory.ClearDescription();
        }
    }

    public void Init(ItemData itemData_)
    {
        itemData = itemData_;
        objectNameText.text = itemData.Name;
        objectImage.sprite = ResourceManager.Instance.Load<Sprite>(itemData.SpritePath);
    }
    
    public void Init(ClueData clueData_)
    {
        clueData = clueData_;
        objectNameText.text = clueData.Name;
        objectImage.sprite = ResourceManager.Instance.Load<Sprite>(clueData.SpritePath);
    }
}
