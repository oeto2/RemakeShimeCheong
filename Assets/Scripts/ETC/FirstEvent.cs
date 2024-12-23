using UnityEngine;

public class FirstEvent : MonoBehaviour
{
    private PlayerController _playerController;
    private void Start()
    {
        _playerController = GameManager.Instance.playerObj.GetComponent<PlayerController>();
        _playerController.IgnoreInput(); //플레이어 입력제한
    }

    private void Update()
    {
        //아무키나 입력할 경우
        if (Input.anyKeyDown)
        {
            _playerController.ReleaseIgnoreInput(); //플레이어 입력제한 해제
            UIManager.Instance.ShowPopup<PlayPopup>(); //플레이 팝업 띄우기
            gameObject.SetActive(false);
        }
    }
}
