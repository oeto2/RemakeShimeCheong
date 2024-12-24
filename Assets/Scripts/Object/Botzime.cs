using System;
using UnityEngine;

public class Botzime : MonoBehaviour
{
    //오브젝트 비활성화 시
   private void OnDisable()
   {
       GameManager.Instance.playerObj.GetComponent<PlayerController>().GetBotzime();
       EventManager.Instance.ClearEvent(10010);
       DialogueManager.Instance.StartTalk(7020);
   }
}
