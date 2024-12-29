using UnityEngine;

public class TutorialEvent : MonoBehaviour
{
    private PlayerController _playerController;
    private void Start()
    {
        _playerController = GameManager.Instance.playerObj.GetComponent<PlayerController>();
        UIManager.Instance.ShowPopup<PlayPopup>(); //플레이 팝업 띄우기
        DialogueManager.Instance.StartTalk(7000); //독백 시작
    }
}
