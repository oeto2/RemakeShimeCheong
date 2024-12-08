using UnityEngine;
using UnityEngine.UI;

public enum UIType
{
    Normal,
    Interact
}

public class UIBase : MonoBehaviour
{
    [SerializeField] protected Button _closeButton;
    public UIType _uiType = UIType.Normal; 
    private PlayerController _playerController;

    protected virtual void Start()
    {
        _playerController = GameManager.Instance.playerObj.GetComponent<PlayerController>();
        _closeButton?.onClick.AddListener(CloseUI);
    }

    protected virtual void CloseUI()
    {
        gameObject.SetActive(false);
        GameManager.Instance.ReleasePauseGame(); //게임 일시 정지 해제
        _playerController.ReleaseIgnoreInput(); //플레이어 입력 무시 해제
        Cursor.visible = false; //커서 숨기기
    }
}