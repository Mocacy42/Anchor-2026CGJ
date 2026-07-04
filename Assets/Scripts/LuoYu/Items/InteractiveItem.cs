using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    [SerializeField] private IInteractive _IInteractive;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AnchorAbility _anchorAB;

    void Awake()
    {
        if(_IInteractive == null) _IInteractive = GetComponent<IInteractive>();
        
    }
    // 实际作用
    public void InteractiveEffect()
    {
        _IInteractive?.InteractiveEffect();
        if(_IInteractive == null) Debug.Log("IInteractive failed");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(playerController == null) playerController = collision.GetComponent<PlayerController>();
            if(_anchorAB == null) _anchorAB = playerController.GetComponentInChildren<AnchorAbility>();
        }
    }

    // 修改为 stay
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(_anchorAB.IsInteractPressed)
            {
                _anchorAB.IsInteractPressed = false;
                InteractiveEffect();
            }
            
        }
    }
}
