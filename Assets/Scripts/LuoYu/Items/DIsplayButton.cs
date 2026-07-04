using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayButton : MonoBehaviour
{
    //开关当前状态
    public bool isOpen = false;
    //对应平台列表
    [SerializeField] private List<IPlatform> platforms = new List<IPlatform>();

    //交互效果
    public void InteractiveEffect()
    {
        //改变开关状态
        isOpen = !isOpen;

        if (isOpen)
        {
            //改变对应平台状态
            foreach (var platform in platforms)
            {
                if (platform is IPlatform _platform)
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
