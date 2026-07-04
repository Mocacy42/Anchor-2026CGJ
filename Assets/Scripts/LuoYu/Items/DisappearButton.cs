using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearButton : DisappearItem,IInteractive
{
    //���ص�ǰ״̬
    public bool isOpen = false;
    //��Ӧƽ̨�б�
    [SerializeField] private List<Object> platforms = new List<Object>();

    public override void EffectAppear() { }

    public override void EffectDisappear() { }

    //����Ч��
    public void InteractiveEffect()
    {
        //�ı俪��״̬
        isOpen = !isOpen;
        
        if(isOpen)
        {
            //�ı��Ӧƽ̨״̬
            foreach (var platform in platforms)
            {
                if(platform is IPlatform _platform)
                {
                    _platform.ChangeOpenEffect();
                }
            }
        }
        else
        {
            foreach (var platform in platforms)
            {
                if (platform is IPlatform _platform)
                {
                    _platform.ChangeCloseEffect();
                }
            }
        }
        
    }
}
