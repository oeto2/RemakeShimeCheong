//게임 내 사용되는 변하지 않는 상수
namespace Constants
{
    //게임에 사용되는 씬 이름
    public enum UnitySceneName
    {
        Title,
        Main
    }
    
    //게임내 사용되는 오브젝트 타입
    public enum ObjectType
    {
        Item, //아이템
        Clue, //단서
        Hypothesis, //가설
        None
    }
    
    //대화시 사용되는 화자 타입
    public enum SpeakerType
    {
        Npc,
        Player
    }
    
    //이벤트 타입
    public enum EventType
    {
        Tutorial,
        Main
    }
    
    //게임에 사용되는 동적로딩 이미지 경로
    public static class ResourceImagePath
    {
        public const string Cursor_Idle = "Sprites/CurSor/Brush_Idle"; //브러시 기본
        public const string Cursor_Click = "Sprites/CurSor/Brush_Click"; //브러시 클릭
    }
    
    //게임에 사용되는 오브젝트 레이어 이름
    public class ObjectLayer
    {
        public const int PortalLayer = 6;
        public const int ItemLayer = 7;
        public const int InteractableObjectLayer = 8;
        public const int NPCLayer = 9;
    }
}