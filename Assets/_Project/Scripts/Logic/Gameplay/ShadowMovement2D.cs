using UnityEngine;

/// <summary>
/// 影子 2D 移动，只消费 InputAdapter 的移动意图，不直接接输入。
/// 使用 Rigidbody2D.MovePosition 保证物理触发器稳定检测。
/// Together 与 ShadowOnly 时影子都响应输入；仅 Player 在 ShadowOnly 时不动（见 PlayerMovement）。
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class ShadowMovement2D : MonoBehaviour
{
    [Header("配置")]
    [SerializeField] private ShadowMovementConfig config;
    [Header("参数（config 为空时使用）")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fixedZ = 0f;

    private float Speed => config != null ? config.speed : speed;
    private float FixedZ => config != null ? config.fixedZ : fixedZ;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        Vector2 moveInput = InputAdapter.Instance != null ? InputAdapter.Instance.MoveIntent : Vector2.zero;
        rb.MovePosition(rb.position + moveInput * Speed * Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        // 锁死 Z，保证是"2D世界"
        var p = transform.position;
        p.z = FixedZ;
        transform.position = p;
    }
}
