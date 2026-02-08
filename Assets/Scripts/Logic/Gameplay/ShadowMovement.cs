using UnityEngine;
using UnityEngine.InputSystem;

public class ShadowMovement2D : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fixedZ = 0f; // 影子世界固定Z

    private Vector2 moveInput;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    void OnEnable()
    {
        moveInput = Vector2.zero;
    }

    void Update()
    {
        // Together 与 ShadowOnly 时影子都响应输入；仅 Player 在 ShadowOnly 时不动（见 PlayerMovement）
        Vector3 delta = new Vector3(moveInput.x, moveInput.y, 0f) * speed * Time.deltaTime;
        transform.position += delta;

        // 锁死 Z，保证是“2D世界”
        var p = transform.position;
        p.z = fixedZ;
        transform.position = p;
    }
}
