using System.Collections.Generic;
using UnityEngine;

public class PortalButton : AppearItem
{
    //传送门引用
    [SerializeField] private List<Portal> portals = new List<Portal>();

    //实现抽象方法，出现时效果
    public override void EffectAppear()
    {
        foreach(var portal in portals)
        {
            portal.gameObject.SetActive(true);
        }
    }
    //实现抽象方法，消失时效果
    public override void EffectDisappear()
    {
        foreach (var portal in portals)
        {
            portal.gameObject.SetActive(false);
        }
    }
}
