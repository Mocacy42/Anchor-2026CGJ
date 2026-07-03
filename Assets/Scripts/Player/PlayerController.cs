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
    private PlayerInput _input;
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
    

    void Awake()
    {
        if(_input == null) _input = new PlayerInput();
        _input.Player.MOVE.performed += ctx => _movedir = ctx.ReadValue<Vector2>();
        _input.Player.MOVE.canceled += ctx => _movedir = Vector2.zero;
        _input.Player.JUMP.started += ctx => _isJumpPressed = true;
        _input.Player.JUMP.canceled += ctx => _isJumpPressed = false;

        if(_rb == null)  gameObject.TryGetComponent<Rigidbody2D>(out _rb);
        if(_coll == null)  gameObject.TryGetComponent<CapsuleCollider2D>(out _coll);
        if(_anchor == null) gameObject.TryGetComponent<AnchorAbility>(out _anchor);
    }
    private void OnEnable() 
    {
        _input.Enable();
    }
    private void OnDisable() 
    {
        _input.Disable();    
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
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
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - 0.5f);
        var colls = Physics2D.OverlapCircleAll(pos,0.1f);

        if(colls.Count() > 1)
        {        
            foreach(var item in colls)
            {
                if(item.CompareTag("Ground") == true && _rb.velocity.y <= 0)
                {
                    _isJumping = false;
                }
            }
        }
        if(_isJumping == false) _currJumpCount = 0;
    }


    

    // 播放死亡动画
    public void OnPlayerDie()
    {
        Destroy(gameObject);
    }

}
