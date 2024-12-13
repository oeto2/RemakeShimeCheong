using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessagePopup : UIBase
{
    public Image itemImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI mainText;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    //토스트 메세지 세팅하기
    public void SetToastMessage(Sprite itemImage_, string itemName, string mainText_)
    {
        itemImage.sprite = itemImage_;
        itemNameText.text = itemName;
        mainText.text = mainText_;
    }
    
    //이벤트 : 애니메이션 종료시
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
