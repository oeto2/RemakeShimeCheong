using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance = null;
    private PlayerEquipment _playerEquipment;
    private Dictionary<int, DialogueData> _dialogueDatas;

    [Header("설정")] public GameObject dialoguePanel;
    public TextMeshProUGUI context;
    public TextMeshProUGUI nameText;
    public float textDelay = 0.1f;

    private WaitForSeconds _textWaitForSeconds;
    private DialogueData _findData; //찾은 다이얼로그 데이터
    private bool _isNext; //다음 대사 출력
    private bool _talkEnd; //대화 종료

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
        _dialogueDatas = new(DBManager.Instance.GetDialogueDatas());
        _playerEquipment = GameManager.Instance.playerObj.GetComponent<PlayerEquipment>();
        _textWaitForSeconds = new WaitForSeconds(textDelay);
    }

    //다이얼로그 보여주기
    public void StartTalk(string speakerName)
    {
        dialoguePanel.SetActive(true);

        //상황에 맞는 대사 찾기
        _findData = FindDialogueData(speakerName);
        
        //대화 시작
        StartCoroutine(TypeWriter(_findData.Comment));

        nameText.text = _findData.Name; //이름 적용
    }

    //대사 출력
    private IEnumerator TypeWriter(string context_)
    {
        context.text = ""; //대사 비우기

        while (_talkEnd)
        {
            for (int i = 0; i < context_.Length; i++)
            {
                context.text += context_[i];
                yield return _textWaitForSeconds; //텍스트 딜레이
            }
        }
        
        yield return null;
    }

    //대사 찾기
    private DialogueData FindDialogueData(string speakerName)
    {
        DialogueData findData; //찾은 대화 데이터

        //플레이어가 장착한 아이템이 있는 경우
        if (_playerEquipment.IsEquip())
        {
            //장착중인 아이템 ID와 같고 대화자 이름이 같은 데이터를 가져옴
            findData = _dialogueDatas.Where(x => x.Value.EquipCondition == _playerEquipment.GetEquipDataID()
                                                 && x.Value.Name == speakerName).Select(x => x.Value).FirstOrDefault();
        }

        //플레이어가 장착한 아이템이 없는 경우
        else
        {
            findData = _dialogueDatas.Where(x => x.Value.EquipCondition == 100
                                                 && x.Value.Name == speakerName).Select(x => x.Value).FirstOrDefault();
        }

        return findData;
    }

    //다음 대사 출력
    public void PrintNextContext()
    {
        _isNext = true;
    }
}