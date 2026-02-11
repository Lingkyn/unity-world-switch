using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 输入意图适配层：将 New Input System 的输入转为「玩家意图」（Move / Jump），
/// 供 Gameplay 消费。负责死区等轻量处理，Movement 不再直接接 InputAction。
/// 请挂在与 PlayerInput 同一物体上，并在 PlayerInput 的 Move/Jump 事件中只绑定本脚本的 OnMove/OnJump。
/// </summary>
public class InputIntentAdapter : MonoBehaviour
{
    public static InputIntentAdapter Instance { get; private set; }

    [Header("轻量输入处理")]
    [Range(0f, 1f)]
    [SerializeField] private float moveDeadZone = 0.15f;

    private Vector2 _moveIntent;
    private bool _jumpPressedThisFrame;

    /// <summary> 当前移动意图（已做死区），仅读。 </summary>
    public Vector2 MoveIntent => _moveIntent;

    /// <summary> 本帧是否按下跳跃（每帧最多 true 一帧，在 LateUpdate 末清除）。 </summary>
    public bool JumpPressedThisFrame => _jumpPressedThisFrame;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 raw = context.ReadValue<Vector2>();
        _moveIntent = raw.magnitude < moveDeadZone ? Vector2.zero : raw;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            _jumpPressedThisFrame = true;
    }

    void LateUpdate()
    {
        _jumpPressedThisFrame = false;
    }
}
