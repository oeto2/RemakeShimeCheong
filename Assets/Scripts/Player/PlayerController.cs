using System;
using Cinemachine;
using Constants;
using UnityEngine;
using UnityEngine.InputSystem;

//플레이어 조작
public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private PlayerAnimator _playerAnimator;
    private readonly PlayerAnimationData _playerAnimationData = new PlayerAnimationData();    
    
    //변수 : 포탈 관련
    public CinemachineConfiner2D cinemachineConfiner2D; //카메라 영역 제한
    private GameObject _usePortal;
    private IPortalable _portalScr;
    
    
    private void Awake()
    {
        Init();
    }

    //초기 설정
    private void Init()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAnimationData.Init(); //게임에 사용될 애니메이션 파라미터 해쉬 변환
    }

    //플레이어와 콜라이더 충돌 처리
    private void OnTriggerEnter2D(Collider2D other)
    {
        //포탈
        if (other.gameObject.layer == ObjectLayer.PortalLayer)
        {
            _usePortal = other.gameObject; //사용할 포탈 등록
        }
    }

    //플레이어와 콜라이더 충돌 해제
    private void OnTriggerExit2D(Collider2D other)
    {
        //포탈
        if (other.gameObject.layer == ObjectLayer.PortalLayer)
        {
            _usePortal = null; //사용할 포탈 해제
        }
    }

    //플레이어 이동키 입력
    public void OnMove(InputAction.CallbackContext context)
    {
        //입력 시작
        if (context.started)
        {
            _playerMovement.ApplyMoveVelocity(context.ReadValue<Vector2>()); //플레이어 이동
            _playerAnimator.StartAnimation(_playerAnimationData.MoveParameterHash); //이동 애니메이션 시작
        }

        //입력 종료
        if (context.canceled)
        {
            _playerMovement.ApplyMoveVelocity(Vector2.zero); //플레이어 이동
            _playerAnimator.StopAnimation(_playerAnimationData.MoveParameterHash); //이동 애니메이션 정지
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
            if (_usePortal != null)
            {
                if (_usePortal.TryGetComponent(out _portalScr))
                {
                    transform.position = _portalScr.GetDestination(); //플레이어 포탈 이동 
                    cinemachineConfiner2D.m_BoundingShape2D = _portalScr.GetDestinationCollider(); //카메라 범위 제한
                }
                else
                {
                    ConsoleLogger.LogWarning("이동할 포탈의 인터페이스가 존재하지 않습니다.");
                }
            }
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