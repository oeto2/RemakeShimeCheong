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

    private void Awake()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move(); //이동
    }

    //플레이어 이동 로직
    public void Move()
    {
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
}
