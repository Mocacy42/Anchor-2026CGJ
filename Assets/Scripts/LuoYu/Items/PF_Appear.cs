using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PF_Appear : AppearItem,IPlatform
{
    //Æ½Ì¨¿ª¹Ø×´Ì¬
    [SerializeField] private bool isOpen = false;

    //¿ªÆô×´Ì¬ÇÐ»»
    public void ChangeOpenEffect()
    {
        isOpen = true;
        gameObject.SetActive(false);
    }
    //¹Ø±Õ×´Ì¬ÇÐ»»
    public void ChangeCloseEffect()
    {
        isOpen = false;
        gameObject.SetActive(true);
    }

    public override void EffectAppear()
    {
        
    }

    public override void EffectDisappear()
    {
        
    }
}
