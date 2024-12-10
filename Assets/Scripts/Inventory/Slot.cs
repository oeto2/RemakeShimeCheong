using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("SlotInfo")]
    public bool isUsing = false; //현재 슬롯이 사용중인지
    public TextMeshProUGUI slotNameText;
    public Image slotImage;
    public GameObject SelectSlotObj;

    private Button _slotButton;

    private void Awake()
    {
        _slotButton = GetComponent<Button>();
    }

    private void Start()
    {
        _slotButton?.onClick.AddListener(SlotButtonClick);
    }

    //슬롯 버튼 클릭 시
    public void SlotButtonClick()
    {
        if (!isUsing)
        {
            SelectSlotObj.SetActive(true);
            isUsing = true;
        }

        else
        {
            SelectSlotObj.SetActive(false);
            isUsing = false;
        }
    }
}
