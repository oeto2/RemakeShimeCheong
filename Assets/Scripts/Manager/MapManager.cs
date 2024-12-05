using UnityEngine;

public enum PlaceName
{
    BedRoom,
    Kitchen,
    Madang,
    Town,
    Market,
    Brook,
    Ocean
}

public class MapManager : MonoBehaviour
{
    public static MapManager Instance = null;

    public PlaceName currentPlace = PlaceName.BedRoom; //현재 플레이어의 지역

    [Header("Map Sprites")] 
    public SpriteRenderer[] mapSpriteRenderers; //맵 스프라이트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        //중복 방지 (싱글톤)
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    
    //플레이어 현재 장소 설정
    public void SetPlayerCurrentSpace(PlaceName placeName)
    {
        currentPlace = placeName;
    }
    
    //플레이어의 현재 장소 SpriteRenderer 반환
    public SpriteRenderer GetPlayerCurrentSpaceSpriteRenderer()
    {
        return mapSpriteRenderers[(int)currentPlace];
    }
}
