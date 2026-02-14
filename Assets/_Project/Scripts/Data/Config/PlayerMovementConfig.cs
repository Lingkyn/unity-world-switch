using UnityEngine;

/// <summary>
/// 玩家 3D 移动与跳跃的配置数据。可放置在 Data/Config 下，由 PlayerMovement 引用。
/// </summary>
[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Config/Player Movement", order = 0)]
public class PlayerMovementConfig : ScriptableObject
{
    [Header("移动参数")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float gravity = -20f;
    [Range(0f, 1f)]
    public float airControl = 0.4f;

    [Header("手感优化（可选）")]
    public float coyoteTime = 0.12f;
    public float jumpBufferTime = 0.15f;
}
