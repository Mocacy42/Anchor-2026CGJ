using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PF_Fan : MonoBehaviour,IPlatform
{
    [SerializeField] private Collider2D _coll;
    [SerializeField] private bool _isEnter = false;
    [SerializeField] private Rigidbody2D _playerRB;
    [SerializeField] private float _baseAirPower = 10f;
    [SerializeField] private float _airLength = 10f;
    [SerializeField] private float _verticalDamp = 0.2f;
    [SerializeField] private ParticleSystem _ps;
    [SerializeField] private ParticleSystem.EmissionModule _emission ;
    [SerializeField] private Animator _anim;
    void Awake()
    {
        if(_coll == null) TryGetComponent<Collider2D>(out _coll);
        if(_ps == null) TryGetComponent<ParticleSystem>(out _ps);
        if(_anim == null) TryGetComponent<Animator>(out _anim);
        _emission = _ps.emission;
    }

    public void InteractiveEffect()
    {
        _coll.enabled = !_coll.enabled;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(_playerRB == null) collision.gameObject.TryGetComponent<Rigidbody2D>(out _playerRB);
            _isEnter = true;
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            _isEnter = false;
    }

    void Update()
    {
        if(_isEnter && _coll.enabled)
        {            
            float playerY = _playerRB.transform.position.y;
            float windCenterY = transform.position.y;
            float heightDiff = Mathf.Abs(playerY - windCenterY);

            float t = Mathf.Clamp01(heightDiff / _airLength);
            float currentWindPower = Mathf.Lerp(_baseAirPower, 0, t);

            Vector2 windForce = Vector2.up * currentWindPower;
            float dampForceY = -_playerRB.velocity.y * _verticalDamp;
            windForce.y += dampForceY;
            _playerRB.AddForce(windForce, ForceMode2D.Force);

        }
        if(_coll.enabled)
        {
            _anim.SetBool("IsFanning",true);
            _emission.rateOverTime = 5;
        }
        else
        {
            _anim.SetBool("IsFanning",false);
            _emission.rateOverTime = 0;
        }
    }

    public void ChangeOpenEffect()
    {
        InteractiveEffect();
    }

    public void ChangeCloseEffect()
    {
        InteractiveEffect();
    }
}
