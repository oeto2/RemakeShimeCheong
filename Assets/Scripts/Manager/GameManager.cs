//게임 매니저
using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //커서 세팅 관련
    public Texture2D cursorTexture_Idle;
    public Texture2D cursorTexture_Click;
    private CursorMode cursorMode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;
    
    //플레이어 오브젝트
    public GameObject playerObj;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        //마우스 상태에 따른 커서 이미지 변경
        if (Input.GetMouseButtonDown(0))
        {
            ChangeCursor(cursorTexture_Click);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ChangeCursor(cursorTexture_Idle);
        }
    }

    public override void Init()
    {
        base.Init();
        SettingCursor(); //커서 세팅
        ChangeCursor(cursorTexture_Idle); //기본 커서 변경
    }

    //커서 세팅하기
    public void SettingCursor()
    {
        cursorTexture_Idle = ResourceManager.Instance.Load<Texture2D>(ResourceImagePath.Cursor_Idle);
        cursorTexture_Click = ResourceManager.Instance.Load<Texture2D>(ResourceImagePath.Cursor_Click);
    }

    //커서 변경하기
    public void ChangeCursor(Texture2D cursorTexture_)
    {
        Cursor.SetCursor(cursorTexture_, hotSpot, cursorMode); //커서 변경
    }

    //씬 전환하기
    public void ChangeScene(UnitySceneName sceneName_)
    {
        SceneManager.LoadScene(sceneName_.ToString());
    }

    //게임 종료
    public void ExitGame()
    {
        ConsoleLogger.Log("게임 종료");
        Application.Quit();
    }
    
    //게임 일시정지
    public void PauseGame()
    {
        ConsoleLogger.Log("게임 일시 정지");
        Time.timeScale = 0f;
    }
    
    //게임 일시 정지 해제
    public void ReleasePauseGame()
    {
        ConsoleLogger.Log("게임 일시정지 해제");
        Time.timeScale = 1f;
    }
}