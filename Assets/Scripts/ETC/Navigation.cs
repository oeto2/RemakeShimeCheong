using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public TextMeshProUGUI navigationText;

    public Queue<EventData> TutorialEvents = new Queue<EventData>(); //튜토리얼 목록

    private void Start()
    {
        TutorialEvents = DBManager.Instance.GetTutorialEvents();
        navigationText.text = TutorialEvents.Dequeue().Comment;
    }
    
    //다음 네비게이션 시작
    public void NextNavigation()
    {
        navigationText.text = TutorialEvents.Dequeue().Comment;
    }
}