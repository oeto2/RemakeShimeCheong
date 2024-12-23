using System;
using UnityEngine;

public class DeungJan : MonoBehaviour, Iinteractable
{
    private Animator _animator;

    private string LightOnParamaterName = "LightOn";
    private int LightOnParamaterHash;
    public GameObject lightObject;

    private bool isLightOn; //등잔불을 켰는지

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        LightOnParamaterHash = Animator.StringToHash(LightOnParamaterName);
    }

    //상호작용 시 
    public void OnInteract()
    {
        if (!isLightOn)
        {
            _animator.SetBool(LightOnParamaterHash, true); //애니메이션 시작
            lightObject.SetActive(true); //불빛 켜기
            isLightOn = true;
            return;
        }
        
        _animator.SetBool(LightOnParamaterHash, false); //애니메이션 종료
        lightObject.SetActive(false); //불빛 끄기
        isLightOn = false;
    }
    
    //플레이어랑 접촉 시 
    public void OnPlayerCollision()
    {
    }
}
