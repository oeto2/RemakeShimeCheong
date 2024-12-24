using Constants;
using UnityEngine;

public class TalkToHyang : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == ObjectLayer.PlayerLayer)
        {
            EventManager.Instance.ClearEvent(10030); //포탈 이동 클리어
            DialogueManager.Instance.StartTalk(7050);
            UIManager.Instance.GetUIComponent<PlayPopup>().ShowElements();
            gameObject.SetActive(false);
        }
    }
}
