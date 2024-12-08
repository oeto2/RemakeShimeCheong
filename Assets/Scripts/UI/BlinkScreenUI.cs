using UnityEngine;

public class BlinkScreenUI : UIBase
{
    private Animator _animator;
    private PlayerController _playerController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GameManager.Instance.playerObj.GetComponent<PlayerController>();
    }

    //애니메이션 이벤트 : 게임오브젝트 비활성화
    public void DisableGameObjet()
    {
        _playerController.EnablePortalUse(); // 포탈 다시 이용 가능
        gameObject.SetActive(false);
    }
}
