using System.Collections;
using UnityEngine;
using Input = UnityEngine.Input;

public class NotePopup : UIBase
{
    private PlayerController _playerController;
    private Inventory _inventory;
    private bool _canClose;

    private void Awake()
    {
        _playerController = GameManager.Instance.playerObj.GetComponent<PlayerController>();
    }

    private void Start()
    {
        _inventory = UIManager.Instance.GetPopupObject<InventoryPopup>().GetComponent<Inventory>();
        StartCoroutine(SetUnActiveDealy());
    }
    
    private void OnEnable()
    {
        _playerController.IgnoreInput();
    }

    private void OnDisable()
    {
        _inventory.GetClue(2000); //향리댁 수양딸 단서 획득
        _playerController.ReleaseIgnoreInput();
    }


    private void Update()
    {
        if (Input.anyKeyDown && _canClose)
        {
            gameObject.SetActive(false);
            DialogueManager.Instance.StartTalk(7010);
        }
    }

    private IEnumerator SetUnActiveDealy()
    {
       yield return new WaitForSeconds(1f);
       _canClose = true;
    }
}
