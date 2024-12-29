using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public static Navigation Instance = null;
    
    public TextMeshProUGUI navigationText;

    public Queue<EventData> TutorialEvents = new Queue<EventData>(); //튜토리얼 목록

    private EventData _curActiveTutorial; //현재 진행중인 튜토리얼
    private PlayerController _playerController;

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
        _playerController = GameManager.Instance.playerObj.GetComponent<PlayerController>();
        TutorialEvents = DBManager.Instance.GetTutorialEvents();
        EventManager.Instance.ActiveEvent(TutorialEvents.Peek());
        DialogueManager.Instance.DialogueStartEvent += HideNavigationText;
        DialogueManager.Instance.DialogueEndEvent += ShowNavigationText;
        DialogueManager.Instance.DialogueEndEvent += TutorialIgnoreInput;
    }

    private void OnDisable()
    {
        //1회성 이벤트들 구독 해제
        DialogueManager.Instance.DialogueStartEvent -= HideNavigationText;
        DialogueManager.Instance.DialogueEndEvent -= ShowNavigationText;
        DialogueManager.Instance.DialogueEndEvent -= TutorialIgnoreInput;
    }

    //다음 네비게이션 시작
    public void NextNavigation()
    {
        _curActiveTutorial = TutorialEvents.Dequeue();
        navigationText.text = _curActiveTutorial.Comment;
    }
    
    //플레이어 입력제한
    private void TutorialIgnoreInput()
    {
        //현재 진행중인 튜토리얼 따른 입력제한
        switch (_curActiveTutorial.Id)
        {
            //등잔불 켜기
            case 10000:
                _playerController.IgnoreInput("Inventory");
                _playerController.IgnoreInput("Portal");
                _playerController.IgnoreInput("Map");
                break;
            
            //봇짐 챙기기
            case 10010:
                _playerController.IgnoreInput("Inventory");
                _playerController.IgnoreInput("Portal");
                _playerController.IgnoreInput("Map");
                break;
            
            //지도 챙기기
            case 10020:
                _playerController.IgnoreInput("Map");
                _playerController.IgnoreInput("Portal");
                break;
        }
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