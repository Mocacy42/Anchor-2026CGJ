using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : DisappearItem
{
    [SerializeField] private IInteractive _IInteractive;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AnchorAbility _anchorAB;
    [SerializeField] private bool _canInteract = false;
    [SerializeField] private Animator _anim;
    [SerializeField] private bool _triggerCanceled = false;
    void Awake()
    {
        if(_IInteractive == null) TryGetComponent<IInteractive>(out _IInteractive);
        if(_anim == null) TryGetComponent<Animator>(out _anim);
    }
    // 实际作用
    public void InteractiveEffect()
    {
        _IInteractive?.InteractiveEffect();
        _anim.SetBool("SwitchOn",!_anim.GetBool("SwitchOn"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(playerController == null) playerController = collision.GetComponent<PlayerController>();
            if(_anchorAB == null) _anchorAB = playerController.GetComponentInChildren<AnchorAbility>();
            _canInteract = true;
        }
    }

    void Update()
    {
        if(_canInteract && _anchorAB.IsInteractPressed && _triggerCanceled == false)
        {
            InteractiveEffect();
            _anchorAB.IsInteractPressed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _anchorAB.IsInteractPressed = false;
            _canInteract = false;
        }
    }

    public override void EffectDisappear()
    {
        _triggerCanceled = true;
    }

    public override void EffectAppear()
    {
        _triggerCanceled = false;
    }
}
