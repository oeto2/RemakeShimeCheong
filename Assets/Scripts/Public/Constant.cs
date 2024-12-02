

//게임 내 사용되는 변하지 않는 상수
namespace Constants
{
    //게임에 사용되는 씬 이름
    public enum UnitySceneName
    {
        Title,
        Main
    }
    
    //게임에 사용되는 동적로딩 이미지 경로
    public static class ResourceImagePath
    {
        public const string Cursor_Idle = "Sprites/CurSor/Brush_Idle"; //브러시 기본
        public const string Cursor_Click = "Sprites/CurSor/Brush_Click"; //브러시 클릭
    }
}