using UnityEngine;

//빌드 시 로그 출력을 관리하기 위한 스크립트
public class ConsoleLogger : MonoBehaviour
{
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void Log(object message_)
    {
        Debug.Log(message_);
    }
 
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void Log(object message, Object context)
    {
        Debug.Log(message, context);
    }
 
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogWarning(object message)
    {
        Debug.LogWarning(message);
    }
 
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogWarning(object message, Object context)
    {
        Debug.LogWarning(message, context);
    }
 
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogError(object message)
    {
        Debug.LogError(message);
    }
 
    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogError(object message, Object context)
    {
        Debug.LogError(message, context);
    }
}
