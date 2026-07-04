using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PF_FourDirectionmove : DisappearItem,IPlatform
{
    //平台开关状态
    [SerializeField] private bool isOpen = false;
    //平台移动速度
    [SerializeField] private float moveSpeed = 1f;

    [Header("平台最大/小移动高度")]
    //最大高度
    [SerializeField] private float maxHeight;
    //最小高度
    [SerializeField] private float minHeight;
    [Header("平台最大/小水平位置")]
    //最大水平位置
    [SerializeField] private float maxHP;
    //最小水平位置
    [SerializeField] private float minHP;
    
    [SerializeField] private float direction = 1f; //移动方向 1：向上/右,-1：向下/左


    //判断是否为触发器
    [SerializeField] public bool isTrigger;

    private void FixedUpdate()
    {
        if (isTrigger)
        {
            GetComponent<Collider2D>().isTrigger = isTrigger;
        }
    }

    public void ChangeOpenEffect()
    {
        isOpen = true;
    }
    public void ChangeCloseEffect()
    {
        isOpen = false;
    }

    private void Update()
    {
        //开启状态时持续移动
        if (isOpen)
        {
            transform.Translate(Vector3.up * moveSpeed * direction * Time.deltaTime);
        }else
        {
            transform.Translate(Vector3.left * moveSpeed * direction * Time.deltaTime);
        }
        //到达最大/小高度或水平位置变换方向
        if (transform.position.y < minHeight || transform.position.y > maxHeight ||
            transform.position.x < minHP || transform.position.x > maxHP)
        {
            direction *= -1;
        }
    }
    void IPlatform.SetIsTrigger(bool _isTrigger)
    {
        isTrigger = _isTrigger;
    }
    public override void EffectDisappear()
    {
        
    }

    public override void EffectAppear()
    {
        
    }
}
