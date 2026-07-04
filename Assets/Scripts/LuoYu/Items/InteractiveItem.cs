using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    [SerializeField] private IInteractive _IInteractive;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AnchorAbility _anchorAB;
    [SerializeField] private bool _canInteract = false;

    void Awake()
    {
        if(_IInteractive == null) TryGetComponent<IInteractive>(out _IInteractive);
    }
    // 实际作用
    public void InteractiveEffect()
    {
        _IInteractive?.InteractiveEffect();
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
        if(_canInteract && _anchorAB.IsInteractPressed)
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
}
