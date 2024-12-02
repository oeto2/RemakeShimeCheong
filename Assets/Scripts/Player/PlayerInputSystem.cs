using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private PlayerInput _playerInput;
    
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
    public void OnInteration(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ConsoleLogger.Log("상호작용 버튼 입력");
        }
    }
}
