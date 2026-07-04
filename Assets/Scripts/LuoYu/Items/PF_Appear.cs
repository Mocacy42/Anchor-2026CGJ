using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PF_Appear : AppearItem,IPlatform
{
    //ƽ̨����״̬
    [SerializeField] private bool isOpen = false;

    //����״̬�л�
    public void ChangeOpenEffect()
    {
        isOpen = true;
        gameObject.SetActive(false);
    }
    //�ر�״̬�л�
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
