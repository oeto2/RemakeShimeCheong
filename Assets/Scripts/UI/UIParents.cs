using UnityEngine;

public class UIParents : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.parentsUI = transform; //UI 부모 지정
        UIManager.Instance.ShowPopup<PlayPopup>(); //플레이 팝업 띄우기

    }
}
