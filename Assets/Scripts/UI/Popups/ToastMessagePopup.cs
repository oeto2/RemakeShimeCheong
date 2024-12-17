using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessagePopup : UIBase
{
    public Image itemImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI mainText;
    private Animator _animator;
    
    //애니메이션 파라미터 
    private string _onMessageParameterName = "OnMessage";
    private string _hideMessageParameterName = "HideMessage" ;

    private int _onMessageParameterHash;
    private int _hideMessageParameterHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _onMessageParameterHash = Animator.StringToHash(_onMessageParameterName);
        _hideMessageParameterHash = Animator.StringToHash(_hideMessageParameterName);
    }
    
    //애니메이션 : 토스트 메세지 보여주기
    public void StartOnMessageAnimation()
    {
        _animator.SetTrigger(_onMessageParameterHash);
    }

    //애니메이션 : 토스트 메세지 숨기기
    public void StartHideMessageAnimation()
    {
        _animator.SetTrigger(_hideMessageParameterHash);
    }
    
    //토스트 메세지 세팅하기
    public void SetToastMessage(Sprite itemImage_, string itemName, string mainText_)
    {
        itemImage.sprite = itemImage_;
        itemNameText.text = itemName;
        mainText.text = mainText_;
    }
}
