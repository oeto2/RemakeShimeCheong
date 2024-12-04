using UnityEngine;

interface IPortalable
{
    public Vector3 GetDestination(); //포탈 이동 목적지 구하기
    public PolygonCollider2D GetDestinationCollider(); //목적지의 제한범위 콜라이더 구하기
}

public class Portal : MonoBehaviour, IPortalable
{
    public Transform destination; //목적지    
    public PolygonCollider2D destinationCollider; //목적지 콜라이더
    
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
}
