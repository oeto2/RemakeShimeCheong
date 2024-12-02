using UnityEngine;
using UnityEngine.InputSystem;

//플레이어 조작
public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    //플레이어 이동키 입력
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //플레이어 이동 방향 설정
            _playerMovement.ApplyMoveVelocity(context.ReadValue<Vector2>());
        }

        if (context.canceled)
        {
            //플레이어 이동 정지
            _playerMovement.ApplyMoveVelocity(Vector2.zero);
        }
    }

    //지도 단축키 입력
    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ConsoleLogger.Log("지도 버튼 입력");
        }
    }

    //대화 단축키 입력
    public void OnTalk(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ConsoleLogger.Log("대화 버튼 입력");
        }
    }

    //인벤토리 단축키 입력
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ConsoleLogger.Log("인벤토리 버튼 입력");
        }
    }

    //포탈 단축키 입력
    public void OnPortal(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ConsoleLogger.Log("포탈 버튼 입력");
        }
    }

    //옵션 키 입력
    public void OnOption(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ConsoleLogger.Log("옵션 버튼 입력");
        }
    }

    //상호작용 키 입력
    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ConsoleLogger.Log("상호작용 버튼 입력");
        }
    }
}