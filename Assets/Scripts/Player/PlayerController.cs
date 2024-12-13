using System.Linq;
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
    private bool _canUsePortal = true; //포탈 사용가능 여부    


    //변수 : 이동관련
    private Vector2 _playerMoveDirection; //플레이어의 이동 방향

    //변수 : 오브젝트 감지 관련
    private RaycastHit2D[] _raycastHit2Ds = new RaycastHit2D[10];

    //변수 : 상호작용 관련
    private Iinteractable _interactObject; //상호 작용 가능한 오브젝트

    private void Awake()
    {
        Init();
        GameManager.Instance.Init();
        GameManager.Instance.playerObj = gameObject;
        // Cursor.visible = false; //커서 숨기기
    }

    //초기 설정
    private void Init()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerInput = GetComponent<PlayerInput>();
        _playerAnimationData.Init(); //게임에 사용될 애니메이션 파라미터 해쉬 변환
    }

    //플레이어와 콜라이더 충돌 처리
    private void OnTriggerEnter2D(Collider2D other)
    {
        //충돌한 물체가 아이템이라면
        if (other.gameObject.layer == ObjectLayer.ItemLayer)
        {
            _interactObject = other.gameObject.GetComponent<Iinteractable>();
        }
        
        //충돌한 물체가 상호작용이 가능한 오브젝트라면
        if (other.gameObject.layer == ObjectLayer.InteractableObjectLayer)
        {
            _interactObject = other.gameObject.GetComponent<Iinteractable>();
        }
    }

    //플레이어와 콜라이더 충돌 해제
    private void OnTriggerExit2D(Collider2D other)
    {
        //충돌 해제한 물체가 아이템이라면
        if (other.gameObject.layer == ObjectLayer.ItemLayer)
        {
            _interactObject = null;
        }
        
        //충돌한 물체가 상호작용이 가능한 오브젝트라면
        if (other.gameObject.layer == ObjectLayer.InteractableObjectLayer)
        {
            _interactObject = null;
        }
    }

    //플레이어 이동키 입력
    public void OnMove(InputAction.CallbackContext context)
    {
        //입력 시작
        if (context.started)
        {
            _playerMoveDirection = context.ReadValue<Vector2>();
            _playerMovement.ApplyMoveVelocity(_playerMoveDirection); //플레이어 이동
            _playerAnimator.StartAnimation(_playerAnimationData.MoveParameterHash); //이동 애니메이션 시작
        }

        //입력 종료
        if (context.canceled)
        {
            _playerMoveDirection = Vector2.zero;
            _playerMovement.ApplyMoveVelocity(_playerMoveDirection); //플레이어 이동
            _playerAnimator.StopAnimation(_playerAnimationData.MoveParameterHash); //이동 애니메이션 정지
        }
    }

    //지도 단축키 입력
    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //지도 팝업이 활성화 중이라면
            if (UIManager.Instance.IsActiveUI<MapPopup>())
            {
                UIManager.Instance.ClosePopup<MapPopup>();
                Cursor.visible = false; //커서 숨기기
                return;
            }

            //상호작용 팝업중에 하나라도 활성화 중이라면
            if (UIManager.Instance.IsActiveInteractPopup())
            {
                UIManager.Instance.CloseAllInteractPopups(); //모든 팝업 종료
            }
            UIManager.Instance.ShowPopup<MapPopup>();
            Cursor.visible = true; //커서 보이기
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
            //인벤토리 팝업이 활성화 중이라면
            if (UIManager.Instance.IsActiveUI<InventoryPopup>())
            {
                UIManager.Instance.ClosePopup<InventoryPopup>();
                Cursor.visible = false; //커서 숨기기
                return;
            }

            //상호작용 팝업중에 하나라도 활성화 중이라면
            if (UIManager.Instance.IsActiveInteractPopup())
            {
                UIManager.Instance.CloseAllInteractPopups(); //모든 팝업 종료
            }
            UIManager.Instance.ShowPopup<InventoryPopup>();
            Cursor.visible = true; //커서 보이기
        }
    }

    //포탈 단축키 입력
    public void OnPortal(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //포탈을 사용할 수 있는 상태라면
            if (_canUsePortal)
            {
                //사용할 포탈 미등록 시
                if (_usePortal == null)
                {
                    DetectPortal(); //레이캐스트로 포탈 검출 후 사용할 포탈에 등록
                }

                //사용할 포탈 등록 시
                if (_usePortal != null)
                {
                    //포탈 이동
                    if (_usePortal.TryGetComponent(out _portalScr))
                    {
                        UIManager.Instance.ShowPopup<BlinkScreenUI>(); //스크린 깜빡이기 (애니메이션)
                        transform.position = _portalScr.GetDestination(); //플레이어 포탈 이동 
                        cinemachineConfiner2D.m_BoundingShape2D = _portalScr.GetDestinationCollider(); //카메라 범위 제한
                        MapManager.Instance.SetPlayerCurrentSpace(_portalScr.GetDestinationPlaceName()); //이동 장소 설정
                        _playerMovement.SetPlayerMoveRange(MapManager.Instance
                            .GetPlayerCurrentSpaceSpriteRenderer()); //플레이어의 이동 범위 제한 설정
                        _playerMovement.ApplyMoveVelocity(_playerMoveDirection); //이동 중이 였다면 계속 이동
                        FollowPlayer(); //카메라 플레이어 추적
                        UIManager.Instance.GetUIComponent<MapPopup>()
                            .SetPinPos(_portalScr.GetDestinationPlaceName()); //지도 핀 위치 변경
                        _canUsePortal = false; //포탈 이용 막기
                    }
                }
            }
        }

        if (context.canceled)
        {
            cinemachineConfiner2D.m_Damping = 0.5f;
            ReleasePortal(); //사용할 포탈 비우기
        }
    }

    //옵션 키 입력
    public void OnOption(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //상호작용 팝업중에 하나라도 활성화 중이라면
            if (UIManager.Instance.IsActiveInteractPopup())
            {
                UIManager.Instance.CloseAllInteractPopups(); //모든 팝업 종료
                GameManager.Instance.ReleasePauseGame(); //게임 일시정지 해제
                ReleaseIgnoreInput(); //Input 무시 해제
                Cursor.visible = false; //커서 숨기기
                return;
            }

            UIManager.Instance.ShowPopup<OptionPopup>();
            IgnoreInput(); //Input 무시
            Cursor.visible = true; //커서 보이기
        }
    }

    //상호작용 키 입력
    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //상호작용 가능한 오브젝트가 존재할 경우
            if (_interactObject != null)
            {
                _interactObject.OnInteract(); //상호작용하기
            }
        }
    }

    //입력 무시
    public void IgnoreInput()
    {
        _playerInput.actions["Interaction"].Disable();
        _playerInput.actions["Inventory"].Disable();
        _playerInput.actions["Talk"].Disable();
        _playerInput.actions["Portal"].Disable();
        _playerInput.actions["Move"].Disable();
        _playerInput.actions["Map"].Disable();
    }

    //입력 무시 해제
    public void ReleaseIgnoreInput()
    {
        _playerInput.actions["Interaction"].Enable();
        _playerInput.actions["Inventory"].Enable();
        _playerInput.actions["Talk"].Enable();
        _playerInput.actions["Portal"].Enable();
        _playerInput.actions["Move"].Enable();
        _playerInput.actions["Map"].Enable();
    }

    //포탈 검출하기
    private void DetectPortal()
    {
        //레이캐스트를 활용하여 검출된 콜라이더들 가져오기
        Physics2D.RaycastNonAlloc(transform.position, Vector2.up, _raycastHit2Ds, 1f);

        // 디버그용 드로우 레이
        Debug.DrawRay(transform.position, Vector2.up * 1f, Color.red);
        RaycastHit2D portalHit = _raycastHit2Ds.FirstOrDefault(hit =>
            hit.collider != null && hit.collider.gameObject.layer == ObjectLayer.PortalLayer);

        if (portalHit.collider != null)
        {
            _usePortal = portalHit.collider.gameObject; //사용할 포탈에 등록
        }
    }

    //사용 포탈 해제하기
    private void ReleasePortal()
    {
        _raycastHit2Ds = new RaycastHit2D[10];
        _usePortal = null;
    }

    //포탈을 사용할 수 있게한 함수
    public void EnablePortalUse()
    {
        _canUsePortal = true;
    }

    //카메라 플레이어 추적
    private void FollowPlayer()
    {
        cinemachineConfiner2D.m_Damping = 0f;
        cinemachineConfiner2D.transform.position = transform.position;
    }
}