using Constants;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtons : MonoBehaviour
{
    //타이틀에 사용되는 버튼
    public Button startButton;
    public Button continueButton;
    public Button exitButton;

    private void Awake()
    {
        SettingButtons();
    }

    //버튼 세팅
    private void SettingButtons()
    {
        startButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(UnitySceneName.Main));
        exitButton.onClick.AddListener(GameManager.Instance.ExitGame);
    }
}