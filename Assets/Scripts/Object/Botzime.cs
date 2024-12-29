using System;
using UnityEngine;

public class Botzime : MonoBehaviour
{
    private BoxCollider2D _collider2D;
    
    private void Awake()
    {
        _collider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        EventManager.Instance.StartGetBotzimeEvent += EnableGetBotzime;
    }
    
    //오브젝트 비활성화 시
   private void OnDisable()
   {
       GameManager.Instance.playerObj.GetComponent<PlayerController>().GetBotzime();
       EventManager.Instance.ClearEvent(10010);
       DialogueManager.Instance.StartTalk(7020);
       EventManager.Instance.StartGetBotzimeEvent -= EnableGetBotzime;
   }
   
   
   //봇짐 획득 가능
   private void EnableGetBotzime()
   {
       _collider2D.isTrigger = true;
   }
}
