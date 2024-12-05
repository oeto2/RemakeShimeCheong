using UnityEngine;
using UnityEngine.UI;

public enum UIType
{
    Normal,
    Interact
}

public class UIBase : MonoBehaviour
{
    [SerializeField] protected Button _closeButton;
    public UIType _uiType = UIType.Normal; 

    private void Start()
    {
        _closeButton?.onClick.AddListener(CloseUI);
    }

    protected virtual void CloseUI()
    {
        gameObject.SetActive(false);
    }
}