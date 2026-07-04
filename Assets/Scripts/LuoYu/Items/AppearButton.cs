using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearButton : AppearItem, IInteractive
{
    //���ص�ǰ״̬
    public bool isOpen = false;
    //��Ӧƽ̨�б�
    [SerializeField] private List<Object> platforms = new List<Object>();
    [SerializeField] private Collider2D _trigger;
    public override void EffectAppear() { _trigger.enabled = true;}

    public override void EffectDisappear() { _trigger.enabled = false;}

    //����Ч��
    public void InteractiveEffect()
    {
        //�ı俪��״̬
        isOpen = !isOpen;

        if (isOpen)
        {

            //�ı��Ӧƽ̨״̬
            foreach (GameObject platform in platforms)
            {
                IPlatform temp = platform.GetComponent<IPlatform>();

                if (temp is IPlatform _platform)
                {
                    _platform.ChangeOpenEffect();
                }
            }
        }
        else
        {
            foreach (GameObject platform in platforms)
            {
                IPlatform temp = platform.GetComponent<IPlatform>();
                if (temp is IPlatform _platform)
                {
                    _platform.ChangeCloseEffect();
                }
            }
        }

    }
}
