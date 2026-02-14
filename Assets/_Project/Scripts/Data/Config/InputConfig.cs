using UnityEngine;

/// <summary>
/// 输入适配层的配置数据。可放置在 Data/Config 下，由 InputAdapter 引用。
/// </summary>
[CreateAssetMenu(fileName = "InputConfig", menuName = "Config/Input", order = 2)]
public class InputConfig : ScriptableObject
{
    [Header("轻量输入处理")]
    [Range(0f, 1f)]
    public float moveDeadZone = 0.15f;
}
