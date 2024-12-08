using UnityEngine;

public class BlinkScreenUI : UIBase
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    //애니메이션 이벤트 : 게임오브젝트 비활성화
    public void DisableGameObjet()
    {
        gameObject.SetActive(false);
    }
}
