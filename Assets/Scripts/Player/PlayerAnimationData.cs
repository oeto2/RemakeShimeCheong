using System;
using UnityEngine;

//플레이어 애니메이션 데이터
[Serializable]
public class PlayerAnimationData
{
    //변수 : 애니메이션 관련 파라미터
    private string _moveParameterName = "Move";

    public int MoveParameterHash { get; private set; }

    public void Init()
    {
        //해쉬값 변환
        MoveParameterHash = Animator.StringToHash(_moveParameterName); 
    }
}
