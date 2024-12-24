using UnityEngine;

public class Botzime : MonoBehaviour
{
   //오브젝트 비활성화 시
   private void OnDisable()
   {
       DialogueManager.Instance.StartTalk(7020);
       Navigation.Instance.NextNavigation();
   }
}
