using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : UIBase
{
    [Header("Buttons")]
    [SerializeField] private Button _countinueButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _exitButton;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameManager.Instance.playerObj.GetComponent<PlayerController>();
        _closeButton.onClick.AddListener(CloseUI);
    }

    protected override void CloseUI()
    {
        base.CloseUI();
        GameManager.Instance.ReleasePauseGame(); //게임 일시 정지 해제
        _playerController.ReleaseIgnoreInput(); //플레이어 입력 무시 해제
    }
}
