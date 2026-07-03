using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AnchorAbility : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    void Awake()
    {
        if(_rb == null)  gameObject.TryGetComponent<Rigidbody2D>(out _rb);
    }

    // 传送至锚点，后续可以修改为协程控制
    public void TeleportToAnchor(Vector2 targetPos)
    {
        transform.position = targetPos;
        _rb.velocity = Vector2.zero;
    }
}
