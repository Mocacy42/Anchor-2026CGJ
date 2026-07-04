using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearButton : DisappearItem,IInteractive
{
    //开关当前状态
    public bool isOpen = false;
    //对应平台列表
    [SerializeField] private List<Object> platforms = new List<Object>();

    public override void EffectAppear() { }

    public override void EffectDisappear() { }

    //交互效果
    public void InteractiveEffect()
    {
        //改变开关状态
        isOpen = !isOpen;

        if (isOpen)
        {
            Debug.Log("打开开关");
            //改变对应平台状态
            foreach (GameObject platform in platforms)
            {
                IPlatform temp = platform.GetComponent<IPlatform>();
                Debug.Log($"遍历开关, 对象类型: {platform.GetType().Name}");
                if (temp is IPlatform _platform)
                {
                    Debug.Log("切换开关");
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
