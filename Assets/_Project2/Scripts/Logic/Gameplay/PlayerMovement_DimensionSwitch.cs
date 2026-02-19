using UnityEngine;

/// <summary>
/// 维度切换模块：2D 仅 X 轴，3D 为 X+Z。
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement_DimensionSwitch : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f, jumpForce = 8f, gravity = -20f;
    [Range(0f, 1f)] [SerializeField] private float airControl = 0.4f;
    [SerializeField] private float coyoteTime = 0.12f, jumpBufferTime = 0.15f;
    [SerializeField] private float fixedZ2D = 0f;

    private CharacterController cc;
    private Vector3 velocity;
    private bool jumpRequested;
    private float lastGroundedTime = -99f, lastJumpPressTime = -99f;
    private bool hasBeenGroundedOnce;

    public bool Is2DMode { get; private set; }

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (cc == null) { Debug.LogError("PlayerMovement_DimensionSwitch 需要 CharacterController。", this); enabled = false; }
    }

    void OnEnable() => EventBus.Subscribe<DimensionSwitched>(OnDimensionSwitched);
    void OnDisable() => EventBus.Unsubscribe<DimensionSwitched>(OnDimensionSwitched);
    void OnDimensionSwitched(DimensionSwitched e) => Is2DMode = e.NewDimension == DimensionType.TwoD;

    void Start()
    {
        if (cc == null) return;
        var ctrl = FindFirstObjectByType<DimensionSwitchController>();
        if (ctrl != null) Is2DMode = ctrl.CurrentDimension == DimensionType.TwoD;
    }

    void Update()
    {
        if (cc == null) return;

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
        Vector3 move = Is2DMode ? transform.right * moveInput.x : transform.right * moveInput.x + transform.forward * moveInput.y;
        velocity.z = Is2DMode ? 0f : velocity.z;

        if (move.sqrMagnitude > 1f) move.Normalize();
        float speed = moveSpeed * (grounded ? 1f : airControl);
        velocity.x = move.x * speed;
        if (!Is2DMode) velocity.z = move.z * speed;

        cc.Move(velocity * Time.deltaTime);

        if (Is2DMode) { var p = transform.position; p.z = fixedZ2D; transform.position = p; }
    }
}
