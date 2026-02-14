using UnityEngine;

/// <summary>
/// 玩家 3D 移动与跳跃，只消费 InputAdapter 的意图，不直接接输入。
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("配置")]
    [SerializeField] private PlayerMovementConfig config;

    [Header("移动参数（config 为空时使用）")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = -20f;
    [Range(0f, 1f)]
    [SerializeField] private float airControl = 0.4f;
    [Header("手感优化（可选）")]
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.15f;

    private float MoveSpeed => config != null ? config.moveSpeed : moveSpeed;
    private float JumpForce => config != null ? config.jumpForce : jumpForce;
    private float Gravity => config != null ? config.gravity : gravity;
    private float AirControl => config != null ? config.airControl : airControl;
    private float CoyoteTime => config != null ? config.coyoteTime : coyoteTime;
    private float JumpBufferTime => config != null ? config.jumpBufferTime : jumpBufferTime;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool jumpRequested;
    private float lastGroundedTime = -99f;   // 上次着地时间，用于 Coyote Time
    private float lastJumpPressTime = -99f;  // 上次按跳跃时间，用于 Jump Buffer（初始为 -99 避免开局误触发缓冲跳）
    private bool hasBeenGroundedOnce;        // 是否已经着地过，避免首帧 isGrounded 未就绪时误加重力

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("PlayerMovement 需要同一物体上有 CharacterController。", this);
            enabled = false;
        }
    }

    void Update()
    {
        if (characterController == null) return;
        if (CharacterControlManager.Instance != null && CharacterControlManager.Instance.CurrentMode == CharacterControlManager.ControlMode.ShadowOnly)
            return;

        // 从输入意图适配层取意图（死区已在 Adapter 内处理）
        if (InputAdapter.Instance != null)
        {
            if (InputAdapter.Instance.JumpPressedThisFrame)
            {
                jumpRequested = true;
                lastJumpPressTime = Time.time;
            }
        }

        bool isGrounded = characterController.isGrounded;
        if (isGrounded)
        {
            lastGroundedTime = Time.time;
            hasBeenGroundedOnce = true;
        }

        // 竖直速度：重力 + 跳跃
        if (isGrounded)
        {
            if (velocity.y < 0f)
                velocity.y = -2f;

            if (jumpRequested || (Time.time - lastJumpPressTime) <= JumpBufferTime)
            {
                velocity.y = JumpForce;
                jumpRequested = false;
                lastJumpPressTime = -99f; // 消耗掉 buffer
            }
        }
        else
        {
            // 首帧 CharacterController 可能尚未报告 isGrounded，避免误加重力导致“先掉一下”
            if (hasBeenGroundedOnce)
                velocity.y += Gravity * Time.deltaTime;
            else if (velocity.y < 0f)
                velocity.y = -2f;
            bool canCoyoteJump = (Time.time - lastGroundedTime) <= CoyoteTime && jumpRequested;
            if (canCoyoteJump)
            {
                velocity.y = JumpForce;
                jumpRequested = false;
            }
        }

        // 水平移动：意图已含死区，归一化后乘速度，空中用 airControl 减弱
        Vector2 moveInput = InputAdapter.Instance != null ? InputAdapter.Instance.MoveIntent : Vector2.zero;
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        if (move.sqrMagnitude > 1f)
            move.Normalize();
        float effectiveSpeed = MoveSpeed * (isGrounded ? 1f : AirControl);
        velocity.x = move.x * effectiveSpeed;
        velocity.z = move.z * effectiveSpeed;

        characterController.Move(velocity * Time.deltaTime);
    }
}
