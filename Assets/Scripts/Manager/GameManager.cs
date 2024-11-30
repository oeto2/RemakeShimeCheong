
//게임 매니저

using System;
using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    //커서 세팅 관련
    public Texture2D cursorTexture_Idle;
    public Texture2D cursorTexture_Click; 
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    
    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SettingCursor(); //커서 세팅
        ChangeCursor(); //커서 변경
    }

    //커서 세팅하기
    public void SettingCursor()
    {
        cursorTexture_Idle = ResourceManager.Instance.Load<Texture2D>(ResourceImagePath.Cursor_Idle);
        cursorTexture_Click = ResourceManager.Instance.Load<Texture2D>(ResourceImagePath.Cursor_Click);
    }
    
    //커서 변경하기
    public void ChangeCursor()
    {
        ConsoleLogger.Log("커서 변경완료");
        Cursor.SetCursor(cursorTexture_Idle, hotSpot, cursorMode); //커서 변경
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
}