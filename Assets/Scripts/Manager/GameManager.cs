
//게임 매니저
using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

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