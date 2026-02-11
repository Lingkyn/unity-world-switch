using UnityEngine;

/// <summary>
/// 影子 2D 移动，只消费 InputIntentAdapter 的移动意图，不直接接输入。
/// Together 与 ShadowOnly 时影子都响应输入；仅 Player 在 ShadowOnly 时不动（见 PlayerMovement）。
/// </summary>
public class ShadowMovement2D : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fixedZ = 0f; // 影子世界固定Z

    void Update()
    {
        Vector2 moveInput = InputIntentAdapter.Instance != null ? InputIntentAdapter.Instance.MoveIntent : Vector2.zero;
        Vector3 delta = new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime;
        transform.position += delta;

        // 锁死 Z，保证是“2D世界”
        var p = transform.position;
        p.z = fixedZ;
        transform.position = p;
    }
}
