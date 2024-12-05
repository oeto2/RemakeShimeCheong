using UnityEngine;
public class MapPopup : UIBase
{
    public RectTransform mapPin; //지도에 표시할 핀 오브젝트 RectTransform
    
    public Vector2[] pinPositions; //지도에 표시할 핀 오브젝트 좌표목록

    //핀 위치 새롭게 설정하기
    public void SetPinPos(PlaceName placeName)
    {
        mapPin.localPosition = pinPositions[(int)placeName];
    }
}
