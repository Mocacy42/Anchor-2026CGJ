using UnityEngine;

public class MoveController : MonoBehaviour
{
    //刚体组件
    [SerializeField] private Rigidbody2D rb;
    //移动速度
    [SerializeField] private float moveSpeed = 1.0f;
    //移动方向
    [SerializeField] protected float moveInput;
    void Update()
    {
       moveInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed * moveInput, rb.velocity.y);
    }
}
