using System;
using UnityEngine;

//플레이어 이동 관련
public class PlayerMovement : MonoBehaviour
{
    //컴포넌트
    private Rigidbody2D _rigid2D;
    public SpriteRenderer playerSpriteRen;

    //변수 : 이동관련
    private Vector2 _playerVelocity = Vector2.zero; //플레이어가 이동할 방향
    public float moveSpeed = 10f; //플레이어 이동속도
    public float playerMoveRangeMax; //플레이어의 최대 이동 범위
    public float playerMoveRangeMin; //플레이어의 최소 이동 범위
    public float rangeOffset = 0.25f; //플레이어의 이동 반경 offset

    private void Awake()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //플레이어 이동 범위 제한 설정
        SetPlayerMoveRange(MapManager.Instance.GetPlayerCurrentSpaceSpriteRenderer());
    }

    private void FixedUpdate()
    {
        Move(); //이동
    }

    //플레이어 이동 로직
    public void Move()
    {
        float playerPositionX = transform.position.x;
        
        //좌측 끝 이동 범위 제한
        if (playerPositionX >= playerMoveRangeMax && _playerVelocity.x > 0)
        {
            _playerVelocity = Vector2.zero;
            return;
        }
        
        //우측 끝 이동 범위 제한
        if (playerPositionX <= playerMoveRangeMin && _playerVelocity.x < 0)
        {
            _playerVelocity = Vector2.zero;
            return;
        }
        
        //플레이어 이동 : 이동할 방향 * 속도
        _rigid2D.velocity = _playerVelocity * moveSpeed;
    }
    
    //플레이어 이동 방향 적용 (PlayerInput)
    public void ApplyMoveVelocity(Vector2 Vec2_)
    {
        FlipPlayerSprite(Vec2_.x); //스프라이트 이미지 뒤집기
        _playerVelocity = Vec2_; //플레이어가 이동할 방향
    }
    
    //x입력 값에 따른 플레이어 이미지 뒤집기
    public void FlipPlayerSprite(float x_)
    {
        if(x_ == 0) return;
        
        playerSpriteRen.flipX = x_ < 0;
    }
    
    //플레이어의 이동 반경 설정
    public void SetPlayerMoveRange(SpriteRenderer bgSpriteRen)
    {
        playerMoveRangeMin = bgSpriteRen.bounds.min.x + playerSpriteRen.bounds.size.x - rangeOffset;
        playerMoveRangeMax = bgSpriteRen.bounds.max.x - playerSpriteRen.bounds.size.x + rangeOffset;;
    }
}
