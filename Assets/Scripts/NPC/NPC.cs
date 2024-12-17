using UnityEngine;

public class NPC : MonoBehaviour,Iinteractable
{
    //상호작용 시
    public void OnInteract()
    {
        DialogueManager.Instance.ShowDialogue();   
    }

    //플레이어와 접촉 시 
    public void OnPlayerCollision()
    {
        throw new System.NotImplementedException();
    }
}
