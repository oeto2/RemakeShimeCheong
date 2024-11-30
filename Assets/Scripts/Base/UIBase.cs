using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected Button _closeButton;

    private void Start()
    {
        _closeButton?.onClick.AddListener(CloseUI);
    }

    protected virtual void CloseUI()
    {
        gameObject.SetActive(false);
    }
}