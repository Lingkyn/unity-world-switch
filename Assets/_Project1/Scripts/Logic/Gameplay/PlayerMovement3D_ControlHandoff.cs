using UnityEngine;

/// <summary>
/// 控制权切换模块：3D 角色的移动，受 CharacterControlManager 控制。
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement3D_ControlHandoff : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f, jumpForce = 8f, gravity = -20f;
    [Range(0f, 1f)] [SerializeField] private float airControl = 0.4f;
    [SerializeField] private float coyoteTime = 0.12f, jumpBufferTime = 0.15f;

    private CharacterController cc;
    private Vector3 velocity;
    private bool jumpRequested;
    private float lastGroundedTime = -99f, lastJumpPressTime = -99f;
    private bool hasBeenGroundedOnce;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        if (cc == null) { Debug.LogError("PlayerMovement3D_ControlHandoff 需要 CharacterController。", this); enabled = false; }
    }

    void Update()
    {
        if (cc == null) return;
        if (CharacterControlManager.Instance != null && CharacterControlManager.Instance.CurrentMode == CharacterControlManager.ControlMode.TwoDOnly)
            return;

        if (InputAdapter.Instance != null && InputAdapter.Instance.JumpPressedThisFrame)
        {
            jumpRequested = true;
            lastJumpPressTime = Time.time;
        }

        bool grounded = cc.isGrounded;
        if (grounded) { lastGroundedTime = Time.time; hasBeenGroundedOnce = true; }

        if (grounded)
        {
            if (velocity.y < 0f) velocity.y = -2f;
            if (jumpRequested || (Time.time - lastJumpPressTime) <= jumpBufferTime)
            {
                velocity.y = jumpForce;
                jumpRequested = false;
                lastJumpPressTime = -99f;
            }
        }
        else
        {
            if (hasBeenGroundedOnce) velocity.y += gravity * Time.deltaTime;
            else if (velocity.y < 0f) velocity.y = -2f;
            if ((Time.time - lastGroundedTime) <= coyoteTime && jumpRequested)
            {
                velocity.y = jumpForce;
                jumpRequested = false;
            }
        }

        Vector2 moveInput = InputAdapter.Instance != null ? InputAdapter.Instance.MoveIntent : Vector2.zero;
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        if (move.sqrMagnitude > 1f) move.Normalize();
        float speed = moveSpeed * (grounded ? 1f : airControl);
        velocity.x = move.x * speed;
        velocity.z = move.z * speed;

        cc.Move(velocity * Time.deltaTime);
    }
}
