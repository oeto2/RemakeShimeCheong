using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //색을 변경할 텍스트
    private TextMeshProUGUI _text;

    //기본 색깔
    public Color32 color32_Origin = new Color32(255, 255, 255, 255);

    //변경할 색깔
    public Color32 color_Change = new Color32(100, 100, 100, 255);

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Text 색 변경
        _text.color = color_Change;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Text 색 변경
        _text.color = color32_Origin;
    }
}