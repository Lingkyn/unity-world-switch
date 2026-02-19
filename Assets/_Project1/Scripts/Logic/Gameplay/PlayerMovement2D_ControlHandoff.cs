using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D_ControlHandoff : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fixedZ = 0f;

    private Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void FixedUpdate()
    {
        if (rb == null) return;
        Vector2 moveInput = InputAdapter.Instance != null ? InputAdapter.Instance.MoveIntent : Vector2.zero;
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        var p = transform.position;
        p.z = fixedZ;
        transform.position = p;
    }
}
