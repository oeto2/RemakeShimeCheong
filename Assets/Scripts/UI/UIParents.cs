using UnityEngine;

public class UIParents : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.parentsUI = transform; //UI 부모 지정
    }
}
