using Constants;
using UnityEngine;

public class NPC : MonoBehaviour, Iinteractable
{
    [Header("설정")] public string speakerName;

    //상호작용 시
    public void OnInteract()
    {
        DialogueManager.Instance.StartCoroutine("StartTalk", speakerName);
    }

    //플레이어와 접촉 시 
    public void OnPlayerCollision()
    {
        throw new System.NotImplementedException();
    }
}