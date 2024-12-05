using UnityEngine;

interface IPortalable
{
    public Vector3 GetDestination(); //포탈 이동 목적지 구하기
    public PolygonCollider2D GetDestinationCollider(); //목적지의 제한범위 콜라이더 구하기
    public PlaceName GetDestinationPlaceName(); //목적지의 장소 이름 구하기
}

public class Portal : MonoBehaviour, IPortalable
{
    public Transform destination; //목적지    
    public PolygonCollider2D destinationCollider; //목적지 콜라이더
    public PlaceName destinationPlaceName; //이동할 목적지 장소 이름
    
    
    //이동할 목적지 반환
    public Vector3 GetDestination()
    {
        return destination.position;
    }

    //이동할 목적지의 제한 범위 콜라이더 반환
    public PolygonCollider2D GetDestinationCollider()
    {
        if(destinationCollider == null)
            ConsoleLogger.LogWarning("이동할 목적지의 카메라 범위 제한 콜라이더가 없습니다.");
        
        return destinationCollider;
    }
    
    //이동할 목적지의 제한 범위 Vector[] 반환
    public Vector2[] GetDestinationVectorArray()
    {
        return destinationCollider.points;
    }
    
    //이동할 목적지의 장소 enum 반환
    public PlaceName GetDestinationPlaceName()
    {
        return destinationPlaceName;
    }
}
