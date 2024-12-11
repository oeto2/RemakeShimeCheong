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
            SelectSlotObj.SetActive(true);
            isUsing = true;

            switch (_inventory.curInventoryTab)
            {
                case InventoryTab.Item :
                _inventory.WriteDescription(itemData.Comment);
                break;
                
                case InventoryTab.Clue :
                    _inventory.WriteDescription(clueData.Comment);
                    break;
            }
        }

        //선택중일 경우
        else
        {
            SelectSlotObj.SetActive(false);
            isUsing = false;
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
