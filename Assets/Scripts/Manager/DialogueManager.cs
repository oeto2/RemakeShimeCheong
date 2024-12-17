using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance = null;
    public GameObject dialoguePanel;
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    //다이얼로그 보여주기
    public void ShowDialogue()
    {
        dialoguePanel.SetActive(true);
    }
}