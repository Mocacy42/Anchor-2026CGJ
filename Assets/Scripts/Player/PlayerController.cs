using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Mathematics;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D)),RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CapsuleCollider2D _coll;
    [SerializeField] private AnchorAbility _anchor;

    [Header("Move")]
    [SerializeField] private Vector2 _movedir;
    [SerializeField] private float _moveSpeedMultiper = 1.0f;
    [SerializeField] private float _maxMoveSpeed = 10f;
    [SerializeField] private float _frictionSpeed = 1.0f;

    [Header("Jump")]
    [SerializeField] private int _maxJumpCount = 1;
    [SerializeField] private int _currJumpCount = 0;
    [SerializeField] private float _jumpForce = 1.0f;
    [SerializeField] private bool _isJumpPressed;
    [SerializeField] private bool _isJumping = false;

    [Header("Anim")]
    [SerializeField] private bool _isWalking = false;
    [SerializeField] private Animator _anim;

    
  


    void Awake()
    {
        InputInstance.Instance.PInput.Player.MOVE.performed += ctx => _movedir = ctx.ReadValue<Vector2>();
        InputInstance.Instance.PInput.Player.MOVE.canceled += ctx => _movedir = Vector2.zero;
        InputInstance.Instance.PInput.Player.JUMP.started += ctx => _isJumpPressed = true;
        InputInstance.Instance.PInput.Player.JUMP.canceled += ctx => _isJumpPressed = false;

        if(_rb == null)  gameObject.TryGetComponent<Rigidbody2D>(out _rb);
        if(_coll == null)  gameObject.TryGetComponent<CapsuleCollider2D>(out _coll);
        if(_anchor == null) gameObject.TryGetComponent<AnchorAbility>(out _anchor);
        if(_anim == null) gameObject.TryGetComponent(out _anim);
    }
    private void OnEnable() 
    {
        InputInstance.Instance.PInput.Enable();
    }
    private void OnDisable() 
    {
        InputInstance.Instance.PInput.Disable();    
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.localScale = new Vector3( math.abs(transform.localScale.x) * (mouseWorld.x > transform.position.x ? 1 : -1),
            transform.localScale.y , transform.localScale.z);
        
        if(math.abs(_movedir.x) < 0.0001f) _isWalking = false;
        else _isWalking = true;
        
        _anim.SetBool("IsWalking",_isWalking);

        
    }
    // 移动逻辑
    private void Move()
    {
        if(math.abs( _movedir.x) >  0.0001f)
        {
            Vector2 moveV = _movedir * _moveSpeedMultiper * Time.deltaTime;
            Vector2 finalV = _rb.velocity + new Vector2 (moveV.x, 0);
            finalV.x = math.clamp(finalV.x,-_maxMoveSpeed, _maxMoveSpeed);
            _rb.velocity = finalV;
            
        }
        else
        {
            Vector2 friction = new Vector2(_frictionSpeed, 0) * Time.deltaTime;
            float dirmultiper = _rb.velocity.x > 0 ? 1 : (_rb.velocity.x == 0 ? 0 : -1);
            Vector2 finalV = _rb.velocity - friction * dirmultiper;
            _rb.velocity = finalV;
        }
    }
    // 跳跃逻辑
    private void Jump()
    {
        if(_isJumpPressed && _currJumpCount < _maxJumpCount)
        {
            ++_currJumpCount;
            _rb.AddForce(Vector2.up * _jumpForce,ForceMode2D.Impulse);
            _isJumpPressed = false;
            _isJumping = true;
        }
        CheckGround();
    }
    // 检测地面tag，清除jump标记
    private void CheckGround()
    {

        RaycastHit2D[] hitinfo1 = Physics2D.RaycastAll(transform.position - new Vector3(0.2f, 1f, 0f) ,Vector2.down ,  0.1f);
        RaycastHit2D[] hitinfo2 = Physics2D.RaycastAll(transform.position - new Vector3( - 0.2f, 1f, 0f) ,Vector2.down,  0.1f);

        bool hitGround1 = false;
        foreach (var hit in hitinfo1)
        {
            if (!hit.collider.isTrigger)
            {
                hitGround1 = true;
                break;
            }
        }
        bool hitGround2 = false;
        foreach (var hit in hitinfo2)
        {
            if (!hit.collider.isTrigger)
            {
                hitGround2 = true;
                break;
            }
        }

        if((hitGround1 || hitGround2) && _rb.velocity.y <= 0)
        {
            _isJumping = false;
            _currJumpCount = 0;
        } 
    }


    // 播放死亡动画
    public void OnPlayerDie()
    {
        Destroy(gameObject);
    }

}
