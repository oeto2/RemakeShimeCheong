using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public static Navigation Instance = null;
    
    public TextMeshProUGUI navigationText;

    public Queue<EventData> TutorialEvents = new Queue<EventData>(); //튜토리얼 목록

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

    private void Start()
    {
        TutorialEvents = DBManager.Instance.GetTutorialEvents();
        EventManager.Instance.ActiveEvent(TutorialEvents.Peek());
        DialogueManager.Instance.DialogueStartEvent += HideNavigationText;
        DialogueManager.Instance.DialogueEndEvent += ShowNavigationText;
    }

    //다음 네비게이션 시작
    public void NextNavigation()
    {
        navigationText.text = TutorialEvents.Dequeue().Comment;
    }
    
    //글자 숨기기
    public void HideNavigationText()
    {
        navigationText.gameObject.SetActive(false);
    }
    
    //글자 보여주기
    public void ShowNavigationText()
    {
        navigationText.gameObject.SetActive(true);
    }
}