using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance = null;
    private PlayerEquipment _playerEquipment;
    private PlayerController _playerController;
    private Inventory _inventory;
    private Dictionary<int, DialogueData> _dialogueDatas;

    [Header("설정")] public GameObject dialoguePanel;
    public TextMeshProUGUI context;
    public TextMeshProUGUI nameText;
    public float textDelay = 0.1f;
    public Image portrait_Left;
    public Image portrait_Right;
    public Sprite[] portraits;

    private WaitForSeconds _textWaitForSeconds;

    private Queue<DialogueData> _talkList = new Queue<DialogueData>(); //대화 목록
    private DialogueData _findData; //찾은 다이얼로그 데이터
    private bool _isNext; //다음 대사 출력
    private bool _talkEnd; //대화 종료

    //변수 : 대화관련
    private DialogueData _tempDialogueData;
    private bool printAllcontext; //모든 대사 출력하기
    private StringBuilder _contextSb = new StringBuilder(); //대사 출력용 스트링빌더
    private string _replaceText; //컬러코드 적용 후 텍스트
    private int _getClueID; //획득할 단서 ID
    private bool isClueTalk; //단서 대화를 하고 있는지


    //변수 : 초상화 관련
    private Color32 darkPortraitColor = new Color32(90, 90, 90, 255);
    private Color32 originPortraitColor = new Color32(255, 255, 255, 255);

    //변수 : 이벤트
    public event Action DialogueStartEvent;
    public event Action DialogueEndEvent;

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
        _playerController = GameManager.Instance.playerObj.GetComponent<PlayerController>();
        _inventory = UIManager.Instance.GetPopupObject<InventoryPopup>().GetComponent<Inventory>();
        _textWaitForSeconds = new WaitForSeconds(textDelay);
    }

    //이름으로 대화 코루틴 호출
    public void StartTalk(string speakerName)
    {
        ChangeDialogueSpeakerName(speakerName); 
        StartCoroutine(StartTalkCoroutine(FindDialogueData(speakerName)));
    }

    //ID로 대화 코루틴 호출
    public void StartTalk(int dialogueId)
    {
        StartCoroutine(StartTalkCoroutine(_dialogueDatas[dialogueId]));
    }

    //다이얼로그 보여주기
    private IEnumerator StartTalkCoroutine(DialogueData dialogueData)
    {
        if (dialogueData == null)
        {
            ConsoleLogger.LogError("대화 데이터를 찾을 수 없습니다!");
        }

        //착용하고 있는 아이템에 따라 대사 추가
        if (_playerEquipment.IsEquip())
        {
            AddClueOrItemSentence();
        }

        //대사 넣기
        for (int i = 0; i < dialogueData.NextCommentNum + 1; i++)
        {
            _talkList.Enqueue(_dialogueDatas[dialogueData.Id + i]);
        }

        //입력 제한
        _playerController.TalkIgnoreInput();

        //모든 대사를 출력할때까지 반복
        while (_talkList.Count > 0)
        {
            //대화 시작
            StartCoroutine(TypeWriter());

            //다음 대사 출력 신호 전까지 대기
            while (!_isNext)
            {
                yield return null;
            }

            isClueTalk = false;
        }

        EndTalk(); //대화 종료
        EventManager.Instance.ClearEvent(_tempDialogueData.EndEventID); //이벤트 클리어
        yield return null;
    }

    //대사 출력
    private IEnumerator TypeWriter()
    {
        CallDialogueStartEvent(); //대화 시작 후 이벤트 호출

        context.text = ""; //대사 비우기
        printAllcontext = false; //대사 모두 출력 off
        _isNext = false; //다음대사 출력 off
        
        _tempDialogueData = _talkList.Dequeue(); //이번에 출력할 대사 뽑기
        ChangePortrait(_tempDialogueData.Name); //인물 초상화 변경
        
        _replaceText = GetReplaceColorMarkText(_tempDialogueData.Comment);
        dialoguePanel.SetActive(true);
        ChangeDialogueSpeakerName(_tempDialogueData.Name); //대화자 이름 변경
        _inventory.GetClue(_tempDialogueData.RewardID); //단서 획득하기

        EventManager.Instance.ActiveEvent(DBManager.Instance.GetEventData(_tempDialogueData.StartEventID)); //이벤트 시작
        
        //한 글자씩 대사 출력
        for (int i = 0; i < _tempDialogueData.Comment.Length; i++)
        {
            //대사 전부 출력 기능
            if (printAllcontext)
            {
                context.text = _replaceText;
                break;
            }

            //컬러 기호가 있는지 체크하기
            switch (_tempDialogueData.Comment[i])
            {
                default:
                    _contextSb.Append(_tempDialogueData.Comment[i]);
                    break;

                case 'ⓦ':
                    _contextSb.Append("</b>").Append("<color=#ffffff>");
                    break;

                case 'ⓡ':
                    _contextSb.Append("<color=#850000>").Append("<b>");
                    break;
            }

            context.text = _contextSb.ToString();
            yield return _textWaitForSeconds; //텍스트 딜레이
        }

        _contextSb.Clear();
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

            //만약 찾는 데이터가 없을 경우 기본 대사
            if (findData == null)
            {
                findData = _dialogueDatas.Where(x => x.Value.EquipCondition == 100
                                                     && x.Value.Name == speakerName).Select(x => x.Value)
                    .FirstOrDefault();
            }

            //단서 대화일 경우
            else
            {
                isClueTalk = true;
            }
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
        //대사가 모두 출력되었다면
        if (context.text == _replaceText)
        {
            _isNext = true; //다음대사 출력
        }

        //대사가 모두 출력되지 않았다면
        else
        {
            printAllcontext = true; //모든대사 출력하기
        }
    }

    //다음 대사 출력 멈추기
    public void StopNextContext()
    {
        _isNext = false;
    }

    //대화 종료
    public void EndTalk()
    {
        dialoguePanel.SetActive(false); //대화창 끄기
        _playerController.ReleaseTalkIgnoreInput(); //입력무시 해제
        CallDialogueEndEvent(); //대화 종료 후 이벤트 호출
        isClueTalk = false; //단서 대화중인지 확인하는 flag off
    }

    //현재 진행중인 대화 데이터 얻기
    public DialogueData GetTalkData()
    {
        if (_findData == null)
        {
            ConsoleLogger.LogError("현재 대화중인 데이터를 찾을 수 없습니다");
            return null;
        }

        return _findData;
    }


    //컬러코드 변환 대사값 반환
    private string GetReplaceColorMarkText(string text_)
    {
        _replaceText = text_;
        _replaceText = _replaceText.Replace("ⓦ", "</b><color=#ffffff>");
        _replaceText = _replaceText.Replace("ⓡ", "<color=#850000><b>");

        return _replaceText;
    }

    //인물 초상화 변경하기
    private void ChangePortrait(string name_)
    {
        switch (name_)
        {
            case "뺑덕 어멈":
                portrait_Right.sprite = portraits[0];
                portrait_Right.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                portrait_Right.rectTransform.sizeDelta = new Vector2(1000, 1000);
                DarkenLeftPortrait(); //왼쪽 초상화 어둡게
                ResetRightPortraitColor(); //오른쪽 초상화 색 복구
                break;

            case "거지":
                portrait_Right.sprite = portraits[1];
                portrait_Right.rectTransform.anchoredPosition = new Vector3(-43, -132, 0);
                portrait_Right.rectTransform.sizeDelta = new Vector2(700, 800);
                DarkenLeftPortrait(); //왼쪽 초상화 어둡게
                ResetRightPortraitColor(); //오른쪽 초상화 색 복구
                break;

            case "승려":
                portrait_Right.sprite = portraits[2];
                portrait_Right.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                portrait_Right.rectTransform.sizeDelta = new Vector2(850, 1000);
                DarkenLeftPortrait(); //왼쪽 초상화 어둡게
                ResetRightPortraitColor(); //오른쪽 초상화 색 복구
                break;

            case "귀덕 어멈":
                portrait_Right.sprite = portraits[3];
                portrait_Right.rectTransform.anchoredPosition = new Vector3(153, -21, 0);
                portrait_Right.rectTransform.sizeDelta = new Vector2(1100, 950);
                DarkenLeftPortrait(); //왼쪽 초상화 어둡게
                ResetRightPortraitColor(); //오른쪽 초상화 색 복구
                break;

            case "장사꾼":
                portrait_Right.sprite = portraits[4];
                portrait_Right.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                portrait_Right.rectTransform.sizeDelta = new Vector2(950, 950);
                DarkenLeftPortrait(); //왼쪽 초상화 어둡게
                ResetRightPortraitColor(); //오른쪽 초상화 색 복구
                break;

            case "향리 댁 부인":
                portrait_Right.sprite = portraits[5];
                portrait_Right.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                portrait_Right.rectTransform.sizeDelta = new Vector2(1000, 1000);
                DarkenLeftPortrait(); //왼쪽 초상화 어둡게
                ResetRightPortraitColor(); //오른쪽 초상화 색 복구
                break;

            case "뱃사공":
                portrait_Right.sprite = portraits[6];
                portrait_Right.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                portrait_Right.rectTransform.sizeDelta = new Vector2(780, 780);
                DarkenLeftPortrait(); //왼쪽 초상화 어둡게
                ResetRightPortraitColor(); //오른쪽 초상화 색 복구
                break;

            case "심청":
                portrait_Right.sprite = portraits[7];
                portrait_Right.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                portrait_Right.rectTransform.sizeDelta = new Vector2(750, 950);
                DarkenLeftPortrait(); //왼쪽 초상화 어둡게
                ResetRightPortraitColor(); //오른쪽 초상화 색 복구
                break;

            case "송나라 상인":
                portrait_Right.sprite = portraits[8];
                portrait_Right.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                portrait_Right.rectTransform.sizeDelta = new Vector2(850, 1100);
                DarkenLeftPortrait(); //왼쪽 초상화 어둡게
                ResetRightPortraitColor(); //오른쪽 초상화 색 복구
                break;

            case "장지언":
                portrait_Right.sprite = portraits[9];
                portrait_Right.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                portrait_Right.rectTransform.sizeDelta = new Vector2(750, 950);
                DarkenLeftPortrait(); //왼쪽 초상화 어둡게
                ResetRightPortraitColor(); //오른쪽 초상화 색 복구
                break;

            case "심학규":
                portrait_Left.sprite = portraits[10];
                portrait_Left.rectTransform.sizeDelta = new Vector2(1000, 1000);
                DarkenRightPortrait(); //오른쪽 초상화 어둡게
                ResetLeftPortraitColor(); //왼쪽 초상화 색 복구
                break;

            case "독백":
                portrait_Left.sprite = portraits[10];
                portrait_Left.rectTransform.sizeDelta = new Vector2(1000, 1000);
                portrait_Right.gameObject.SetActive(false); // 오른쪽 초상화 비활성화
                ResetLeftPortraitColor(); //왼쪽 초상화 색 복구
                break;
        }

        //만약 첫 번째 단서 대화라면
        if (isClueTalk)
        {
            //심학규 초상화만 밝게
            ResetLeftPortraitColor();
            DarkenRightPortrait();
        }
    }

    //왼쪽 초상화 어둡게 설정
    private void DarkenLeftPortrait()
    {
        portrait_Left.gameObject.SetActive(true);
        portrait_Left.color = darkPortraitColor;
    }

    //오른쪽 초상화 어둡게 설정
    private void DarkenRightPortrait()
    {
        portrait_Right.gameObject.SetActive(true);
        portrait_Right.color = darkPortraitColor;
    }

    //왼쪽 초상화 색 복구
    private void ResetLeftPortraitColor()
    {
        portrait_Left.gameObject.SetActive(true);
        portrait_Left.color = originPortraitColor;
    }

    //오른쪽 초상화 색 복구
    private void ResetRightPortraitColor()
    {
        portrait_Right.gameObject.SetActive(true);
        portrait_Right.color = originPortraitColor;
    }

    //이벤트 호출
    private void CallDialogueStartEvent()
    {
        DialogueStartEvent?.Invoke();
    }

    //이벤트 호출
    private void CallDialogueEndEvent()
    {
        DialogueEndEvent?.Invoke();
    }

    //다이얼로그 이름 변경
    private void ChangeDialogueSpeakerName(string speakerName)
    {
        if (speakerName == "독백")
        {
            nameText.text = "심학규";
            return;
        }

        nameText.text = speakerName;
    }

    //아이템, 단서 대사 넣기
    private void AddClueOrItemSentence()
    {
        switch (_playerEquipment.GetEquipDataID())
        {
            //주먹밥
            case 1005:
                _talkList.Enqueue(_dialogueDatas[8006]);
                break;
            
            //비녀
            case 1006:
                _talkList.Enqueue(_dialogueDatas[8007]);
                break;
            
            //먹
            case 1007:
                _talkList.Enqueue(_dialogueDatas[8008]);
                break;
            
            //보리떡
            case 1008:
                _talkList.Enqueue(_dialogueDatas[8009]);
                break;
            
            //꽃
            case 1009:
                _talkList.Enqueue(_dialogueDatas[8010]);
                break;
            
            //약초
            case 1010:
                _talkList.Enqueue(_dialogueDatas[8011]);
                break;
            
            //사공의 물건
            case 1011:
                _talkList.Enqueue(_dialogueDatas[8012]);
                break;
            
            //향리 댁의 수양딸
            case 2000:
                _talkList.Enqueue(_dialogueDatas[8013]);
                break;
            
            //청이의 거짓말
            case 2001:
                _talkList.Enqueue(_dialogueDatas[8014]);
                break;
            
            //청이의 행방
            case 2002:
                _talkList.Enqueue(_dialogueDatas[8015]);
                break;
            
            //청이와 장터
            case 2003:
                _talkList.Enqueue(_dialogueDatas[8016]);
                break;
            
            //청이와 사내
            case 2004:
                _talkList.Enqueue(_dialogueDatas[8017]);
                break;
            
            //누군가의 아들
            case 2005:
                _talkList.Enqueue(_dialogueDatas[8018]);
                break;
            
            //송나라 상인과 청이
            case 2006:
                _talkList.Enqueue(_dialogueDatas[8019]);
                break;
            
            //승려와 청이
            case 2007:
                _talkList.Enqueue(_dialogueDatas[8020]);
                break;
            
            //승려의 마음
            case 2008:
                _talkList.Enqueue(_dialogueDatas[8021]);
                break;
            
            //청이의 도움
            case 2009:
                _talkList.Enqueue(_dialogueDatas[8022]);
                break;
            
            //공양미 삼백석
            case 2010:
                _talkList.Enqueue(_dialogueDatas[8023]);
                break;
            
            //공양미 출처
            case 2011:
                _talkList.Enqueue(_dialogueDatas[8024]);
                break;
            
            //청이의 거래
            case 2012:
                _talkList.Enqueue(_dialogueDatas[8025]);
                break;
            
            //향리 댁의 셋째 아들
            case 2013:
                _talkList.Enqueue(_dialogueDatas[8026]);
                break;
            
            //잠잠해져야 할 물살
            case 2014:
                _talkList.Enqueue(_dialogueDatas[8027]);
                break;
            
            //청이가 사간것
            case 2015:
                _talkList.Enqueue(_dialogueDatas[8028]);
                break;
            
            //짚신을 사간 청이
            case 2016:
                _talkList.Enqueue(_dialogueDatas[8029]);
                break;
            
            //배의 출항
            case 2017:
                _talkList.Enqueue(_dialogueDatas[8030]);
                break;
            
            //노점의 단골
            case 2018:
                _talkList.Enqueue(_dialogueDatas[8031]);
                break;
            
            //뜨지않는 배
            case 2019:
                _talkList.Enqueue(_dialogueDatas[8032]);
                break;
            
            //사공에게 있었던 일
            case 2020:
                _talkList.Enqueue(_dialogueDatas[8033]);
                break;
            
            //사공의 물건
            case 2021:
                _talkList.Enqueue(_dialogueDatas[8034]);
                break;
            
            //뱃길을 잠재울 방법
            case 2022:
                _talkList.Enqueue(_dialogueDatas[8035]);
                break;
            
            //3월 보름날
            case 2023:
                _talkList.Enqueue(_dialogueDatas[8036]);
                break;
            
            //공양미를 구한 방법
            case 4023:
                _talkList.Enqueue(_dialogueDatas[8037]);
                break;
            
            //청이가 사라진 날
            case 4015:
                _talkList.Enqueue(_dialogueDatas[8038]);
                break;
            
            //청이와 그의 관계
            case 4017:
                _talkList.Enqueue(_dialogueDatas[8039]);
                break;
            
            //함께 사라진 두 사람
            case 8032:
                _talkList.Enqueue(_dialogueDatas[8040]);
                break;
            
            //무역의 중단
            case 4033:
                _talkList.Enqueue(_dialogueDatas[8041]);
                break;
            
            //청의 가출
            case 9000:
                _talkList.Enqueue(_dialogueDatas[8042]);
                break;
            
            //바다의 바쳐질 제물
            case 6045:
                _talkList.Enqueue(_dialogueDatas[8043]);
                break;
        }
    }
}