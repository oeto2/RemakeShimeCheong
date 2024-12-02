using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    protected static bool _isLoad = true;
   
    //어플리케이션 종료 플래그
    private static bool _applicationIsQuitting = false;

    private void OnDestroy()
    {
        _applicationIsQuitting = true;
    }

    public static T Instance
    {
        get
        {
            //어플리케이션이 종료중이면 동작하지않음.
            if(_applicationIsQuitting)
            {
                return null;
            }

            if (_instance == null)
            {
                //오브젝트 생성 이름 지정
                string typeName = typeof(T).Name;
                GameObject obj = new GameObject(typeName);

                _instance = obj.AddComponent<T>();

                //isLoad가 true일때만 로드함
                if (_isLoad)
                    DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public virtual void Init()
    {
        ConsoleLogger.Log(transform.name + "is Init");
    }
}