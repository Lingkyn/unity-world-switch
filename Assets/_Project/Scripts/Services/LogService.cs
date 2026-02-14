using UnityEngine;

/// <summary>
/// 日志服务：封装 Debug.Log / 自定义日志等级 / 发布版本控制。
/// TODO: 实现日志等级过滤、文件输出、发布时关闭等。
/// </summary>
public class LogService : MonoBehaviour
{
    public void Log(string message) => Debug.Log(message);
    public void LogWarning(string message) => Debug.LogWarning(message);
    public void LogError(string message) => Debug.LogError(message);
}
