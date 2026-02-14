using UnityEngine;

/// <summary>
/// 时间服务：封装 Time.time 等，便于测试时 Mock、暂停时间等。
/// TODO: 若需暂停/倍速/回放，在此实现。
/// </summary>
public class TimeService : MonoBehaviour
{
    public float TimeNow => Time.time;
    public float DeltaTime => Time.deltaTime;
    public float UnscaledDeltaTime => Time.unscaledDeltaTime;
}
