using System.Collections.Generic;
using UnityEngine;

public class PortalButton : AppearItem
{
    //����������
    [SerializeField] private List<Portal> portals = new List<Portal>();
    [SerializeField] private HashSet<Animator> _anims = new HashSet<Animator>();

    //ʵ�ֳ��󷽷�������ʱЧ��
    public override void EffectAppear()
    {
        foreach(var portal in portals)
        {
            // portal.gameObject.SetActive(true);
            _anims.Add(portal.gameObject.GetComponent<Animator>());
            portal.CanTeleport = true;
        }

        foreach(var anim in _anims)
        {
            anim.SetBool("IsActive", true);
        }
    }
    //ʵ�ֳ��󷽷�����ʧʱЧ��
    public override void EffectDisappear()
    {
        foreach (var portal in portals)
        {
            //portal.gameObject.SetActive(false);
            portal.CanTeleport = false;
        }
        foreach(var anim in _anims)
        {
            anim.SetBool("IsActive", false);
        }
    }
}
