using UnityEngine;
using UnityEngine.InputSystem;

public class InputAdapter : MonoBehaviour
{
    public static InputAdapter Instance { get; private set; }

    [Range(0f, 1f)] [SerializeField] private float moveDeadZone = 0.15f;

    private Vector2 _moveIntent;
    private bool _jumpPressedThisFrame;
    private bool _toggleWorldPressedThisFrame;

    public Vector2 MoveIntent => _moveIntent;
    public bool JumpPressedThisFrame => _jumpPressedThisFrame;
    public bool ToggleWorldPressedThisFrame => _toggleWorldPressedThisFrame;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void OnDestroy() { if (Instance == this) Instance = null; }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 raw = context.ReadValue<Vector2>();
        _moveIntent = raw.magnitude < moveDeadZone ? Vector2.zero : raw;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) _jumpPressedThisFrame = true;
    }

    public void OnToggleWorld(InputAction.CallbackContext context)
    {
        if (context.performed) _toggleWorldPressedThisFrame = true;
    }

    void LateUpdate()
    {
        _jumpPressedThisFrame = false;
        _toggleWorldPressedThisFrame = false;
    }
}
