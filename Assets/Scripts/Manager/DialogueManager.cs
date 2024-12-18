using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance = null;
    private PlayerEquipment _playerEquipment;
    private Dictionary<int, DialogueData> _dialogueDatas;
    
    [Header("설정")]    
    public GameObject dialoguePanel;
    public TextMeshProUGUI context;
    public TextMeshProUGUI nameText;
   
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
        _dialogueDatas = new (DBManager.Instance.GetDialogueDatas());
        _playerEquipment = GameManager.Instance.playerObj.GetComponent<PlayerEquipment>();
    }

    //다이얼로그 보여주기
    public void StartTalk(string speakerName)
    {
        dialoguePanel.SetActive(true);

        var findData = _dialogueDatas.Where(x => x.Value.EquipCondition == 100
                                                 && x.Value.Name == speakerName).Select(x => x.Value).FirstOrDefault();
        context.text = findData.Comment;
        nameText.text = findData.Name;
    }
}