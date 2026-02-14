using UnityEngine;

/// <summary>
/// 影子 2D 移动的配置数据。可放置在 Data/Config 下，由 ShadowMovement2D 引用。
/// </summary>
[CreateAssetMenu(fileName = "ShadowMovementConfig", menuName = "Config/Shadow Movement", order = 1)]
public class ShadowMovementConfig : ScriptableObject
{
    public float speed = 5f;
    public float fixedZ = 0f;
}
